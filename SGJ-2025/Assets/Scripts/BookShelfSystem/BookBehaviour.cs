using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BookBehaviour : MonoBehaviour
{
    //Haykou, se voce está lendo esse codigo para aprender algo, não
    //Va ler outra coisa, o que eu fiz aqui ta uma bosta

    [Header("Private Parameters")]
    [SerializeField] private LayerMask bookDropLayer;
    [SerializeField] private DropArea currentDropArea;
    private Vector3 startDragPos;
    private Vector3 cursorOffset;
    private bool beingDragged;
    private bool canMove = true;

    private void Start()
    {
        if (currentDropArea != null) 
        {
            currentDropArea.OnBookDrop(this);
        }
    }

    private void OnMouseDown()
    {
        if (!canMove) return;

        beingDragged = true;
        startDragPos = transform.position;
        cursorOffset = transform.position - GetMousePositionInWorldSpace();
        if (currentDropArea != null)
            currentDropArea.RemoveBook();
    }

    private void OnMouseDrag()
    {
        transform.position = GetMousePositionInWorldSpace() + cursorOffset;
    }

    public void OnBookDrop(InputAction.CallbackContext context)
    {
        if (context.canceled) 
        {
            if (!beingDragged) return;

            beingDragged = false;

            Collider2D hitCollider = Physics2D.OverlapPoint(transform.position, bookDropLayer);

            if (hitCollider != null)
            {
                currentDropArea = hitCollider.GetComponent<DropArea>();
            }
            else
            {
                transform.position = startDragPos;
            }

            if (currentDropArea != null)
                currentDropArea.OnBookDrop(this);
        }
    }

    public Vector3 GetMousePositionInWorldSpace()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;
        return mousePosition;
    }

    public void SetMovement(bool value) 
    {
        canMove = value;
    }
}
