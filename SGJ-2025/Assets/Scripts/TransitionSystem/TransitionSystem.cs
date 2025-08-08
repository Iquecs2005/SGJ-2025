using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionSystem : MonoBehaviour
{
    [Header("Link in Inspector")]
    public Animator transition;


    [Header("Transition Parameters")]
    public float transitionTime = 1f;

    public bool isThisSceneReloadable;

    void Update()
    {
        if (isThisSceneReloadable)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                RestartCurrentScene();
            }
        }
    }

    public void LoadLevelByName(string sceneName)
    {
        StartCoroutine(LoadLevelByNameCoroutine(sceneName));
        Time.timeScale = 1;
    }

    IEnumerator LoadLevelByNameCoroutine(string sceneName)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(sceneName);
    }

    public void RestartCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }
}
