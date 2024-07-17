using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public string ItemName;
    public bool playerInRange;

    public string GetItemName()
    {
        return ItemName;
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Mouse0) && playerInRange && SelectionManager.Instance.onTarget && SelectionManager.Instance.selectedObject == gameObject) {
            if(InventorySystem.Instance.checkSlotsAvailable(1)){
                Destroy(gameObject);
                InventorySystem.Instance.AddToInventory(ItemName);
            }else
                Debug.Log("Inventory is full");
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")){
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("Player")){
            playerInRange = false;
        }
    }
}