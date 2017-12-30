using UnityEngine;

public class InputManager : MonoBehaviour {

    public delegate void HotKeyDownHandler(KeyCode key);
    public static event HotKeyDownHandler HotKeyDown;

    public delegate void InventoryToggledHandler();
    public static event InventoryToggledHandler InventoryToggled;

    public static readonly KeyCode[] PossibleHotKeys = { KeyCode.Mouse0 };
    public static readonly KeyCode OpenMenu = KeyCode.I;
    public static readonly KeyCode JumpKey = KeyCode.Space;
    
    private void Update()
    {
        if (Input.anyKeyDown)
        {
            foreach(KeyCode key in PossibleHotKeys)
            {
                if (Input.GetKeyDown(key))
                {
                    if (HotKeyDown != null) HotKeyDown(key);
                    
                }
            }
            if (Input.GetKeyDown(OpenMenu))
            {
                if (InventoryToggled != null) InventoryToggled();
            }
        }
    }

    public static float GetAxis(string name)
    {
        return Input.GetAxis(name);
    }

    public static bool Jump()
    {
        return Input.GetKeyDown(JumpKey);
    }
}
