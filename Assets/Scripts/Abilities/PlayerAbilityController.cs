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
        CmdHandleCombat(code, Camera.main.transform.position, Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 1)) - Camera.main.transform.position);
        Debug.Log("should be on client");
    }

    [Command]
    public void CmdHandleCombat(KeyCode hotkey, Vector3 focalPoint, Vector3 focalDirection)
    {
        Debug.Log("server handling combat for " + gameObject.name);
        _owner.UpdateFocus(focalPoint, focalDirection);
        var ability = HotKeyManager.HotKeyMaps.ToList().Find(m => m.Key == hotkey);
        var casting = AvailableAbilities[ability.Ability];
        casting.PerformAbility(PlayerCharacter.Me);
    }
}
