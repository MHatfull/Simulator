using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Lobby : MonoBehaviour {

    void Start()
    {
        var manager = GetComponent<NetworkManager>();
        manager.networkPort = 4444;
        if (Application.platform == RuntimePlatform.LinuxPlayer)
        {
            manager.StartServer();
            Debug.Log("Server started");
        }
        else
        {
            manager.networkAddress = "ec2-34-244-249-255.eu-west-1.compute.amazonaws.com";
            var client = manager.StartClient();
            Debug.Log("client started " + client.isConnected);
        }
    }
}
