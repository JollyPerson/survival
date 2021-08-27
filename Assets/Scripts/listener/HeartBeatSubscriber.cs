
    using UnityEngine;

    public class HeartBeatSubscriber
    {
        private int i = 0;
        public void OnHeartBeat(PacketEventArgs args)
        {
            HeartbeatPacketData packetData =(HeartbeatPacketData) args.Packet.Data;
            Debug.Log("Beat" + i++);
        }
    }
