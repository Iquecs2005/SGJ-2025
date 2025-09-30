using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class InventoryUI : MonoBehaviour
{
    [Header("Inventory Reference")]
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private GameObject inventoryButton;
    [SerializeField] private GameObject itemPanel;
    [SerializeField] private GameObject itemSlotPrefab;
    [SerializeField] private GameObject dragItemPrefab;

    [Header("Events")]
    [SerializeField] private UnityEvent OnOpen;
    [SerializeField] private UnityEvent OnClose;

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

    public void Invetory(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            TogglePanel();
        }
    }

    public void Open()
    {
        inventoryPanel.SetActive(true);
        inventoryButton.SetActive(false);

        OnOpen.Invoke();
    }

    public void Close()
    {
        inventoryPanel.SetActive(false);
        inventoryButton.SetActive(true);

        OnClose.Invoke();
    }

    public void TogglePanel()
    {
        //inventoryPanel.SetActive(!inventoryPanel.activeInHierarchy);
        if (!inventoryPanel.activeInHierarchy)
            Open();
        else
            Close();
    }

    private void AdjustPanelSize()
    {
        inventoryItems = playerInventorySystem.GetItems();

        float sizePerItem = gridLayout.cellSize.x + gridLayout.spacing.x;
        float totalSize = sizePerItem * Mathf.Max(inventoryItems.Count, 1) + gridLayout.padding.left;

        panelRectTransform.sizeDelta = new Vector2(totalSize, panelRectTransform.sizeDelta.y);

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
