using System.Linq;
using UnityEngine;

[RequireComponent(typeof(HotKeyManager))] 
public class PlayerAbilityController : AbilityController
{
    private Transform _owner;
    private HotKeyManager _hotKeyManager;
    Character _self;

    private new void Awake()
    {
        base.Awake();
        _hotKeyManager = GetComponent<HotKeyManager>();
        _self = GetComponent<Character>();
    }

    private void Start()
    {
        InputManager.HotKeyDown += HandleCombat;
    }

    public void HandleCombat(KeyCode hotkey)
    {
        var ability = _hotKeyManager.HotKeyMaps.ToList().Find(m => m.Key == hotkey);
        var casting = AvailableAbilities[ability.Ability];
        casting.PerformAbility(_self);
    }
}
