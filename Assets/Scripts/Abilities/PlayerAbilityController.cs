using System.Linq;
using UnityEngine;

public class PlayerAbilityController : AbilityController
{
    private Transform _owner;
    Character _self;

    private new void Awake()
    {
        base.Awake();
        _self = GetComponent<Character>();
    }

    private void Start()
    {
        InputManager.HotKeyDown += HandleCombat;
    }

    public void HandleCombat(KeyCode hotkey)
    {
        var ability = HotKeyManager.HotKeyMaps.ToList().Find(m => m.Key == hotkey);
        var casting = AvailableAbilities[ability.Ability];
        casting.PerformAbility(_self);
    }
}
