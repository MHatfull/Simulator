using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(NetworkIdentity))]
[RequireComponent(typeof(Collider))]
public class Collectable : NetworkBehaviour {
    public Sprite Icon;

    protected virtual void Awake()
    {
        GetComponent<Collider>().isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isServer)
        {
            PlayerCharacter player = other.GetComponent<PlayerCharacter>();
            if (player)
            {
                player.Inventory.RpcAddToInventory(this.netId); 
                RpcDeactivate();
                gameObject.SetActive(false);
            }
        }
    }

    [ClientRpc]
    private void RpcDeactivate()
    {
        gameObject.SetActive(false);
    }
}
