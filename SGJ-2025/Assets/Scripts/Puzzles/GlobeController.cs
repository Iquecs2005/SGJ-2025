using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GlobeController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private GameObject[] globePanels;

    [Header("Variables")]
    [SerializeField] private GameObject[] correctButtonOrder;

    [Header("Events")]
    [SerializeField] private UnityEvent OnCorrectSolution;

    private int currentIndex;
    private int currentPanel;

    private void OnEnable()
    {
        currentIndex = 0;

        for (int i = 0; i < globePanels.Length; i++)
        {
            GameObject panel = globePanels[i];
            if (panel.activeInHierarchy)
            {
                currentPanel = i;
                break;
            }
        }
    }

    public void OnButtonClick(GameObject button) 
    {
        if (button == correctButtonOrder[currentIndex]) 
        {
            currentIndex++;

            if (currentIndex == correctButtonOrder.Length) 
            {
                currentIndex = 0;
                OnCorrectSolution.Invoke();
            }
        }
        else 
        {
            currentIndex = 0;
            if (button == correctButtonOrder[0]) 
            {
                currentIndex++;
            }
        }
    }

    public void ChangeCurrentPanel(int modifyAmount) 
    {
        globePanels[currentPanel].SetActive(false);

        currentPanel = (currentPanel + modifyAmount) % globePanels.Length;
        if (currentPanel < 0) 
        {
            currentPanel = globePanels.Length - 1;
        }

        globePanels[currentPanel].SetActive(true);
    }

    
}