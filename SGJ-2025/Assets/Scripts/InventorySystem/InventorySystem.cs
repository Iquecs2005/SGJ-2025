using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// [RequireComponent(typeof(Collider2D))]
public class InventorySystem : MonoBehaviour
{
    // [Header("References")]
    // [SerializeField] InventoryUI ui;
    // [SerializeField] AudioSource audioSource; -> PARTE DE AUDIO VER COM IQUE

    // [Header("Prefabs")]
    // [SerializeField] GameObject droppedItemPrefab;

    // [Header("Audio Clips")] -> PARTE DE AUDIO VER COM IQUE
    // [SerializeField] AudioClip pickUpItemAudio;
    // [SerializeField] AudioClip droppItemAudio;

    // [Header("State")]
    [SerializeField] private Dictionary<string, InventoryAssets> inventoryDictionary = new();
    public UnityEvent OnInventoryChange;

    //public void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("DroppedItem"))
    //    {
    //        Debug.Log("zap");

    //        // var droppedItem = collision.GetComponent<DroppedItem>();
    //        // if (droppedItem.pickedUp)
    //        // {
    //        //     return;
    //        // }
    //        // droppedItem.pickedUp = true;
    //        // AddItem(droppedItem.item);
    //        // Destroy(collision.gameObject);
    //        // AudioSource.PlayOneShot(pickUpItemAudio);
    //    }
    //}

    public void AddItem(InventoryAssets item)
    {
        if (!inventoryDictionary.TryAdd(item.id, item)) 
        {
            print("Already has item");
            return;
        }

        OnInventoryChange.Invoke();
        // ui.AddUIItem(inventoryId, item);
    }

    public void RemoveItem(InventoryAssets item)
    {
        RemoveItem(item.id);
    }

    public void RemoveItem(string key) 
    {
        if (!inventoryDictionary.Remove(key)) 
        {
            print("Player doesnt have this item");
            return;
        }

        OnInventoryChange.Invoke();
    }

    public List<InventoryAssets> GetItems() 
    {
        List<InventoryAssets> inventoryItems = new();

        foreach (var item in inventoryDictionary)
        {
            inventoryItems.Add(item.Value);
        }

        return inventoryItems;
    }

    // public void DropItem(string inventoryId)
    // {
    //     var droppedItem = Instantiate(droppedItemPrefab, transform.position, Quaternion.identity).GetComponent<DroppedItem>();
    //     var item = inventory.GetValueOrDefault(inventoryId);
    //     droppedItem.Initialize(item);
    //     inventory.Remove(inventoryId);
    //     // ui.RemoveUIItem(inventoryId);
    //     // AudioSource.PlayOneShot(droppItemAudio);
    // }
}
