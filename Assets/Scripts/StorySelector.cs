using UnityEngine;
using UnityEngine.UI;

namespace Underlunchers.Stories
{
    [RequireComponent(typeof(Dropdown))]
    public class StorySelector : MonoBehaviour
    {
        public static string CurrentStory { get; private set; }

        private Dropdown _dropdown;

        private void Awake()
        {
            _dropdown = GetComponent<Dropdown>();
            _dropdown.onValueChanged.AddListener(delegate { UpdateStory(); });
        }

        private void UpdateStory()
        {
            Debug.Log("setting story!");
            CurrentStory = _dropdown.options[_dropdown.value].text;
        }
    }
}