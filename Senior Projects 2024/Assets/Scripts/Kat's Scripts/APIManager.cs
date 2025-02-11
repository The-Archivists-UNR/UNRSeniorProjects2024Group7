using System.Collections;
using System.Collections.Generic;
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
        if (Input.GetKeyDown(KeyCode.RightShift))
        {
            StartCoroutine(SendDataToGAS());
        }
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
