using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpringJoint))]
public class ItemThrower : MonoBehaviour
{
    SpringJoint _spring;

    [SerializeField] List<KeyCode> _throwingKeys;
    [SerializeField] List<Collectable> _collectables;

    public void Awake()
    {
        _spring = GetComponent<SpringJoint>();
        if(_throwingKeys.Count != _collectables.Count)
        {
            throw new System.Exception("You must have the same number of controls as collectables!");
        }
    }

    public void ThrowItem()
    {
        if (Input.anyKeyDown)
        {
            foreach (KeyCode key in _throwingKeys)
            {
                if (Input.GetKeyDown(key))
                {
                    FireObject(_collectables[_throwingKeys.IndexOf(key)]);
                }
            }
        }
    }

    void FireObject(Collectable prefab)
    {
        var newObject = Instantiate(prefab);
        newObject.transform.position = transform.TransformPoint(_spring.anchor);
        newObject.Rigidbody.velocity = transform.TransformPoint(Vector3.forward * 10) - transform.position;
    }
}
