using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Hosts", menuName = "Hosts")]
public class Hosts : ScriptableObject
{
    [SerializeField] private string _storyManagement;
    public string StoryManagement
    {
        get { return _storyManagement; }
    }
}
