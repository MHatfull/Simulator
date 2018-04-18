using Underlunchers.Characters.Abilities;
using Underlunchers.Items.Equipment;
using Underlunchers.UI;
using UnityEngine;
using UnityEngine.Networking;

namespace Underlunchers.Characters
{
    [RequireComponent(typeof(HealthDisplay))]
    [RequireComponent(typeof(AbilityController))]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(MinimapIcon))]
    [RequireComponent(typeof(EquipmentManager))]
    public class Character : NetworkBehaviour
    {
        public delegate void OnSelfJoinedHandler(Character me);
        public static event OnSelfJoinedHandler OnSelfJoined;

        public delegate void OnDeathHandler();
        public event OnDeathHandler OnDeath;

        private Animator _animator;
        private HealthDisplay _healthDisplay;
        private EquipmentManager _equipmentManager;


        public float MaxHealth
        {
            get { return _maxHealth; }
            protected set { _maxHealth = value; _healthDisplay.MaxHealth = value; }
        }
        [SerializeField] protected float _maxHealth;

        public float CurrentHealth
        {
            get { return _currentHealth; }
            protected set { _currentHealth = value; OnHealthChanged(value); }
        }

        [SyncVar(hook = "OnHealthChanged")]
        protected float _currentHealth;

        private void OnHealthChanged(float value)
        {
            _healthDisplay.CurrentHealth = value;
        }

        [SerializeField] private Transform _focus;

        public Transform Focus { get { return _focus; } }

        internal float DamageBonus()
        {
            return _equipmentManager.Damage();
        }
        protected virtual void Awake()
        {
            _animator = GetComponent<Animator>();
            _healthDisplay = GetComponent<HealthDisplay>();
            _equipmentManager = GetComponent<EquipmentManager>();
        }

        public void Start()
        {
            _healthDisplay.MaxHealth = _maxHealth;
            CurrentHealth = MaxHealth;
            if (isLocalPlayer && OnSelfJoined != null)
            {
                OnSelfJoined(this);
            }
        }

        public virtual void Die()
        {
            if (OnDeath != null) OnDeath();
        }

        public void DealDamage(float damage, Character source)
        {
            RpcDealDamage();
            CurrentHealth -= Mathf.Clamp(damage - _equipmentManager.DamageReduction(), 0, Mathf.Infinity);
            CheckForDeath();
        }

        [ClientRpc]
        void RpcDealDamage()
        {
            _animator.SetTrigger("TakeDamage");
        }

        private void CheckForDeath()
        {
            if (CurrentHealth <= 0)
            {
                Die();
                NetworkServer.Destroy(gameObject);
            }
        }
    }
}