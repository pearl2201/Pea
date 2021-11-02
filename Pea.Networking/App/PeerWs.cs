using Coldairarrow.DotNettySocket;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pea.Networking.App
{
    public class PeerWs : BasePeer
    {
        private readonly ServerSocketWs _transport;
        private readonly IWebSocketConnection _connection;

        public PeerWs(ServerSocketWs transport, IWebSocketConnection connection) : base()
        {
            _transport = transport;
            _connection = connection;
        }
        public override bool IsConnected => throw new NotImplementedException();

        public override void SendMessage(IMessage message, DeliveryMethod deliveryMethod)
        {
            var msg = message.ToString();
            _connection.Send(msg);
        }

        public override void Disconnect(string reason)
        {
            _connection.Close();
        }

        public void HandleDataReceived(string message)
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
