using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NoireBoss : MonoBehaviour
{

    public NewRoom bossRoom;
    public TextMeshProUGUI dialogueText;
    bool crRunning;
    bool flagged = false;
    public BoxCollider hitbox;
    public Image healthColor;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    int lastWaveNum = 0;
    // Update is called once per frame
    void Update()
    {
        if (bossRoom.inRoom)
        {
            if(lastWaveNum != bossRoom.currentWave)
            {
                lastWaveNum++;
                if(bossRoom.currentWave == 1)
                {
                    StartCoroutine(DisplayText("Get Her!"));
                }
                if (bossRoom.currentWave == 2)
                {
                    if (crRunning)
                    {
                        StopCoroutine("DisplayText");
                        crRunning = false;
                    }
                        
                    StartCoroutine(DisplayText("C'mon! What do I pay you for!"));
                }
                if (bossRoom.currentWave == 3)
                {
                    if(crRunning)
                    {
                        StopCoroutine("DisplayText");
                        crRunning = false;
                    }
                    StartCoroutine(DisplayText("She's only ONE GIRL!"));
                }
                if (bossRoom.currentWave == 4)
                {
                    if (crRunning)
                    {
                        StopCoroutine("DisplayText");
                        crRunning = false;
                    }
                    StartCoroutine(DisplayText("You scumbags are worthless!"));
                }
            }
        }
        if (bossRoom.enemiesDead && !flagged)
        {
            if (crRunning)
            {
                StopCoroutine("DisplayText");
                crRunning = false;
            }
            flagged = true;
            StartCoroutine(DisplayText("Just you and me..."));
            healthColor.color = Color.red;
            hitbox.enabled = true;
        }
    }

    IEnumerator DisplayText(string text)
    {
        crRunning = true;
        dialogueText.text = text;
        yield return new WaitForSeconds(5);
        dialogueText.text = "";
        crRunning = false;
    }
}
