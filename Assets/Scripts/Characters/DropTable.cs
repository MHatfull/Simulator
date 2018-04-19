using Underlunchers.Items;
using UnityEngine;
using UnityEngine.Networking;

namespace Underlunchers.Characters
{
    [RequireComponent(typeof(Character))]
    public class DropTable : NetworkBehaviour
    {
        Character _self;

        [SerializeField] Drop[] _drops;

        [System.Serializable]
        struct Drop
        {
            [SerializeField] [Range(0, 1)] private float _dropChance;
            public float DropChance { get { return _dropChance; } }
            [SerializeField] Collectable _toDrop;
            public Collectable ToDrop { get { return _toDrop; } }
        }

        void Start()
        {
            if (isServer)
            {
                _self = GetComponent<Character>();
                _self.OnDeath += DropItems;
            }
        }

        [Server]
        private void DropItems()
        {
            Debug.Log("dropping loots");
            foreach(Drop drop in _drops)
            {
                if (Random.Range(0f, 1f) < drop.DropChance)
                {
                    GameObject toDrop = Instantiate(drop.ToDrop).gameObject;
                    toDrop.transform.position = transform.position;
                    NetworkServer.Spawn(toDrop);
                }
            }
        }
    }
}