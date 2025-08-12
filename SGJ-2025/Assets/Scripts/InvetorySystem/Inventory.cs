using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

// [RequireComponent(typeof(Collider2D))]
public class Inventory : MonoBehaviour
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
    // [SerializeField] SerializedDictionary<string, InventoryAssets> inventory = new();

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("DroppedItem"))
        {
            Debug.Log("zap");

            // var droppedItem = collision.GetComponent<DroppedItem>();
            // if (droppedItem.pickedUp)
            // {
            //     return;
            // }
            // droppedItem.pickedUp = true;
            // AddItem(droppedItem.item);
            // Destroy(collision.gameObject);
            // AudioSource.PlayOneShot(pickUpItemAudio);
        }
    }

    // void AddItem(InventoryAssets item)
    // {
    //     var inventoryId = Guid.NewGuid().ToString();
    //     inventory.Add(inventoryId, item);
    //     // ui.AddUIItem(inventoryId, item);
    // }

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
