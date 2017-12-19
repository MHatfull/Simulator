using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(SpringJoint))]
[RequireComponent(typeof(ItemThrower))]
public class PlayerAbilities : NetworkBehaviour
{
    SpringJoint _spring;
    ItemThrower _itemThrower;

    bool _holding;
    float _scrollBy;

    enum ControlMode { Throwing, Collecting }

    ControlMode _currentMode = ControlMode.Throwing;

    private void Start()
    {
        _spring = GetComponent<SpringJoint>();
        _itemThrower = GetComponent<ItemThrower>();
    }

    void Update()
    {
        if (!isLocalPlayer) return;
        switch(_currentMode)
        {
            case ControlMode.Collecting:
                {
                    if (Input.GetKeyDown(KeyCode.Mouse0))
                    {
                        if (!_holding)
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
                    if (_scrollBy != 0)
                    {
                        _spring.anchor += new Vector3(0, 0, _scrollBy);
                    }
                    break;
                }
            case ControlMode.Throwing:
                {
                    _itemThrower.ThrowItem();
                    break;
                }
        }
    }

    void Raycast()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            Transform objectHit = hit.transform;
            if(objectHit.GetComponent<Collectable>() != null)
            {
                _holding = true;
                _spring.connectedBody = objectHit.GetComponent<Rigidbody>();
            }
        }
    }

}
