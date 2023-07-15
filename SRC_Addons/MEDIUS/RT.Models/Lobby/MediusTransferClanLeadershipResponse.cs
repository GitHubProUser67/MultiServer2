using PSMultiServer.SRC_Addons.MEDIUS.RT.Common;
using PSMultiServer.SRC_Addons.MEDIUS.Server.Common.Stream;

namespace PSMultiServer.SRC_Addons.MEDIUS.RT.Models
{
    [MediusMessage(NetMessageClass.MessageClassLobby, MediusLobbyMessageIds.TransferClanLeadershipResponse)]
    public class MediusTransferClanLeadershipResponse : BaseLobbyMessage, IMediusResponse
    {
        public override byte PacketType => (byte)MediusLobbyMessageIds.TransferClanLeadershipResponse;

        public bool IsSuccess => StatusCode >= 0;

        public MessageId MessageID { get; set; }
        public MediusCallbackStatus StatusCode;

        public override void Deserialize(MessageReader reader)
        {
            // 
            base.Deserialize(reader);

            // 
            MessageID = reader.Read<MessageId>();
            reader.ReadBytes(3);
            StatusCode = reader.Read<MediusCallbackStatus>();
        }

        public override void Serialize(MessageWriter writer)
        {
            // 
            base.Serialize(writer);

            // 
            writer.Write(MessageID ?? MessageId.Empty);
            writer.Write(new byte[3]);
            writer.Write(StatusCode);
        }


        public override string ToString()
        {
            return base.ToString() + " " +
                $"MessageID: {MessageID}" + " " +
                $"StatusCode: {StatusCode}";
        }
    }
}