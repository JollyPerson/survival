using System;
using System.Collections.Generic;



    public class EventHandler
    {
        private static Dictionary<PacketType, HashSet<Action<PacketEventArgs>>> yes = new Dictionary<PacketType, HashSet<Action<PacketEventArgs>>>();
        
        public EventHandler()
        {
            RegisterEvents();      
        }

        public void PacketReceivedListener(object sender, PacketEventArgs args)
        {
            if (yes.ContainsKey(args.Packet.Type))
            {
                foreach (var x in yes[args.Packet.Type])
                {
                    x.Invoke(args);
                }
            }
        }

        public static void AddEventListener(PacketType type, Action<PacketEventArgs> listener)
        {
            if (yes.ContainsKey(type))
            {
                yes[type].Add(listener);
            }
            else
            {
                HashSet<Action<PacketEventArgs>> set = new HashSet<Action<PacketEventArgs>>();
                set.Add(listener);
                yes.Add(type, set);
            }

        }

        public static void RemoveEventListener(PacketType type, Action<PacketEventArgs> listener)
        {
            yes[type].Remove(listener);
        }

        private void RegisterEvents()
        {
            AddEventListener(PacketType.MESSAGE, new MessageSubscriber().OnMessageReceiveEvent);
            AddEventListener(PacketType.HEARTBEAT, new HeartBeatSubscriber().OnHeartBeat);
        }

    }
