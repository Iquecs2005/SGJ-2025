using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private GameObject itemPanel;
    [SerializeField] private GameObject itemSlotPrefab;
    [SerializeField] private GameObject dragItemPrefab;

    private List<InventoryAssets> inventoryItems;
    private List<GameObject> itemSlots;
    private RectTransform panelRectTransform;
    private GridLayoutGroup gridLayout;
    private GameObject playerRef;
    private InventorySystem playerInventorySystem;
    private GameObject currentDragItem;
    private DragItem dragItemController;
    private int nChild;

    public void Start()
    {
        itemSlots = new();
        nChild = 0;
        gridLayout = itemPanel.GetComponent<GridLayoutGroup>();
        playerRef = GameManager.instance.GetPlayerRef();
        playerInventorySystem = playerRef.GetComponent<InventorySystem>();
        playerInventorySystem.OnInventoryChange.AddListener(AdjustPanelSize);
        panelRectTransform = itemPanel.GetComponent<RectTransform>();
        currentDragItem = Instantiate(dragItemPrefab, playerRef.transform);
        dragItemController = currentDragItem.GetComponent<DragItem>();
        currentDragItem.SetActive(false);
        AdjustPanelSize();
    }

    public void TogglePanel() 
    {
        inventoryPanel.SetActive(!inventoryPanel.activeInHierarchy);
    }

    private void AdjustPanelSize() 
    {
        inventoryItems = playerInventorySystem.GetItems();

        float sizePerItem = gridLayout.cellSize.x + gridLayout.spacing.x;
        float totalSize = sizePerItem * Mathf.Max(inventoryItems.Count, 1) + gridLayout.padding.left;

        panelRectTransform.sizeDelta =  new Vector2(totalSize, panelRectTransform.sizeDelta.y);

        AdjustChildren();
    }

    private void AdjustChildren() 
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

            currentSlot.GetComponent<InventorySlot>().Initialization(this, i, currentItem.icon);
        }
    }

    public void OnItemSlotClick(int slotId) 
    {
        InventoryAssets currentItem = inventoryItems[slotId];

        dragItemController.SetCurrentItem(currentItem);
        currentDragItem.SetActive(true);
    }
}
