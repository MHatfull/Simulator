using System;
using Underlunchers.Characters;
using UnityEngine;

namespace Underlunchers.Stories
{
    [CreateAssetMenu(fileName = "Kill", menuName = "Stories/Tasks/Kill")]
    public class KillTask : Task
    {
        [SerializeField] Character _toKill;
        [SerializeField] int _numberToKill;

        private int _progress;

        public override void ResetTask()
        {
            base.ResetTask();
            _progress = 0;
            Character.OnGlobalDeath += CheckKill;
        }

        internal override string GetProgress()
        {
            return "Killed " + _progress + "/" + _numberToKill;
        }

        private void CheckKill(Character character)
        {
            if (character.SameType(_toKill))
            {
                _progress++;
                CheckProgress();
                UpdateTask();
            }
        }

        private void CheckProgress()
        {
            if(_progress == _numberToKill)
            {
                CompleteTask();
            }
        }
    }
}