using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookShelfManager : MonoBehaviour
{
    private int[,] matrix;
    [SerializeField] int rows = 0;
    [SerializeField] int columns = 0;

    void Start()
    {
        matrix = new int[rows, columns];
    }
}
