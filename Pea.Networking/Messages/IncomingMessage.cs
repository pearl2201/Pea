using MessagePack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pea.Networking
{
    /// <summary>
    ///     Default implementation of incomming message
    /// </summary>
    [MessagePackObject]
    public class IncommingMessage : IIncommingMessage
    {
        private readonly byte[] _data;

        public IncommingMessage(short opCode, byte flags, byte[] data, IPeer peer)
        {
            OpCode = opCode;
            Peer = peer;
            _data = data;
        
        }

        /// <summary>
        ///     Message flags
        /// </summary>
        public byte Flags { get; private set; }

        /// <summary>
        ///     Operation code (message type)
        /// </summary>
        public short OpCode { get; private set; }

        /// <summary>
        ///     Sender
        /// </summary>
        public IPeer Peer { get; private set; }

        /// <summary>
        ///     Ack id the message is responding to
        /// </summary>
        public int? AckResponseId { get; set; }

        /// <summary>
        ///     We add this to a packet to so that receiver knows
        ///     what he responds to
        /// </summary>
        public int? AckRequestId { get; set; }

        /// <summary>
        ///     Returns true, if sender expects a response to this message
        /// </summary>
        public bool IsExpectingResponse
        {
            get { return AckResponseId.HasValue; }
        }

        /// <summary>
        ///     For ordering
        /// </summary>
        public int SequenceChannel { get; set; }

        /// <summary>
        ///     Message status code
        /// </summary>
        public ResponseStatus Status { get; set; }

        /// <summary>
        ///     Respond with a message
        /// </summary>
        /// <param name="message"></param>
        /// <param name="statusCode"></param>
        public void Respond(IMessage message, ResponseStatus statusCode = ResponseStatus.Default)
        {
            message.Status = statusCode;

            if (AckResponseId.HasValue)
                message.AckResponseId = AckResponseId.Value;

            Peer.SendMessage(message);
        }

        /// <summary>
        ///     Respond with data (message is created internally)
        /// </summary>
        /// <param name="data"></param>
        /// <param name="statusCode"></param>
        public void Respond(byte[] data, ResponseStatus statusCode = ResponseStatus.Default)
        {
            Respond(MessageHelper.Create(OpCode, data), statusCode);
        }

        /// <summary>
        ///     Respond with data (message is created internally)
        /// </summary>
        /// <param name="data"></param>
        /// <param name="statusCode"></param>
        public void Respond(ISerializablePacket packet, ResponseStatus statusCode = ResponseStatus.Default)
        {
            Respond(MessageHelper.Create(OpCode, packet.ToBytes()), statusCode);
        }

        /// <summary>
        ///     Respond with empty message and status code
        /// </summary>
        /// <param name="statusCode"></param>
        public void Respond(ResponseStatus statusCode = ResponseStatus.Default)
        {
            Respond(MessageHelper.Create(OpCode), statusCode);
        }

        public void Respond(string message, ResponseStatus statusCode = ResponseStatus.Default)
        {
            Respond(message.ToBytes(), statusCode);
        }

        public void Respond(int response, ResponseStatus statusCode = ResponseStatus.Default)
        {
            Respond(MessageHelper.Create(OpCode, response), statusCode);
        }

        /// <summary>
        ///     Returns true if message contains any data
        /// </summary>
        public bool HasData
        {
            get { return _data.Length > 0; }
        }

        /// <summary>
        ///     Returns contents of this message. Mutable
        /// </summary>
        /// <returns></returns>
        public byte[] AsBytes()
        {
            return _data;
        }

        public override string ToString()
        {
            return AsString(base.ToString());
        }
    }
}