using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(SpringJoint))]
public class PlayerAbilities : NetworkBehaviour
{
    SpringJoint _spring;
    CombatHandler _combatHandler = new CombatHandler();

    bool _holding;
    float _scrollBy;

    enum ControlMode { Combat, Building }

    ControlMode _currentMode = ControlMode.Combat;

    private void Start()
    {
        _spring = GetComponent<SpringJoint>();
    }

    void Update()
    {
        if (!isLocalPlayer) return;
        switch(_currentMode)
        {
            case ControlMode.Building:
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
            case ControlMode.Combat:
                {
                    _combatHandler.HandleCombat();
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
