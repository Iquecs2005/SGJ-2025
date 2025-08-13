using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableOject", menuName = "Inventory/New Item", order = 1)]
public class InventoryAssets : ScriptableObject
{
    public string id;
    public Sprite icon;
    public GameObject prefab;
    [TextArea] public string description;

}
