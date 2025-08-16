using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class QuantumLockedBehaviour : MonoBehaviour
{
    [SerializeField] private float starterProbability;
    [SerializeField] private float probabilityStep;
    [SerializeField] private UnityEvent[] SetState;

    private int currentSwap = 0;
    private float currentProbability;

    private void Start()
    {
        currentProbability = starterProbability;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        print("a");
        float randomValue = Random.Range(0f, 100f);

        if (randomValue < currentProbability) 
        {
            SwapState();
            currentProbability = starterProbability;
        }
        else 
        {
            currentProbability += probabilityStep;
        }
    }

    private void SwapState() 
    {
        currentSwap = (currentSwap + 1) % SetState.Length;

        SetState[currentSwap].Invoke();
    }
}
