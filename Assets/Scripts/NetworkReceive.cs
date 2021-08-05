using System;
using KaymakNetwork;
using KaymakNetwork.Network.Client;
using UnityEngine;

namespace DefaultNamespace
{
    enum ServerPacket
    {
        WelcomeMsg = 1,
        InstantiatePlayer,
    }
    internal static class NetworkReceive
    {
        internal static void PacketRouter()
        {
            NetworkConfig.socket.PacketId[(int) ServerPacket.WelcomeMsg] = new Client.DataArgs(Packet_WelcomeMsg);
            NetworkConfig.socket.PacketId[(int) ServerPacket.InstantiatePlayer] = new Client.DataArgs(Packet_InstantiateNetworkPlayer);
        }

        private static void Packet_WelcomeMsg(ref byte[] data)
        {
            ByteBuffer buffer = new ByteBuffer(data);
            string message = buffer.ReadString();
            buffer.Dispose();
            Debug.Log(message);
            
            NetworkSend.SendPing();
        }

        private static void Packet_InstantiateNetworkPlayer(ref byte[] data)
        {
            ByteBuffer buffer = new ByteBuffer(4);
            int connectionID = buffer.ReadInt32();
            NetworkManager.instance.InstantiateNetworkPlayer(connectionID);
        }
    }
}