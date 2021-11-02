
namespace Pea.Networking
{
    public interface ISerializablePacket
    {
        byte[] ToBytes();

        string ToString();
    }
}