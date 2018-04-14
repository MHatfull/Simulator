using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Underlunchers.UI
{
    public class MinimapIcon : MonoBehaviour
    {
        [SerializeField] private Sprite _miniMapTexture;

        private void Start()
        {
            var minimapIcon = new GameObject();
            minimapIcon.transform.parent = transform;
            minimapIcon.transform.eulerAngles = new Vector3(90, 0, 0);
            minimapIcon.transform.localPosition = new Vector3(0, 10, 0);
            minimapIcon.layer = LayerMask.NameToLayer("MiniMap");
            var minimapSprite = minimapIcon.AddComponent<SpriteRenderer>();
            minimapSprite.sprite = _miniMapTexture;
        }
    }
}
