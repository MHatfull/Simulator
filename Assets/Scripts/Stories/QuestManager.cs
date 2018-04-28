using System;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace Underlunchers.Stories
{
    public class QuestManager : NetworkBehaviour
    {
        [SerializeField] Quest[] _quests;

        public delegate void OnDescriptionUpdatedHandler(string description);
        public event OnDescriptionUpdatedHandler OnDescriptionUpdated;

        [SyncVar(hook = "DescriptionUpdated")]
        string _description;
        
        void DescriptionUpdated(string description)
        {
            if(OnDescriptionUpdated != null)
            {
                OnDescriptionUpdated(description);
            }
        }

        void Start()
        {
            Debug.Log("starting quests");

            if (!isServer)
            {
                Debug.Log("not server, returning");
                return;
            }
            foreach (Quest q in _quests)
            {
                q.ResetQuest();
            }
            foreach (Quest q in _quests)
            {
                q.SetupTriggers();
                q.QuestUpdated += UpdateQuestLog;
            }

            UpdateQuestLog();
        }

        private void UpdateQuestLog()
        {
            Debug.Log("updating log");
            string log = "Quests:\n";
            foreach (Quest q in _quests)
            {
                if (q.IsActive)
                {
                    log += q.GetDescription();
                }
            }
            _text.text = log;
            _description = log;
        }
    }
}