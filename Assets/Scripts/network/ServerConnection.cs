using System;
using System.Net.Sockets;
using System.Collections.Generic;

public class ServerConnection
{
    public Socket Socket = null;
    public const int BufferSize = 256;
    public byte[] Buffer = new byte[BufferSize];
    public List<byte> Message = new List<byte>();
}