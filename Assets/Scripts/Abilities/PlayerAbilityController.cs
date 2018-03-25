using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(PlayerCharacter))]
public class PlayerAbilityController : AbilityController
{
    private PlayerCharacter _owner;

    private new void Awake()
    {
        base.Awake();
        _owner = GetComponent<PlayerCharacter>();
    }

    private void Start()
    {
        if (isLocalPlayer)
        {
            InputManager.HotKeyDown += HotKeyDown;
        }
    }

    private void HotKeyDown(KeyCode code)
    {
        var ability = HotKeyManager.HotKeyMaps.ToList().Find(m => m.Key == code);
        var casting = ability.Ability;
        CmdHandleCombat(casting, Camera.main.transform.position, Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 1)) - Camera.main.transform.position);
    }

    [Command]
    public void CmdHandleCombat(AbilityController.Ability casting, Vector3 focalPoint, Vector3 focalDirection)
    {
        _owner.UpdateFocus(focalPoint, focalDirection);
        AvailableAbilities[casting].PerformAbility(_owner);
    }
}
