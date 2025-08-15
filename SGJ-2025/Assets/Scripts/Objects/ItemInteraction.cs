using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ItemInteraction : MonoBehaviour
{
    [SerializeField] private InventoryAssets correctItem;
    [SerializeField] private bool removeItem;
    [SerializeField] private bool oneTime;
    [SerializeField] private UnityEvent OnRightItemEvent;

    public void OnItemInteraction(InventoryAssets attemptedItem) 
    {
        if (correctItem == attemptedItem) 
        {
            if (removeItem) 
            {
                GameObject playerObject = GameManager.instance.GetPlayerRef();
                InventorySystem inventorySystem = playerObject.GetComponent<InventorySystem>();
                inventorySystem.RemoveItem(correctItem);
            }
            if (oneTime) 
            {
                Destroy(gameObject);
            }
            OnRightItemEvent.Invoke();
        }
    }
}
