using Simulator.Characters.Player;
using UnityEngine;

namespace Simulator.Items
{
    [RequireComponent(typeof(Collider))]
    public class Collectable : MonoBehaviour
    {
        public Sprite Icon;

        protected virtual void Awake()
        {
            GetComponent<Collider>().isTrigger = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.transform == PlayerCharacter.Me.transform)
            {
                InventoryManager.AddToInventory(this);
                gameObject.SetActive(false);
            }
        }
    }
}