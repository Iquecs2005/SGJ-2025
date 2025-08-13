using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }
    private GameObject playerCursorObj;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public GameObject GetPlayerRef() 
    {
        if (playerCursorObj == null) 
        {
            playerCursorObj = GameObject.FindGameObjectWithTag("Player");
        }
        return playerCursorObj;
    }
}
