using UnityEngine;

[RequireComponent(typeof(AbilityController))]
public class HotKeyManager : MonoBehaviour {
    [System.Serializable]
    public struct HotKeyMapping
    {
        public KeyCode Key;
        public AbilityController.Ability Ability;
        public AbilityIcon UIIcon;
        public Sprite Icon;
    }

    public HotKeyMapping[] HotKeyMaps;

    private void Start()
    {
        var abilityController = GetComponent<AbilityController>();
        foreach(HotKeyMapping map in HotKeyMaps)
        {
            abilityController.AvailableAbilities[map.Ability].AbilityCast += map.UIIcon.ResetLoadingProgress;
            map.UIIcon.SetIcon(map.Icon);
            map.UIIcon.SetCooldown(abilityController.AvailableAbilities[map.Ability].Cooldown);
        }
    }
}
