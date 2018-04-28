using System.Collections;
using System.Collections.Generic;
using Underlunchers.Stories;
using UnityEngine;
using UnityEngine.UI;

namespace Underlunchers
{
    [RequireComponent(typeof(Text))]
    public class QuestUI : MonoBehaviour
    {
        Text _text;

        void UpdateText(string description)
        {
            _text.text = description;
        }

        private void Start()
        {
            _text = GetComponent<Text>();
            QuestManager man = FindObjectOfType<QuestManager>();
            man.OnDescriptionUpdated += UpdateText;        }
    }
}