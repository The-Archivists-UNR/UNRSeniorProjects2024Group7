using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

//code provided by Can with Code on youtube
public class APIManager : MonoBehaviour
{
    [SerializeField] private string gasURL;
    [SerializeField] private string prompt;

    private void Update()
    {
        //change this to something like on trigger when near an NPC
        //if (Input.GetKeyDown(KeyCode.RightShift))
        //{
        //    StartCoroutine(SendDataToGAS());
        //}
        // above 'if' commented out by Lanielle 2/22/25 because API call is being made elsewhere
    }
    private IEnumerator SendDataToGAS()
    {
        WWWForm form = new WWWForm();
        form.AddField("parameter", prompt);
        UnityWebRequest www = UnityWebRequest.Post(gasURL, form);

        yield return www.SendWebRequest();
        string response = "";

        if(www.result == UnityWebRequest.Result.Success)
        {
            response = www.downloadHandler.text;
        }
        else
        {
            response = "Error";
        }
        Debug.Log(response);
    }
}
