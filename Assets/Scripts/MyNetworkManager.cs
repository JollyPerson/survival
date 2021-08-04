using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class MyNetworkManager : NetworkManager
{
    public override void OnStartServer()
    {
        Debug.Log("Server started");
    }

    public override void OnStopServer()
    {
        Debug.Log("Server stopped");
    }

    public override void OnClientConnect(NetworkConnection conn)
    {
        Debug.Log("Client connected");
    }

    public override void OnClientDisconnect(NetworkConnection conn)
    {
        Debug.Log("Client disconnected");
    }
}
