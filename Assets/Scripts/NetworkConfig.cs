using System;
using KaymakNetwork.Network;
using KaymakNetwork.Network.Client;

namespace DefaultNamespace
{
    internal static class NetworkConfig
    {
        internal static Client socket;


        internal static void InitNetwork()
        {
            if (!ReferenceEquals(socket, null)) return;
            socket = new Client((int) 100);
            NetworkReceive.PacketRouter();
            
        }

        internal static void ConnectToServer()
        {
            socket.Connect("localhost", 5555);
            
        }

        internal static void DisconnectFromServer()
        {
            socket.Dispose();
        }
    }
}