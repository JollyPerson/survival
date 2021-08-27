using System;
using System.Net;
using System.Net.Sockets;
using UnityEngine;
using System.Linq;

public class AsyncClient : IDisposable
{
    private const int port = 8001;
    private Socket client;

    internal event PacketReceivedEvent OnPacketReceived;

    public void StartClient(IPAddress ip, int port)
    { 
        IPEndPoint remoteEndpoint = new IPEndPoint(ip, port);
        try
        {
            Socket client = new Socket(ip.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            client.BeginConnect(remoteEndpoint, new AsyncCallback(ConnectCallback), client);
        }
        catch (Exception ex)
        {
            Debug.LogError(ex.ToString());
        }
    }
    public void Send(Packet p)
    {
        
        var byteData = p.Serialize();
        client.BeginSend(byteData, 0, byteData.Length, SocketFlags.None, new AsyncCallback(SendCallback), client);
}   
    public void Receive(Socket client)
    {
        try
        {
            ServerConnection state = new ServerConnection();
            state.Socket = client;

            client.BeginReceive(state.Buffer, 0, ServerConnection.BufferSize, 0,
                new AsyncCallback(ReceiveCallback), state);
        }
        catch (Exception e)
        {
            Debug.LogError(e.ToString());
        }
    }


    private void ConnectCallback(IAsyncResult ar)
    {
        try
        {
            client = (Socket)ar.AsyncState;
            client.EndConnect(ar);
            Debug.Log(string.Format("Socket connected to {0}", client.RemoteEndPoint.ToString()));
            Receive(client);
        }
        catch (Exception e)
        {
            Debug.LogError(e.ToString());
        }
    }
    private void ReceiveCallback(IAsyncResult ar)
    {
        ServerConnection state = (ServerConnection)ar.AsyncState;
        Socket client = state.Socket;

        int bytesRead = 0;

        try
        {
            bytesRead = client.EndReceive(ar);
        }
        catch (Exception e)
        {
            Debug.LogError(e.ToString());
        }

        if (bytesRead > 0)
        {
            state.Message.AddRange(state.Buffer.Take(bytesRead));

            int byteCount = BitConverter.ToInt32(state.Message.Take(sizeof(Int32)).ToArray(), 0);
            if (state.Message.Count == byteCount + sizeof(int))
            {
                Packet p = Packet.Deserialize(state.Message);

                OnPacketReceived?.Invoke(this, new PacketEventArgs { Packet = p });

                state.Message.Clear();
            }

            client.BeginReceive(state.Buffer, 0, ServerConnection.BufferSize, 0, new AsyncCallback(ReceiveCallback), state);
        }
    }
    private void SendCallback(IAsyncResult ar)
    {
        try
        {
            Socket client = (Socket)ar.AsyncState;
            int bytesSent = client.EndSend(ar);
            Debug.Log(string.Format("Sent {0} bytes to server.", bytesSent));
        }
        catch (Exception e)
        {
            Debug.LogError(e.ToString());
        }

    }

    internal delegate void PacketReceivedEvent(object sender, PacketEventArgs e);

    
    public void Shutdown()
    {
        client.Shutdown(SocketShutdown.Both);
        client.Close();
    }

    #region IDisposable Support
    private bool disposedValue = false; // To detect redundant calls

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                Shutdown();
            }
            disposedValue = true;
        }
    }
    public void Dispose()
    {
        Dispose(true);
    }
    #endregion
}
