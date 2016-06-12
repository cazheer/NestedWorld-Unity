using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class ServerListener : MonoBehaviour
{
    private static int TIME_OUT = 10000;
    private static string HOST = "eip.kokakiwi.net";
    private static int PORT = 6464;

    [HideInInspector]
    public bool isStarted = false;
    private NetworkClient network = new NetworkClient();

    public void StartConnexion()
    {
        isStarted = true;

        network.RegisterHandler(MsgType.Connect, OnConnected);
        network.Connect(HOST, PORT);
    }

    void OnConnected(NetworkMessage netMsg)
    {
        Debug.Log("Connected to server");
    }
}
