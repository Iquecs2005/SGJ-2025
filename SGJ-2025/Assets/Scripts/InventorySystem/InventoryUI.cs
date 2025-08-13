using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private GameObject itemPanel;
    [SerializeField] private GameObject itemSlotPrefab;

    private List<GameObject> itemSlots;
    private RectTransform panelRectTransform;
    private GridLayoutGroup gridLayout;
    private InventorySystem playerInventorySystem;
    private int nChild;

    public void Start()
    {
        itemSlots = new();
        nChild = 0;
        gridLayout = itemPanel.GetComponent<GridLayoutGroup>();
        playerInventorySystem = GameManager.instance.GetPlayerRef().GetComponent<InventorySystem>();
        playerInventorySystem.OnInventoryChange.AddListener(AdjustPanelSize);
        panelRectTransform = itemPanel.GetComponent<RectTransform>();
        AdjustPanelSize();
    }

    public void TogglePanel() 
    {
        inventoryPanel.SetActive(!inventoryPanel.activeInHierarchy);
    }

    private void AdjustPanelSize() 
    {
        List<InventoryAssets> inventoryItems = playerInventorySystem.GetItems();

        float sizePerItem = gridLayout.cellSize.x + gridLayout.spacing.x;
        float totalSize = sizePerItem * Mathf.Max(inventoryItems.Count, 1) + gridLayout.padding.left;

        panelRectTransform.sizeDelta =  new Vector2(totalSize, panelRectTransform.sizeDelta.y);

        AdjustChildren(ref inventoryItems);
    }

    private void AdjustChildren(ref List<InventoryAssets> inventoryItems) 
    {
        int nItems = inventoryItems.Count;

        while (nChild < nItems)
        {
            GameObject newItemSlot = Instantiate(itemSlotPrefab, itemPanel.transform);
            itemSlots.Add(newItemSlot);
            nChild++;
        }
        while (nChild > nItems)
        {
            Destroy(itemSlots[0]);
            itemSlots.RemoveAt(0);
            nChild--;
        }

        for (int i = 0; i < nItems; i++)
        {
            InventoryAssets currentItem = inventoryItems[i];
            GameObject currentSlot = itemSlots[i];

            currentSlot.GetComponent<Image>().sprite = currentItem.icon;
        }
    }
}
