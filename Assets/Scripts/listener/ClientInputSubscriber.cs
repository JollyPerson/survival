using System;
using Survival_Game_Server.Packet.data;


    public class ClientInputSubscriber
    {

        public void OnClientInput(PacketEventArgs args)
        {
            ClientInputPacketData data = (ClientInputPacketData) args.Packet.Data;
            Console.WriteLine(data.movement.ToString());
        }
    }
