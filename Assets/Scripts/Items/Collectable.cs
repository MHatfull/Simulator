using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Collectable : MonoBehaviour {
    public Sprite Icon;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform == PlayerCharacter.Me.transform)
        {
            InventoryManager.AddToInventory(this);
            gameObject.SetActive(false);
        }
    }
}
