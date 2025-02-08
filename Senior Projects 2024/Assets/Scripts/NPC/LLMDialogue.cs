/*
 * todo:
 * manage prompts for different NPCs
 * move access to AIText box to "Dialogue"
 * rename to LLMHandling?
 */

using UnityEngine;
using LLMUnity;
// using UnityEditor.VersionControl;
using UnityEngine.Networking;
using System.Collections;

//Authored By Lanielle, based on SimpleInteraction example code from LLM Plugin
public class LLMInteraction : MonoBehaviour
{
    //public LLMCharacter llmCharacter;
    public TMPro.TextMeshProUGUI AIText;
    private int count;
    private int rating;
    [SerializeField] private string gasURL;

    void Start()
    {
        count = 0;
    }

    public void welcome()
    {
        //the prompt below should be changed for each NPC, replace with variable later
        _ = StartCoroutine(askLLM("Welcome Ophelia to the cursed library"));
    }

    public void getResponse(string message)
    {
        AIText.text = "...";
        _ = StartCoroutine(askLLM(message));
    }

    public void SetAIText(string text)
    {
        AIText.text = text;
    }

    //added for debugging purposes - LLMCharacter.Chat() requires callback functions
    public void setRatingVar(string text)
    {
        Debug.Log("LLM: "+text);
        int.TryParse(text, out rating);
    }

    public void SetAIGoodbyeText(string text)
    {
        AIText.text = text + " Goodbye!";
    }

    public void EndConversation(string text, EmptyCallback callback = null)
    {
        //_ = llmCharacter.Chat("respond to the following: \""+text+"\" and say goodbye to Ophelia.",
        //    SetAIText, AIReplyComplete);
        _ = StartCoroutine(askLLM(text));//todo: move setAItext, etc. to Dialogue
        _ = StartCoroutine(askLLM("Rate the pleasantness of this conversation on a scale from 1 to 10. " +
            "Respond with only the number.", true));
    }

    public int getRating()
    {
        int r = rating;
        rating = 0;
        return r-5;

    }

    //public void AIReplyComplete()
    //{
    //    playerText.interactable = true;
    //    playerText.Select();
    //    if (count != -1)
    //    {
    //        playerText.text = "";
    //    } 
    //    else
    //    {
    //        playerText.text = "The Ghost seems busy. Use 'enter' to exit.";
    //    }
    //}

    //public void CancelRequests()
    //{
    //    llmCharacter.CancelRequests();
    //    AIReplyComplete();
    //}

    //public void ExitGame()
    //{
    //    Debug.Log("Exit button clicked");
    //    Application.Quit();
    //}

    //bool onValidateWarning = true;
    //void OnValidate()
    //{
    //    if (onValidateWarning && !llmCharacter.remote && llmCharacter.llm != null && llmCharacter.llm.model == "")
    //    {
    //        Debug.LogWarning($"Please select a model in the {llmCharacter.llm.gameObject.name} GameObject!");
    //        onValidateWarning = false;
    //    }
    //}


    //from Kat, from Can with Code on YouTube
    private IEnumerator askLLM(string prompt, bool isScore=false)
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

        if (isScore)
        {
            setRatingVar(response);
        }
        else
        {
            SetAIText(response);
        }
    }
}
