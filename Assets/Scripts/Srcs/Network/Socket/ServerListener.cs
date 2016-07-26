using UnityEngine;
using System;
using System.IO;
using System.Net.Sockets;
using MessagePack.Serveur;
using MessagePack.Client;

public class ServerListener : MonoBehaviour
{
    [HideInInspector]
    public bool socketReady = false;
    TcpClient mySocket = null;
    NetworkStream theStream = null;
    StreamWriter theWriter = null;
    StreamReader theReader = null;
    String Host = "eip.kokakiwi.net";
    Int32 Port = 6464;

    UserData userData;

    public ServeurMessageList serverMsg;
    public ClientMessageList clientMsg;

    void Start()
    {
        userData = gameObject.GetComponent<UserData>();
        clientMsg = new ClientMessageList(this);
        serverMsg = new ServeurMessageList();
    }

    void Update()
    {
        if (socketReady)
            serverMsg.SelectMessage(Read());
    }

    public void setupSocket()
    {
        try
        {
            mySocket = new TcpClient(Host, Port);
            theStream = mySocket.GetStream();
            theWriter = new StreamWriter(theStream);
            theReader = new StreamReader(theStream);
            socketReady = true;

            serverMsg.Init();
            var request = MessagePack.Client.Special.Authenticate.GetAuthenticate(userData.token);
            clientMsg.SendRequest(request);
        }
        catch (Exception e)
        {
            Logger.Instance.Log("Socket error: " + e);
        }
    }

    public void Send(Stream stream)
    {
        if (!socketReady)
            return;
        StreamReader reader = new StreamReader(stream);
        string text = reader.ReadToEnd();
        theWriter.Write(text);
        theWriter.Flush();
    }

    public byte[] Read()
    {
        if (!socketReady)
            return null;
        if (theStream.DataAvailable)
            return System.Text.Encoding.UTF8.GetBytes(theReader.ReadToEnd());
        return System.Text.Encoding.UTF8.GetBytes("");
    }

    public void closeSocket()
    {
        if (theWriter != null)
            theWriter.Close();
        if (theReader != null)
            theReader.Close();
        if (mySocket != null)
            mySocket.Close();
        socketReady = false;
    }
}
