using UnityEngine;

public class HotKeyManager : MonoBehaviour {
    [System.Serializable]
    public struct HotKeyMapping
    {
        public KeyCode Key;
        public AbilityController.Ability Ability;
    }

    public HotKeyMapping[] HotKeyMaps;
}
