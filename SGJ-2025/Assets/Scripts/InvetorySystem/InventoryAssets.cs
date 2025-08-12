using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableOject", menuName = "Inventory/New Item")]
public class InventoryAssets : ScriptableObject
{
    public string itemName;
    [TextArea]
    public string description;
    public Sprite icon;

    public InventoryAssets(string name, string description, Sprite icon)
    {
        itemName = name;
        this.description = description;
        this.icon = icon;
    }
}
