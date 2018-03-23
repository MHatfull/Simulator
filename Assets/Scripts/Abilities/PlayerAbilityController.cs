using System.Linq;
using UnityEngine;

public class PlayerAbilityController : AbilityController
{
    private Transform _owner;

    private new void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        if (isLocalPlayer)
        {
            InputManager.HotKeyDown += HandleCombat;
        }
    }

    [Command]
    public void HandleCombat(KeyCode hotkey)
    {
        Debug.Log("handling combat for " + gameObject.name);
        var ability = HotKeyManager.HotKeyMaps.ToList().Find(m => m.Key == hotkey);
        var casting = AvailableAbilities[ability.Ability];
        casting.PerformAbility(PlayerCharacter.Me);
    }
}
