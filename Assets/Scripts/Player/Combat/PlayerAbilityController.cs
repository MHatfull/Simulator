using UnityEngine;

[RequireComponent(typeof(HotKeyManager))] 
public class PlayerAbilityController : AbilityController
{
    private Transform _owner;
    private HotKeyManager _hotKeyManager;

    private new void Awake()
    {
        base.Awake();
        _hotKeyManager = GetComponent<HotKeyManager>();
    }

    private void Update()
    {
        HandleCombat();
    }

    public void HandleCombat()
    {
        if (Input.anyKeyDown)
        {
            foreach (var mapping in _hotKeyManager.HotKeyMaps)
            {
                if (Input.GetKeyDown(mapping.Key))
                {
                    var casting = _availableAbilities[mapping.Ability];
                    casting.PerformAbility(transform);
                }
            }
        }
    }
}
