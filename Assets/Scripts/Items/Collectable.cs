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
                InventoryManager inventory = other.GetComponent<InventoryManager>();
                if (inventory)
                {
                    inventory.Add(this);
                    NetworkSetActive(false);
                    gameObject.SetActive(false);
                }
            }
        }

        public void NetworkSetActive(bool active)
        {
            if (isClient)
            {
                CmdSetActive(active);
            }
            else
            {
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