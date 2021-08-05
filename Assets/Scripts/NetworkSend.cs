using System;
using KaymakNetwork;

namespace DefaultNamespace
{
    internal enum ClientPackets
    {
        Ping = 1,
    }

    internal static class NetworkSend
    {
        public static void SendPing()
        {
            ByteBuffer buffer = new ByteBuffer(4);
            buffer.WriteInt32((int)ClientPackets.Ping);
            buffer.WriteString("Hello I am the client.");
            NetworkConfig.socket.SendData(buffer.Data, buffer.Head);
            buffer.Dispose();
        }
    }
}