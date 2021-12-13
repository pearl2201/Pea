using Coldairarrow.DotNettySocket;
using MessagePack;
using System;

namespace Pea.Networking.App
{
    public class PeerWs : BasePeer
    {
        private readonly ServerSocketWs _transport;
        private readonly IWebSocketConnection _connection;
        private bool _connected;

        public PeerWs(ServerSocketWs transport, IWebSocketConnection connection) : base()
        {
            _transport = transport;
            _connection = connection;
            _connected = true;
        }
        public override bool IsConnected
        {
            get { return _connected; }

        }

        public void SetConnectionState(bool connected)
        {
            _connected = connected;
        }

        public override void SendMessage(IMessage message)
        {
            byte[] bytes = MessagePackSerializer.Serialize(message);
            var msg = MessagePackSerializer.ConvertToJson(bytes);
            _connection.Send(msg);
        }

        public override void Disconnect(string reason)
        {
            _connection.Close();
        }

        public void HandleDataReceived(string content)
        {
            IIncommingMessage message = null;

            try
            {


                var bytes = MessagePackSerializer.ConvertFromJson(content);

                message = MessagePackSerializer.Deserialize<IIncommingMessage>(bytes);

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

                Serilog.Log.Error("Failed parsing an incomming message: " + e);

                return;
            }

            HandleMessage(message);
        }


    }
}
