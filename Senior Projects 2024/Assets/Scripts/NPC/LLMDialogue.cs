//Authored By Lanielle and Kat

/*
 * todo:
 * manage prompts for different NPCs
 * rename to LLMHandling?
 */

/**
 * This file defines the LLMDialogue class, which contains:
 * 
 * (public methods)
 * Start: it doesn't do anything, but it's there if we want it
 * getResponse: gets text from LLM based on prompt parameter and applies output to callback function
 * 
 * (private methods)
 * askLLM: interacts with Gemini via API
 * 
 */


using UnityEngine;
//using LLMUnity;
// using UnityEditor.VersionControl;
using UnityEngine.Networking;
using System.Collections;

public class LLMInteraction : MonoBehaviour
{
    [SerializeField] private string gasURL;

    void Start()
    {

    }

    public void getResponse(string message, Callback<string> handleOutput = null, EmptyCallback onComplete = null)
    {
        _ = StartCoroutine(askLLM(message, handleOutput, onComplete));
    }
    
    //from Kat, from Can with Code on YouTube
    private IEnumerator askLLM(string prompt, Callback<string> handleOutput = null, EmptyCallback onComplete = null)
    {
        WWWForm form = new WWWForm();
        form.AddField("parameter", prompt);
        UnityWebRequest www = UnityWebRequest.Post(gasURL, form);

        yield return www.SendWebRequest();
        string response = "";

        if (www.result == UnityWebRequest.Result.Success)
        {
            response = www.downloadHandler.text;
        }
        else
        {
            response = "Error";
        }

        Debug.Log(response);

        if (handleOutput != null) handleOutput(response);
        if (onComplete != null) onComplete();
    }
}
