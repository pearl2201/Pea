using dotNetty_kcp;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pea.Networking.App
{
    public class PeerKcp : BasePeer
    {
        private readonly Ukcp _ukcp;

        public PeerKcp(Ukcp ukcp)
        {
            _ukcp = ukcp;
        }


        public override bool IsConnected => throw new NotImplementedException();


        public override void SendMessage(IMessage message)
        {
            var bytes = message.ToBytes();
            _ukcp.write(bytes);
        }

        public override void Disconnect(string reason)
        {
            _ukcp.close();
        }

        public void HandleDataReceived(byte[] buffer, int start)
        {
            IIncommingMessage message = null;

            try
            {
                message = MessageHelper.FromBytes(buffer, start, this);

                if (message.AckRequestId.HasValue)
                {
                    // We received a message which is a response to our ack request
                    TriggerAck(message.AckRequestId.Value, message.Status, message);
                    return;
                }
            }
            catch (Exception e)
            {
#if UNITY_EDITOR
                if (DontCatchExceptionsInEditor)
                    throw e;
#endif

                Log.Error("Failed parsing an incomming message: " + e);

                return;
            }

            HandleMessage(message);
        }


    }
}
