using UnityEngine;

public class InputManager : MonoBehaviour {

    public delegate void HotKeyDownHandler(KeyCode key);
    public static event HotKeyDownHandler HotKeyDown;

    public delegate void InventoryToggledHandler();
    public static event InventoryToggledHandler InventoryToggled;

    public static readonly KeyCode OpenMenu = KeyCode.I;
    public static readonly KeyCode JumpKey = KeyCode.Space;
    
    enum InputMode { Playing, Menus }
    static InputMode _currentMode = InputMode.Playing;

    private void Start()
    {
        LockCursor(true);
    }

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            switch (_currentMode)
            {
                case InputMode.Playing:
                    foreach (KeyCode key in HotKeyManager.HotKeys)
                    {
                        if (Input.GetKeyDown(key))
                        {
                            if (HotKeyDown != null) HotKeyDown(key);

                        }
                    }
                    break;
                case InputMode.Menus: break;
            }
            if (Input.GetKeyDown(OpenMenu))
            {
                _currentMode = _currentMode == InputMode.Playing ? InputMode.Menus : InputMode.Playing;
                LockCursor(_currentMode == InputMode.Playing);
                if (InventoryToggled != null) InventoryToggled();
            }
        }
    }

    void LockCursor(bool locked)
    {
        Cursor.lockState = locked ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = !locked;
    }

    public static float GetAxis(string name)
    {
        if (_currentMode == InputMode.Playing)
        {
            return Input.GetAxis(name);
        }
        else
        {
            return 0;
        }
    }

    public static bool Jump()
    {
        if (_currentMode == InputMode.Playing)
        {
            return Input.GetKeyDown(JumpKey);
        }
        else
        {
            return false;
        }
    }
}
