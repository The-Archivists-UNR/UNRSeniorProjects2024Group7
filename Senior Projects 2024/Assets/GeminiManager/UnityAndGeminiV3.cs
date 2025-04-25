//unity-gemini plugin, modifications by Lanielle

using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;
using TMPro;

[System.Serializable]
public class UnityAndGeminiKey
{
    public string key;
}

[System.Serializable]
public class Response
{
    public Candidate[] candidates;
    //line below added apr 24, 2025  -  remove if it breaks everything
    public PromptFeedback promptFeedback;
}

public class ChatRequest
{
    public Content[] contents;
    //line below added apr 24, 2025  -  remove if it breaks everything
    public List<SafetySetting> safetySettings;
}

[System.Serializable]
public class Candidate
{
    public Content content;
    //new variables below
    public string finishReason;
    public int index;
    public SafetyRating[] safetyRatings;
}

[System.Serializable]
public class Content
{
    public string role; 
    public Part[] parts;
}


[System.Serializable]
public class Part
{
    public string text;
}


//created to facilitate use of safety settings
[System.Serializable]
public class SafetySetting
{
    public string category;
    public string threshold;
}

//created to facilitate use of safety settings
[System.Serializable]
public class PromptFeedback
{
    public string blockReason;
    public SafetyRating[] safetyRatings;
}

//created to facilitate use of safety settings
[System.Serializable]
public class SafetyRating
{
    public string category;
    public string probability;
    public string severity;
    public bool blocked;//"bool?"?
}


public class UnityAndGeminiV3: MonoBehaviour
{
    [Header("JSON API Configuration")]
    public TextAsset jsonApi;
    private string apiKey = ""; 
    private string apiEndpoint = "https://generativelanguage.googleapis.com/v1beta/models/gemini-1.5-flash-latest:generateContent"; // Edit it and choose your prefer model


    [Header("ChatBot Function")]
    public TMP_InputField inputField;
    public TMP_Text uiText;
    private Content[] chatHistory;

    [Header("Prompt Function")]
    public string prompt = "";



    void Start()
    {
        UnityAndGeminiKey jsonApiKey = JsonUtility.FromJson<UnityAndGeminiKey>(jsonApi.text);
        apiKey = jsonApiKey.key;   
        chatHistory = new Content[] { };

        var safetySettings = new List<SafetySetting> {
            new SafetySetting { category = "HARM_CATEGORY_HARASSMENT", threshold = "BLOCK_MEDIUM_AND_ABOVE" },
            new SafetySetting { category = "HARM_CATEGORY_HATE_SPEECH", threshold = "BLOCK_MEDIUM_AND_ABOVE" },
            new SafetySetting { category = "HARM_CATEGORY_SEXUALLY_EXPLICIT", threshold = "BLOCK_LOW_AND_ABOVE" }, // Stricter example
            new SafetySetting { category = "HARM_CATEGORY_DANGEROUS_CONTENT", threshold = "BLOCK_MEDIUM_AND_ABOVE" }
        };

        StartCoroutine( SendPromptRequestToGemini(prompt, safetySettings));        
    }

    private IEnumerator SendPromptRequestToGemini(string promptText, List<SafetySetting> safetySettings)
    {
        string url = $"{apiEndpoint}?key={apiKey}";
        string settings = JsonUtility.ToJson(safetySettings);
        string jsonData = "{\"contents\": [{\"parts\": [{\"text\": \"{" + promptText + "}\"}]}], \"safetySettings\": "+settings+"}";
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(jsonData);

        // Create a UnityWebRequest with the JSON data
        using (UnityWebRequest www = new UnityWebRequest(url, "POST")){
            www.uploadHandler = new UploadHandlerRaw(jsonToSend);
            www.downloadHandler = new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");

            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success) {
                Debug.LogError(www.error);
            } else {
                Debug.Log("Request complete!");
                Response response = JsonUtility.FromJson<Response>(www.downloadHandler.text);
                if (response.candidates.Length > 0 && response.candidates[0].content.parts.Length > 0)
                    {
                        //This is the response to your request
                        string text = response.candidates[0].content.parts[0].text;
                        Debug.Log(text);
                    }
                else
                {
                    Debug.Log("No text found.");
                }
            }
        }
    }

    public void SendChat()
    {
        string userMessage = inputField.text;
        StartCoroutine( SendChatRequestToGemini(userMessage));
    }

    private IEnumerator SendChatRequestToGemini(string newMessage)
    {

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
        using (UnityWebRequest www = new UnityWebRequest(url, "POST")){
            www.uploadHandler = new UploadHandlerRaw(jsonToSend);
            www.downloadHandler = new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");

            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success) {
                Debug.LogError(www.error);
            } else {
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
                        uiText.text = reply;
                        //This part adds the response to the chat history, for your next message
                        contentsList.Add(botContent);
                        chatHistory = contentsList.ToArray();
                    }
                else
                {
                    Debug.Log("No text found.");
                }
             }
        }  
    }
}


