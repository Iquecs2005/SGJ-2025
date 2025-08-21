using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [SerializeField] private Image itemSprite;

    private InventoryUI inventoryUI;
    private int inventorySlotID;

    public void Initialization(InventoryUI inventoryUIRef, int inventoryId, Sprite desiredItemSprite) 
    {
        inventoryUI = inventoryUIRef;
        inventorySlotID = inventoryId;
        itemSprite.sprite = desiredItemSprite;
    }

    public void OnItemSlotClick() 
    {
        inventoryUI.OnItemSlotClick(inventorySlotID);
    }
}
