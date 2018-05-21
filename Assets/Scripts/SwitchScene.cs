using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SwitchScene : MonoBehaviour {
    [SerializeField] string _sceneName;
    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(delegate { SceneManager.LoadScene(_sceneName); });
    }
}
