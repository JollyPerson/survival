using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System.Net;

public class NetworkManager {
    private NetworkHUD networkHUD;
    private AsyncClient _client;
    public static NetworkManager INSTANCE;

    public NetworkManager(NetworkHUD hud) {
        INSTANCE = this;
        this.networkHUD = hud;
        _client = new AsyncClient();
        _client.OnPacketReceived += (object sender, PacketReceivedEventArgs e) =>
        {
            
        };
    }

    public void Connect()
    {
        _client.StartClient(IPAddress.Parse(networkHUD.ip.text), 8001);
        networkHUD.ip.gameObject.SetActive(false);
        networkHUD.connect.gameObject.SetActive(false);

    }

    public void SendPacket(Packet p)
    {
       
        _client.Send(p);
    }

    public void OnApplicationQuit()
    {
        _client.Dispose();
    }
}
