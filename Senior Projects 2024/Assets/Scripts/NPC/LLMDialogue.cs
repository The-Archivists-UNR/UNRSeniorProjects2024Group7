//Authored By Lanielle and Kat

/*
 * todo:
 * manage prompts for different NPCs
 * rename to LLMHandling?
 */

using UnityEngine;
//using LLMUnity;
// using UnityEditor.VersionControl;
using UnityEngine.Networking;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;
using TMPro;

public class LLMInteraction : MonoBehaviour
{
    //[SerializeField] private string gasURL;

    [Header("JSON API Configuration")]
    public TextAsset jsonApi;
    private string apiKey = "";
    private string apiEndpoint = "https://generativelanguage.googleapis.com/v1beta/models/gemini-1.5-flash-latest:generateContent"; // Edit it and choose your prefer model

    [Header("ChatBot Function")]
    //public TMP_InputField inputField;
    //public TMP_Text uiText;
    private Content[] chatHistory;

    void Start()
    {
        Debug.Log("start function called");
        UnityAndGeminiKey jsonApiKey = JsonUtility.FromJson<UnityAndGeminiKey>(jsonApi.text);
        apiKey = jsonApiKey.key;
        chatHistory = new Content[] { };
        StartCoroutine(SendPromptRequestToGemini(""));
        //StartCoroutine(SendPromptRequestToGemini(prompt));
        Debug.Log("start function concluded");
        //startAPIUnitTest();
    }

    public void getResponse(string message, Callback<string> handleOutput = null, EmptyCallback onComplete = null)
    {
        Debug.Log("getResponse called");
        _ = StartCoroutine(SendChatRequestToGemini(message, handleOutput, onComplete));
        Debug.Log("getResponse concluded");
    }


    private IEnumerator SendPromptRequestToGemini(string promptText, Callback<string> handleOutput = null, EmptyCallback onComplete = null)
    {
        Debug.Log("sentPrompt called");
        string url = $"{apiEndpoint}?key={apiKey}";
        string jsonData = "{\"contents\": [{\"parts\": [{\"text\": \"{" + promptText + "}\"}]}]}";
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(jsonData);

        // Create a UnityWebRequest with the JSON data
        using (UnityWebRequest www = new UnityWebRequest(url, "POST"))
        {
            www.uploadHandler = new UploadHandlerRaw(jsonToSend);
            www.downloadHandler = new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");

            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success) { Debug.LogError(www.error); }
            else
            {
                //Debug.Log("Request complete!");
                Response response = JsonUtility.FromJson<Response>(www.downloadHandler.text);
                if (response.candidates.Length > 0 && response.candidates[0].content.parts.Length > 0)
                {
                    //This is the response to your request
                    string text = response.candidates[0].content.parts[0].text;
                    Debug.Log(text);
                    if (handleOutput != null) handleOutput(text);
                    if (onComplete != null) onComplete();
                }
                else
                {
                    Debug.Log("No text found.");
                }
            }
        }
        Debug.Log("sendPrompt concluded");
    }

    //public void SendChat(string userMessage)
    //{
    //    //string userMessage = inputField.text;
    //    StartCoroutine(SendChatRequestToGemini(userMessage));
    //    }

    private IEnumerator SendChatRequestToGemini(string newMessage, Callback<string> handleOutput = null, EmptyCallback onComplete = null)
    {
        Debug.Log("sendChat called");
        string url = $"{apiEndpoint}?key={apiKey}";
        Content userContent = new Content
        {
            role = "user",
            parts = new Part[]
            {
                    new Part { text = newMessage }
            }
        };

        List<Content> contentsList = new List<Content>(chatHistory);
        contentsList.Add(userContent);
        chatHistory = contentsList.ToArray();

        ChatRequest chatRequest = new ChatRequest { contents = chatHistory };
        string jsonData = JsonUtility.ToJson(chatRequest);
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(jsonData);

        // Create a UnityWebRequest with the JSON data
        using (UnityWebRequest www = new UnityWebRequest(url, "POST"))
        {
            www.uploadHandler = new UploadHandlerRaw(jsonToSend);
            www.downloadHandler = new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");

            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success) { Debug.LogError(www.error); }
            else
            {
                Debug.Log("Request complete!");
                Response response = JsonUtility.FromJson<Response>(www.downloadHandler.text);

                if (response.candidates.Length > 0 && response.candidates[0].content.parts.Length > 0)
                {
                    //This is the response to your request
                    string reply = response.candidates[0].content.parts[0].text;
                    Content botContent = new Content
                    {
                        role = "model",
                        parts = new Part[]
                        {
                                    new Part { text = reply }
                        }
                    };

                    Debug.Log(reply);
                    //This part shows the text in the Canvas
                    //uiText.text = reply;
                    if (handleOutput != null) handleOutput(reply);
                    if (onComplete != null) onComplete();

                    //This part adds the response to the chat history, for your next message
                    contentsList.Add(botContent);
                    chatHistory = contentsList.ToArray();
                }
                else { Debug.Log("No text found."); }
            }
        }
        Debug.Log("sendChat concluded");
    }
}





