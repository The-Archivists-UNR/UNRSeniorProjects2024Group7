using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class VideoCutscene : MonoBehaviour
{
    //Author: Fenn Edmonds
    public VideoPlayer player;
    bool loading = false;
    // Start is called before the first frame update
    void Start()
    {
    }


    //public void LoadScene()
    //{
    //    StartCoroutine(LoadYourAsyncScene());
    //}

    //Basic Load
    public IEnumerator LoadYourAsyncScene()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(7);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (player.frame >= (long)(player.frameCount - 1) && !loading)
        {
            StartCoroutine(LoadYourAsyncScene());
            loading = true;
            // Add any other action you want to take here.
        }
    }
}
