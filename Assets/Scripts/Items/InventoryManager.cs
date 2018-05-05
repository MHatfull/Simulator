using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace Underlunchers.Items
{
    public class InventoryManager : NetworkBehaviour
    {
        List<Collectable> _inventory = new List<Collectable>();

        public int Length { get { return _inventory.Count; } }

        public delegate void InventoryUpdatedHandler();
        public event InventoryUpdatedHandler InventoryUpdated;

        private void IssueInventoryUpdated()
        {
            if(InventoryUpdated != null)
            {
                InventoryUpdated();
            }
        }

        public void Add(Collectable collectable)
        {
            if (!(isServer && isLocalPlayer))
            {
                if (isServer) RpcAdd(collectable.netId);
                else if (isClient) CmdAdd(collectable.netId);
            }
            else
            {
                RpcAdd(collectable.netId);
            }
            LocalAdd(collectable);
        }

        [Command]
        private void CmdAdd(NetworkInstanceId id)
        {
            Collectable collectable = ClientScene.FindLocalObject(id).GetComponent<Collectable>();
            LocalAdd(collectable);
        }

        [ClientRpc]
        private void RpcAdd(NetworkInstanceId id)
        {
            Collectable collectable = ClientScene.FindLocalObject(id).GetComponent<Collectable>();
            LocalAdd(collectable);
        }

        private void LocalAdd(Collectable collectable)
        {
            _inventory.Add(collectable);
            IssueInventoryUpdated();
        }

        public void Remove(Collectable collectable)
        {
            if (!(isServer && isLocalPlayer))
            {
                if (isServer)      RpcRemove(collectable.netId);
                else if (isClient) CmdRemove(collectable.netId);
            }
            else
            {
                RpcRemove(collectable.netId);
            }
            LocalRemove(collectable);
        }

        [Command]
        private void CmdRemove(NetworkInstanceId id)
        {
            Collectable collectable = ClientScene.FindLocalObject(id).GetComponent<Collectable>();
            LocalRemove(collectable);
        }

        [ClientRpc]
        private void RpcRemove(NetworkInstanceId id)
        {
            Collectable collectable = ClientScene.FindLocalObject(id).GetComponent<Collectable>();
            LocalRemove(collectable);
        }

        private void LocalRemove(Collectable collectable)
        {
            _inventory.Remove(collectable);
            IssueInventoryUpdated();
        }

        public Collectable this[int i]
        {
            get
            {
                return _inventory[i];
            }
        }
    }
}