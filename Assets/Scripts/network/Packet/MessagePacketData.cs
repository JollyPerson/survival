public class MessagePacketData : PacketData
{
    public string Message { get; set; }

    public MessagePacketData(string data)
    {
        Message = data;
    }
}