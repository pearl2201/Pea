using MessagePack;
using System;

namespace Pea.Networking
{

    /// <summary>
    ///     Represents an outgoing message.
    ///     Default barebones implementation
    /// </summary>
    [MessagePackObject]
    public class Message : IMessage
    {
        public Message(short opCode) : this(opCode, new byte[0])
        {
            OpCode = opCode;
            Status = 0;
        }

        public Message(short opCode, byte[] data)
        {
            OpCode = opCode;
            Status = 0;
            SetBinary(data);
        }
        [Key("ReceiverId")]
        public int? ReceiverId { get; set; }

        /// <summary>
        ///     Operation code, a.k.a message type
        /// </summary>
        [Key("OpCode")]
        public short OpCode { get; private set; }

        /// <summary>
        ///     Content of the message
        /// </summary>
        [Key("Data")]
        public byte[] Data { get; private set; }

        /// <summary>
        ///     Returns true if data is not empty
        /// </summary>
        public bool HasData
        {
            get { return Data.Length > 0; }
        }

        /// <summary>
        ///     An id of ack request. It's set when we send a message,
        ///     and expect a response. This is how we tell which message we got a response to
        /// </summary>
        [Key("AckRequestId")]
        public int? AckRequestId { get; set; }

        /// <summary>
        ///     Used to identify what message we are responsing to
        /// </summary>
        [Key("AckResponseId")]
        public int? AckResponseId { get; set; }

        /// <summary>
        ///     Internal flags, used to help identify what kind of message we've received
        /// </summary>
        [Key("Flags")]
        public byte Flags { get; set; }

        /// <summary>
        ///     Status code of the message
        /// </summary>
        [Key("Status")]
        public ResponseStatus Status { get; set; }

        public IMessage SetBinary(byte[] data)
        {
            Data = data;
            return this;
        }

        /// <summary>
        ///     Serializes message to byte array
        /// </summary>
        /// <returns></returns>
        public byte[] ToBytes()
        {
            var messagePacket = MessagePackSerializer.Serialize(this);
            return messagePacket;
        }

        public static byte GenerateFlags(IMessage message)
        {
            var flags = message.Flags;

            if (message.AckRequestId.HasValue)
                flags |= (byte) MessageFlag.AckRequest;

            if (message.AckResponseId.HasValue)
                flags |= (byte) MessageFlag.AckResponse;

            return flags;
        }
    }
}