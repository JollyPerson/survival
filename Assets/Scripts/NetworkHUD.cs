using System;
using UnityEngine;
using UnityEngine.UI;

public class NetworkHUD : MonoBehaviour
{
    public InputField ip;
    public InputField name;
    public Button connect;
    private NetworkManager manager;

    public void Connect()
    {
        manager = new NetworkManager(this);
        manager.Connect();
        manager.SendPacket(new Packet(){Data = new PacketData() , Type = PacketType.UPDATE_NAME});
    }
}
