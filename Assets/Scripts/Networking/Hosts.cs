using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Underlunchers.Networking
{
    [CreateAssetMenu(fileName = "Hosts", menuName = "Hosts")]
    public class Hosts : ScriptableObject
    {
        [SerializeField] private string _storyManagement;
        public string StoryManagement => _storyManagement;
        
        [SerializeField] private string _storyBucket;
        public string StoryBucket => _storyBucket;
    }
}