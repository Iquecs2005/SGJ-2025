using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BookDirection
{
    none,
    dir,
    esq
}

public class BookBehaviour : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] GameObject desireBook;


    [Header("Book Direction")]
    public BookDirection bookDirection;


    [Header("Private Parameters")]
    private Collider2D bookCollider;
    private Vector3 startDragPos;


    [Header("Test")]
    [SerializeField] bool bookCheck;


    private void Start()
    {
        bookCollider = GetComponent<Collider2D>();
    }

    private void OnMouseDown()
    {
        startDragPos = transform.position;
        transform.position = GetMousePositionInWorldSpace();
    }

    private void OnMouseDrag()
    {
        transform.position = GetMousePositionInWorldSpace();
    }

    private void OnMouseUp()
    {
        bookCollider.enabled = false;
        Collider2D hitCollider = Physics2D.OverlapPoint(transform.position);
        bookCollider.enabled = true;
        if (hitCollider != null && hitCollider.TryGetComponent(out IBookDropArea bookDropArea))
        {
            bookDropArea.OnBookDrop(this);
        }
        else
        {
            transform.position = startDragPos;
        }
    }

    public Vector3 GetMousePositionInWorldSpace()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;
        return mousePosition;
    }
}
