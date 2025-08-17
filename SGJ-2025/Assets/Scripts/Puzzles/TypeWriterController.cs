using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class TypeWriterController : MonoBehaviour
{
    [SerializeField] private string correctString;
    [SerializeField] private string allowedCharacters;
    [SerializeField] private int maxLenght;
    [SerializeField] private TMP_Text textField;
    private string currentString = "";

    [SerializeField] private UnityEvent OnCorrectString;

    void OnEnable()
    {
        Keyboard.current.onTextInput += OnKeyPress;

        currentString = "";
    }

    void OnDisable()
    {
        Keyboard.current.onTextInput -= OnKeyPress;
    }

    public void OnKeyPress(string newString)
    {
        if (newString == "BACKSPACE") 
        {
            OnKeyPress('\b');
        }
        else if (newString == "ENTER") 
        {
            OnKeyPress((char)13);
        }
        else 
        {
            OnKeyPress(newString[0]);
        }
    }

    public void OnKeyPress(char newChar) 
    {
        newChar = char.ToUpper(newChar);

        if (allowedCharacters.Contains(newChar)) 
        {
            if (currentString.Length <= maxLenght)
                currentString += newChar;
        }
        else if (newChar == '\b') 
        {
            if (currentString.Length > 0)
                currentString = currentString.Remove(currentString.Length - 1);
        }
        else if (newChar == 13) 
        {
            CheckCombination();
            currentString = "";
        }

        UpdateUI();
    }

    private void UpdateUI() 
    {
        textField.text = currentString;
    }

    private void CheckCombination() 
    {
        if (currentString == correctString) 
        {
            OnCorrectString.Invoke();
        }
    }

}
