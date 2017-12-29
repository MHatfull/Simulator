using System.Linq;
using UnityEngine;

[RequireComponent(typeof(AbilityController))]
public class HotKeyManager : MonoBehaviour {
    [System.Serializable]
    public struct HotKeyMapping
    {
        public KeyCode Key;
        public AbilityController.Ability Ability;
        public Sprite Icon;
    }

    public HotKeyMapping[] HotKeyMaps;

    [SerializeField] AbilityIcon[] UIIcons;

    private void Start()
    {
        var abilityController = GetComponent<AbilityController>();
        foreach (AbilityIcon icon in UIIcons)
        {
            HotKeyMapping? mapping = HotKeyMaps.ToList().Find(m => m.Key == icon.Key);
            if (mapping.HasValue)
            {
                abilityController.AvailableAbilities[mapping.Value.Ability].AbilityCast += icon.ResetLoadingProgress;
                icon.SetIcon(mapping.Value.Icon);
                icon.SetCooldown(abilityController.AvailableAbilities[mapping.Value.Ability].Cooldown);
            }
        }
    }
}
