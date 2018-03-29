using Underlunchers.Characters.Player;
using UnityEngine;
using UnityEngine.Networking;

namespace Underlunchers.Items
{
    [RequireComponent(typeof(NetworkIdentity))]
    [RequireComponent(typeof(Collider))]
    public class Collectable : NetworkBehaviour
    {
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
                    NetworkSetActive(false);
                    gameObject.SetActive(false);
                }
            }
        }

        public void NetworkSetActive(bool active)
        {
            if (isClient)
            {
                Debug.Log("client setting active " + active);
                CmdSetActive(active);
            }
            else
            {
                Debug.Log("server setting active " + active);
                RpcSetActive(active);
            }
        }

        [Command]
        private void CmdSetActive(bool active)
        {
            gameObject.SetActive(active);
            RpcSetActive(active);
        }

        [ClientRpc]
        private void RpcSetActive(bool active)
        {
            gameObject.SetActive(active);
        }
    }
}