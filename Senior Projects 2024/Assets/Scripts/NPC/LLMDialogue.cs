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

    [Header("JSON API Configuration")]
    public TextAsset jsonApi;
    private string apiKey = "";
    private string apiEndpoint = "https://generativelanguage.googleapis.com/v1beta/models/gemini-1.5-flash-latest:generateContent"; 

    [Header("ChatBot Function")]
    private Content[] chatHistory;

    void Start()
    {
        UnityAndGeminiKey jsonApiKey = JsonUtility.FromJson<UnityAndGeminiKey>(jsonApi.text);
        apiKey = jsonApiKey.key;
        chatHistory = new Content[] { };

        //var safetySettings = new List<SafetySetting> {
        //    new SafetySetting { category = "HARM_CATEGORY_HARASSMENT", threshold = "BLOCK_MEDIUM_AND_ABOVE" },
        //    new SafetySetting { category = "HARM_CATEGORY_HATE_SPEECH", threshold = "BLOCK_MEDIUM_AND_ABOVE" },
        //    new SafetySetting { category = "HARM_CATEGORY_SEXUALLY_EXPLICIT", threshold = "BLOCK_LOW_AND_ABOVE" },
        //    new SafetySetting { category = "HARM_CATEGORY_DANGEROUS_CONTENT", threshold = "BLOCK_MEDIUM_AND_ABOVE" }
        //};

        //StartCoroutine(SendPromptRequestToGemini("", safetySettings));
    }

    public void getResponse(string message, Callback<string> handleOutput = null, EmptyCallback onComplete = null)
    {
        var safetySettings = new List<SafetySetting> {
            new SafetySetting { category = "HARM_CATEGORY_HARASSMENT", threshold = "BLOCK_MEDIUM_AND_ABOVE" },
            new SafetySetting { category = "HARM_CATEGORY_HATE_SPEECH", threshold = "BLOCK_MEDIUM_AND_ABOVE" },
            new SafetySetting { category = "HARM_CATEGORY_SEXUALLY_EXPLICIT", threshold = "BLOCK_LOW_AND_ABOVE" }, 
            new SafetySetting { category = "HARM_CATEGORY_DANGEROUS_CONTENT", threshold = "BLOCK_MEDIUM_AND_ABOVE" }
        };
        _ = StartCoroutine(SendChatRequestToGemini(message, safetySettings, handleOutput, onComplete));
    }


    //private IEnumerator SendPromptRequestToGemini(string promptText, List<SafetySetting> safetySettings, Callback<string> handleOutput = null, EmptyCallback onComplete = null)
    //{
    //    //Debug.Log("sentPrompt called");
    //    string url = $"{apiEndpoint}?key={apiKey}";
    //    string settings = JsonUtility.ToJson(safetySettings);
    //    string jsonData = "{\"contents\": [{\"parts\": [{\"text\": \"{" + promptText + "}\"}]}], \"safetySettings\": " + settings + "}";
    //    byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(jsonData);

    //    // Create a UnityWebRequest with the JSON data
    //    using (UnityWebRequest www = new UnityWebRequest(url, "POST"))
    //    {
    //        www.uploadHandler = new UploadHandlerRaw(jsonToSend);
    //        www.downloadHandler = new DownloadHandlerBuffer();
    //        www.SetRequestHeader("Content-Type", "application/json");

    //        yield return www.SendWebRequest();

    //        if (www.result != UnityWebRequest.Result.Success) {
    //            Debug.LogError("error");
    //            Debug.Log(www.error); 
    //        }
    //        else
    //        {
    //            Debug.Log("EEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEE");
    //            Response response = JsonUtility.FromJson<Response>(www.downloadHandler.text);
    //            if (response.promptFeedback != null && !string.IsNullOrEmpty(response.promptFeedback.blockReason))
    //            {
    //                Debug.LogWarning($"Prompt blocked by Gemini. Reason: {response.promptFeedback.blockReason}");
    //                //HANDLING GOES HERE
    //            }
    //            else if (response.candidates.Length > 0 && response.candidates[0].content.parts.Length > 0)
    //            {
    //                //This is the response to your request
    //                string text = response.candidates[0].content.parts[0].text;
    //                Debug.Log(text);

    //                if (handleOutput != null) handleOutput(text);
    //                if (onComplete != null) onComplete();
    //            }
    //            else
    //            {
    //                Debug.Log("No text found.");
    //            }
    //        }
    //    }
    //}


    private IEnumerator SendChatRequestToGemini(string newMessage, List<SafetySetting> safetySettings, Callback<string> handleOutput = null, EmptyCallback onComplete = null)
    {
        string url = $"{apiEndpoint}?key={apiKey}";
        string prompt = newMessage;
        Content userContent = new Content
        {
            role = "user",
            parts = new Part[]
            {
                    new Part { text = prompt }
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
                Debug.Log("Chat request complete! Prompt Sent: \n\n"+prompt);
                Response response = JsonUtility.FromJson<Response>(www.downloadHandler.text);

                if (response.promptFeedback != null && !string.IsNullOrEmpty(response.promptFeedback.blockReason))
                {
                    Debug.LogWarning($"Prompt blocked by Gemini. Reason: {response.promptFeedback.blockReason}");
                }
                else if (response.candidates.Length > 0 && response.candidates[0].content.parts.Length > 0)
                {
                    string reply = response.candidates[0].content.parts[0].text.Trim();
                    Content botContent = new Content
                    {
                        role = "model",
                        parts = new Part[]
                        {
                                    new Part { text = reply }
                        }
                    };

                    Debug.Log(reply);

                    if (handleOutput != null) handleOutput(reply);
                    if (onComplete != null) onComplete();

                    //This part adds the response to the chat history, for your next message
                    contentsList.Add(botContent);
                    chatHistory = contentsList.ToArray();
                }
                else { Debug.Log("No text found."); }
            }
        }
    }
}



/* Gemini's suggested Java implementation of safety settings:

SafetySetting harassmentSafety = new SafetySetting(HarmCategory.HARASSMENT, BlockThreshold.LOW_AND_ABOVE);

SafetySetting hateSpeechSafety = new SafetySetting(HarmCategory.HATE_SPEECH, BlockThreshold.LOW_AND_ABOVE);

GenerativeModel gm = new GenerativeModel(
    "gemini-1.5-flash",
    BuildConfig.apiKey,
    null, // generation config is optional
    Arrays.asList(harassmentSafety, hateSpeechSafety)
);

GenerativeModelFutures model = GenerativeModelFutures.from(gm);
*/