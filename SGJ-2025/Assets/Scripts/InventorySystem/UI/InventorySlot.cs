using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlot : MonoBehaviour
{
    private InventoryUI inventoryUI;
    private int inventorySlotID;

    public void Initialization(InventoryUI inventoryUIRef, int inventoryId) 
    {
        inventoryUI = inventoryUIRef;
        inventorySlotID = inventoryId;
    }

    public void OnItemSlotClick() 
    {
        inventoryUI.OnItemSlotClick(inventorySlotID);
    }
}
