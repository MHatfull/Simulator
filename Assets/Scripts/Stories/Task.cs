using System;
using UnityEngine;

namespace Underlunchers.Stories
{
    public abstract class Task : ScriptableObject
    {
        public bool IsComplete { get; private set; }

        public delegate void TaskCompleteHandler();
        public event TaskCompleteHandler TaskComplete;

        public delegate void TaskUpdatedHandler();
        public event TaskUpdatedHandler TaskUpdated;

        public virtual void ResetTask()
        {
            TaskComplete = null;
            IsComplete = false;
        }

        protected void CompleteTask()
        {
            IsComplete = true;
            if (TaskComplete != null)
            {
                TaskComplete();
            }
        }

        protected void UpdateTask()
        {
            if( TaskUpdated != null)
            {
                TaskUpdated();
            }
        }

        internal abstract string GetProgress();
    }
}