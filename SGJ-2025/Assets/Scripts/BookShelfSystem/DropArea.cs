using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropArea : MonoBehaviour, IBookDropArea
{
    public BookBehaviour bookBehaviour;

    [SerializeField] DropArea left;
    [SerializeField] DropArea right;

    void Start()
    {
        bookBehaviour = gameObject.GetComponent<BookBehaviour>();
    }

    public void OnBookDrop(BookBehaviour book)
    {
        book.transform.position = transform.position;
    }
}
