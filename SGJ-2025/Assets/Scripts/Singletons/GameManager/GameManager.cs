using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }
    private GameObject playerCursorObj;
    private GameObject cameraCursorObj;

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

    public GameObject GetCameraRef()
    {
        if (cameraCursorObj == null)
        {
            cameraCursorObj = GameObject.FindGameObjectWithTag("CameraController");
        }
        return cameraCursorObj;
    }
}
