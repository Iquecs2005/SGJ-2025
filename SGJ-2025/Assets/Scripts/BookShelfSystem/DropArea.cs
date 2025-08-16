using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropArea : MonoBehaviour, IBookDropArea
{
    //Haykou, se voce está lendo esse codigo para aprender algo, não
    //Va ler outra coisa, o que eu fiz aqui ta uma bosta

    [HideInInspector] public BookBehaviour bookBehaviour;

    private Collider2D dropAreaCollider;
    private BookShelfManager shelfManager;

    private int rowIndex;
    private int columnIndex;

    void Start()
    {
        dropAreaCollider = GetComponent<Collider2D>();
    }

    public void SaveMatrixPos(BookShelfManager shelf, int row, int collumn) 
    {
        shelfManager = shelf;
        rowIndex = row;
        columnIndex = collumn;
    }

    public void OnBookDrop(BookBehaviour book)
    {
        bookBehaviour = book;
        book.transform.position = transform.position;
        dropAreaCollider.enabled = false;

        shelfManager.CheckCombination(book, rowIndex, columnIndex);
    }

    public void RemoveBook() 
    {
        bookBehaviour = null;
        dropAreaCollider.enabled = true;
    }
}
