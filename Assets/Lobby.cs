using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Lobby : MonoBehaviour {

    void Start()
    {
        var manager = GetComponent<NetworkManager>();
        manager.networkPort = 4444;
        manager.useWebSockets = true;
        if (Application.platform == RuntimePlatform.WindowsPlayer)
        {
            manager.StartServer();
            Debug.Log("Server started");
        }
        else
        {
            manager.networkAddress = "localhost";
            var client = manager.StartClient();
            Debug.Log("client started " + client.isConnected);
        }
    }
}
