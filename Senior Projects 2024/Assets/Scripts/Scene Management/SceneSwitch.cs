using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//Author: Fenn
//Loads scenes depending on the situation
public class SceneSwitch : MonoBehaviour
{
    public Animator loadingBar;
    [HideInInspector]
    public int currentScene = 0;
    public static SceneSwitch inst;

    private void Start()
    {
        print("Erm?");

        if (loadingBar == null)
        {
            print("Animator Error!");
            return;
        }
    }
    public void Awake()
    {

        inst = this;
    }

    public void LoadScene()
    {
        StartCoroutine(LoadYourAsyncScene());
        loadingBar.SetTrigger("StartLoading");

    }

    //Basic Load
    public IEnumerator LoadYourAsyncScene()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(currentScene);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        SaveMgr.inst.LoadData();
    }

    //Sets the current scene to Main Menu
    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        currentScene = 0;
        LoadScene();
    }

    //Quits the game
    public void QuitGame()
    {
        Application.Quit();
    }
}