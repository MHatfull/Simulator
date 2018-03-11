using System.Linq;
using UnityEngine;

public class HotKeyManager : MonoBehaviour {
    [System.Serializable]
    public struct HotKeyMapping
    {
        public KeyCode Key;
        public AbilityController.Ability Ability;
        public Sprite Icon;
    }

    [SerializeField] private HotKeyMapping[] _hotKeyMaps;

    public static HotKeyMapping[] HotKeyMaps;

    AbilityIcon[] _uiIcons;

    public static KeyCode[] HotKeys { get; private set; }

    private void Awake()
    {
        HotKeyMaps = _hotKeyMaps;
        HotKeys = new KeyCode[0];
    }

    public void OnConnect(AbilityController ownAbilities)
    {
        _uiIcons = FindObjectsOfType<AbilityIcon>();
        HotKeys = _uiIcons.Select(icon => icon.Key).ToArray();
        foreach (AbilityIcon icon in _uiIcons)
        {
            HotKeyMapping? mapping = _hotKeyMaps.ToList().Find(m => m.Key == icon.Key);
            if (mapping.HasValue)
            {
                ownAbilities.AvailableAbilities[mapping.Value.Ability].AbilityCast += icon.ResetLoadingProgress;
                icon.SetIcon(mapping.Value.Icon);
                icon.SetCooldown(ownAbilities.AvailableAbilities[mapping.Value.Ability].Cooldown);
            }
        }
    }
}
