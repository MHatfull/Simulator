using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpringJoint))]
public class Collector : MonoBehaviour
{
    bool _holding;
    SpringJoint _spring;
    float _scrollBy;

    [SerializeField]
    Collectable _cube;
    [SerializeField]
    Collectable _sphere;
    [SerializeField]
    Collectable _moo;

    private void Start()
    {
        _spring = GetComponent<SpringJoint>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if(!_holding)
            {
                Debug.Log("grab");
                Raycast();
            }
            else
            {
                Debug.Log("let go");
                _holding = false;
                _spring.connectedBody = null;
            }
        }
        _scrollBy = Input.GetAxis("Mouse ScrollWheel");
        if(_scrollBy != 0)
        {
            _spring.anchor += new Vector3(0, 0, _scrollBy);
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            FireObject(_cube);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            FireObject(_sphere);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            FireObject(_moo);
        }
    }

    void FireObject(Collectable prefab)
    {
        var newObject = Instantiate(prefab);
        newObject.transform.position = transform.TransformPoint(_spring.anchor);
        newObject.Rigidbody.velocity = transform.TransformPoint(Vector3.forward * 10) - transform.position;
    }

    void Raycast()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            Transform objectHit = hit.transform;
            Debug.Log("hit " + objectHit.name);
            if(objectHit.GetComponent<Collectable>() != null)
            {
                Debug.Log("grabbing " + objectHit.name);
                _holding = true;
                _spring.connectedBody = objectHit.GetComponent<Rigidbody>();
            }
        }
    }

}
