using System;
using UnityEngine;
using UnityEngine.UI;

namespace Underlunchers.Stories
{
    [RequireComponent(typeof(Text))]
    public class QuestManager : MonoBehaviour
    {
        [SerializeField] Quest[] _quests;

        Text _text;

        void Start()
        {
            foreach (Quest q in _quests)
            {
                q.ResetQuest();
            }
            foreach (Quest q in _quests)
            {
                q.SetupTriggers();
                q.QuestUpdated += UpdateQuestLog;
            }

            _text = GetComponent<Text>();
            UpdateQuestLog();
        }

        private void UpdateQuestLog()
        {
            string log = "Quests:\n";
            foreach (Quest q in _quests)
            {
                if (q.IsActive)
                {
                    log += q.GetDescription();
                }
            }
            _text.text = log;
        }
    }
}