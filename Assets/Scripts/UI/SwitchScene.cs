using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Underlunchers.Scene
{
    public class SwitchScene : MonoBehaviour
    {
        [SerializeField] private string _sceneName;

        private void Awake()
        {
            GetComponent<Button>().onClick.AddListener(delegate { SceneManager.LoadScene(_sceneName); });
        }
    }
}