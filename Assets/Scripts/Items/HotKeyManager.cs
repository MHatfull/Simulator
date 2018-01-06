using System.Linq;
using UnityEngine;

public class HotKeyManager : MonoBehaviour {
    [System.Serializable]
    public struct HotKeyMapping
    {
        public KeyCode Key;
        public AbilityController.Ability Ability;
        public Sprite Icon;
        public Color Color;
    }

    [SerializeField] private HotKeyMapping[] _hotKeyMaps;

    public static HotKeyMapping[] HotKeyMaps;

    AbilityIcon[] _uiIcons;

    public static KeyCode[] HotKeys { get; private set; }

    [SerializeField] AbilityController _ownAbilities;

    private void Awake()
    {
        HotKeyMaps = _hotKeyMaps;
    }

    private void Start()
    {
        _uiIcons = FindObjectsOfType<AbilityIcon>();
        HotKeys = _uiIcons.Select(icon => icon.Key).ToArray();
        foreach (AbilityIcon icon in _uiIcons)
        {
            HotKeyMapping? mapping = _hotKeyMaps.ToList().Find(m => m.Key == icon.Key);
            if (mapping.HasValue)
            {
                _ownAbilities.AvailableAbilities[mapping.Value.Ability].AbilityCast += icon.ResetLoadingProgress;
                icon.SetIcon(mapping.Value.Icon, mapping.Value.Color);
                icon.SetCooldown(_ownAbilities.AvailableAbilities[mapping.Value.Ability].Cooldown);
            }
        }
    }
}
