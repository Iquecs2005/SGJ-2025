using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BookShelfManager : MonoBehaviour
{
    //Haykou, se voce está lendo esse codigo para aprender algo, não
    //Va ler outra coisa, o que eu fiz aqui ta uma bosta

    [SerializeField] private DropAreaLine[] dropAreaMatrix;

    [SerializeField] private BookBehaviour correctLeftBook;
    [SerializeField] private BookBehaviour correctRightBook;

    [Header("Events")]
    [SerializeField] private UnityEvent OnCorrectBooks;

    private void Awake()
    {
        for (int i = 0; i < dropAreaMatrix.Length; i++)
        {
            DropArea[] bookArray = dropAreaMatrix[i].books;
            for (int j = 0; j < bookArray.Length; j++)
            {
                DropArea book = bookArray[j];
                book.SaveMatrixPos(this, j, i);
            }
        }
    }

    public void CheckCombination(BookBehaviour bookData, int bookRow, int bookColumn) 
    {
        if (bookData == correctLeftBook) 
        {
            if (bookRow + 1 >= dropAreaMatrix[bookColumn].books.Length) return;

            if (dropAreaMatrix[bookColumn].books[bookRow + 1].bookBehaviour == correctRightBook)
            {
                correctLeftBook.SetMovement(false);
                correctRightBook.SetMovement(false);
                OnCorrectBooks.Invoke();
            }

        }
        else if (bookData == correctRightBook) 
        {
            if (bookRow == 0) return;

            if (dropAreaMatrix[bookColumn].books[bookRow - 1].bookBehaviour == correctLeftBook) 
            {
                correctLeftBook.SetMovement(false);
                correctRightBook.SetMovement(false);
                OnCorrectBooks.Invoke();
            }
        }
    }
}

[System.Serializable]
public class DropAreaLine 
{
    public DropArea[] books;
}