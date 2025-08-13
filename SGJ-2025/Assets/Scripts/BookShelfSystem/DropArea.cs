using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropArea : MonoBehaviour, IBookDropArea
{
    public void OnBookDrop(BookBehaviour book)
    {
        book.transform.position = transform.position;
        print("Livro dropado aqui!");
    }
}
