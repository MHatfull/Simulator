using UnityEngine;
using UnityEngine.Networking;

public class Lobby : MonoBehaviour {

    enum Mode { Client, Host, Server }

    [SerializeField] Mode _editorMode = Mode.Host;
    [SerializeField] Mode _standaloneMode = Mode.Client;
    [SerializeField] string _host = "localhost";
    [SerializeField] bool _webSockets = false;
    [SerializeField] int _port = 4444;

    void Start()
    {
        switch (Application.platform)
        {
            case RuntimePlatform.WindowsEditor:
                StartInMode(_editorMode);
                break;
            case RuntimePlatform.WindowsPlayer:
                StartInMode(_standaloneMode);
                break;
            default:
                StartInMode(Mode.Client);
                break;
        }
    }

    private void StartInMode(Mode mode)
    {
        var manager = GetComponent<NetworkManager>();
        manager.networkPort = _port;
        manager.useWebSockets = _webSockets;
        switch (mode)
        {
            case Mode.Client:
                manager.networkAddress = _host;
                manager.StartClient();
                break;
            case Mode.Server:
                manager.StartServer();
                break;
            case Mode.Host:
                manager.StartHost();
                break;
        }
    }
}
