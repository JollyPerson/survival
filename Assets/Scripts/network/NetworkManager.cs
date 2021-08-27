using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System.Net;
using Survival_Game_Server.Packet.data;

public class NetworkManager {
    private NetworkHUD networkHUD;
    private AsyncClient _client;
    private EventHandler _handler;
    public static NetworkManager INSTANCE;

    public NetworkManager(NetworkHUD hud) {
        INSTANCE = this;
        this.networkHUD = hud;
        _client = new AsyncClient();
        _handler = new EventHandler();
        _client.OnPacketReceived += _handler.PacketReceivedListener;
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
