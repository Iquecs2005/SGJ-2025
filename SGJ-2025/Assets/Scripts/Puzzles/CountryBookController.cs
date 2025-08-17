using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountryBookController : MonoBehaviour
{
    [SerializeField] private GameObject[] booksGameObjects;
    private int currentIndex = 0;

    public void AdvanceBooks() 
    {
        booksGameObjects[currentIndex].SetActive(false);
        currentIndex = (currentIndex + 1) % booksGameObjects.Length;
        booksGameObjects[currentIndex].SetActive(true);
    }
    
    public void RetreatBooks() 
    {
        booksGameObjects[currentIndex].SetActive(false);
        currentIndex -= 1;
        if (currentIndex < 0) 
        {
            currentIndex = booksGameObjects.Length - 1;
        }
        booksGameObjects[currentIndex].SetActive(true);
    }
}
