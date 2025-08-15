using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragItem : MonoBehaviour
{
    [SerializeField] private float normalAlpha;
    
    private SpriteRenderer spriteRenderer;
    private PlayerInputController inputController;
    private InventoryAssets currentItem;

    private ItemInteraction currentItemInteraction;


    public void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        inputController = GetComponentInParent<PlayerInputController>();
        inputController.OnMouseUpEvent.AddListener(DeactivateDragItem);
    }

    private void DeactivateDragItem()
    {
        if (currentItemInteraction != null) 
        {
            currentItemInteraction.OnItemInteraction(currentItem);
        }
        gameObject.SetActive(false);
    }

    public void SetCurrentItem(InventoryAssets itemData)
    {
        currentItem = itemData;

        spriteRenderer.sprite = currentItem.icon;
        spriteRenderer.color = new Color(255,255,255, normalAlpha);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        spriteRenderer.color = new Color(255, 255, 255, 1);
        currentItemInteraction = collision.gameObject.GetComponent<ItemInteraction>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        spriteRenderer.color = new Color(255, 255, 255, normalAlpha);
        currentItemInteraction = null; 
    }
}
