using MessagePack;
using System.IO;

namespace Pea.Networking
{
    /// <summary>
    ///     Base class for serializable packets
    /// </summary>
    public abstract class SerializablePacket : ISerializablePacket
    {
        public override string ToString()
        {
            var bytes = MessagePackSerializer.Serialize(this);
            return MessagePackSerializer.ConvertToJson(bytes);
        }

        public byte[] ToBytes()
        {
            var bytes = MessagePackSerializer.Serialize(this);
            return bytes;
        }

        public static T FromBytes<T>(byte[] bytes) where T : ISerializablePacket
        {
            var packet = MessagePackSerializer.Deserialize<T>(bytes);

            return packet;
        }

        public static T FromString<T>(string data) where T : ISerializablePacket
        {
            var bytes = MessagePackSerializer.ConvertFromJson(data);
            var packet = MessagePackSerializer.Deserialize<T>(bytes);
            return packet;
        }
    }
}