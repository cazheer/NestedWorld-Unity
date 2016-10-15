using System;
using System.IO;
using System.Net.Sockets;
using nestedWorld.logs;
using UnityEngine;

namespace nestedWorld.network.socket.implementation
{
    public class SocketService : MonoBehaviour
    {
        private static string Host = "eip.kokakiwi.net";
        private static int Port = 6464;

        private TcpClient _socket;
        private StreamWriter _streamWriter;
        private StreamReader _streamReader;

        private bool _socketReady = false;

        void Update()
        {
            if (_socketReady)
            ; // TODO : Read
        }

        public void SetupSocket()
        {
            try
            {
                _socket = new TcpClient(Host, Port);
                var stream = _socket.GetStream();
                _streamWriter = new StreamWriter(stream);
                _streamReader = new StreamReader(stream);
                _socketReady = true;
            }
            catch (Exception e)
            {
                ConsoleLogger.Instance.LogError("Socket error: " + e.Message);
            }
        }
    }
}
