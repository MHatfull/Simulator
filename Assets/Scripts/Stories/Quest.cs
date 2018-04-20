using System;
using System.Linq;
using UnityEngine;

namespace Underlunchers.Stories
{
    [CreateAssetMenu(fileName = "Quest", menuName = "Stories/Quest")]
    public class Quest : ScriptableObject
    {
        [SerializeField] private Quest _trigger;
        [SerializeField] private Task[] _tasks;

        public delegate void QuestCompleteHandler();
        public event QuestCompleteHandler QuestComplete;

        public delegate void QuestUpdatedHandler();
        public event QuestUpdatedHandler QuestUpdated;

        public bool IsActive { get; private set; }

        public void ResetQuest()
        {
            Debug.Log("deactivating quest");
            IsActive = false;
            _trigger = null;
            foreach(Task t in _tasks)
            {
                t.ResetTask();
            }
        }

        internal string GetDescription()
        {
            string progress = "Kill me some spheres please!";
            foreach (Task t in _tasks)
            {
                progress += "\n - " + t.GetProgress(); 
            }
            return progress;
        }

        public void SetupTriggers()
        {
            Debug.Log("setting up triggers");
            if (_trigger != null)
            {
                _trigger.QuestComplete += StartQuest;
            }
            else
            {
                Debug.Log("no triggers, starting right away");
                StartQuest();
            }
        }

        private void StartQuest()
        {
            IsActive = true;
            foreach (Task t in _tasks)
            {
                t.TaskComplete += CheckQuestComplete;
                t.TaskUpdated += UpdateQuest;
            }
            CheckQuestComplete();
        }

        private void UpdateQuest()
        {
            if (QuestUpdated != null)
            {
                QuestUpdated();
            }
        }

        private void CheckQuestComplete()
        {
            if(_tasks.Count(t => t.IsComplete) == _tasks.Length)
            {
                IsActive = false;
                if(QuestComplete != null)
                {
                    QuestComplete();
                }
            }
        }
    }
}
