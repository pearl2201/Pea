using MessagePack;

namespace Pea.Networking
{
    public class BaseClientSocket : IMsgDispatcher
    {
        public IPeer Peer { get; set; }

        public void SendMessage(short opCode)
        {
            var msg = new Message(opCode);
            SendMessage(msg);
        }

        public void SendMessage(short opCode, ISerializablePacket packet)
        {
            SendMessage(opCode, packet);
        }

        public void SendMessage(short opCode, ISerializablePacket packet, ResponseCallback responseCallback)
        {
            var msg = new Message(opCode, packet.ToBytes());
            Peer.SendMessage(msg, responseCallback);
        }

        public void SendMessage(short opCode, ISerializablePacket packet, ResponseCallback responseCallback, int timeoutSecs)
        {
            var msg = new Message(opCode, packet.ToBytes());
            Peer.SendMessage(msg, responseCallback, timeoutSecs);
        }


        public void SendMessage(short opCode, ResponseCallback responseCallback)
        {
            var msg = new Message(opCode);
            SendMessage(msg, responseCallback);
        }

        public void SendMessage(short opCode, byte[] data)
        {
            SendMessage(opCode, data);
        }

        public void SendMessage(short opCode, byte[] data, ResponseCallback responseCallback)
        {
            var msg = new Message(opCode, data);
            Peer.SendMessage(msg, responseCallback);
        }

        public void SendMessage(short opCode, byte[] data, ResponseCallback responseCallback, int timeoutSecs)
        {
            var msg = new Message(opCode, data);
            Peer.SendMessage(msg, responseCallback, timeoutSecs);
        }

        public void SendMessage(short opCode, string data)
        {
            SendMessage(opCode, data);
        }

        public void SendMessage(short opCode, string data, ResponseCallback responseCallback)
        {
            var msg = new Message(opCode, MessagePackSerializer.Serialize(data));
            Peer.SendMessage(msg, responseCallback);
        }

        public void SendMessage(short opCode, string data, ResponseCallback responseCallback, int timeoutSecs)
        {
            var msg = new Message(opCode, MessagePackSerializer.Serialize(data));
            Peer.SendMessage(msg, responseCallback, timeoutSecs);
        }

        public void SendMessage(short opCode, int data)
        {
            SendMessage(opCode, data);
        }

        public void SendMessage(short opCode, int data, ResponseCallback responseCallback)
        {
            var msg = new Message(opCode, MessagePackSerializer.Serialize(data));
            Peer.SendMessage(msg, responseCallback);
        }

        public void SendMessage(short opCode, int data, ResponseCallback responseCallback, int timeoutSecs)
        {
            var msg = new Message(opCode, MessagePackSerializer.Serialize(data));
            Peer.SendMessage(msg, responseCallback, timeoutSecs);
        }

        public void SendMessage(IMessage message)
        {
            SendMessage(message);
        }

        public void SendMessage(IMessage message, ResponseCallback responseCallback)
        {
            Peer.SendMessage(message, responseCallback);
        }

        public void SendMessage(IMessage message, ResponseCallback responseCallback, int timeoutSecs)
        {
            Peer.SendMessage(message, responseCallback, timeoutSecs);
        }
    }
}