using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MasherMinigameMgr : MonoBehaviour
{
    public float timer = 100000;
    public float maxTime = 0;
    public Slider timerSlider;
    public int score = 0;
    public List<int> maxScore;
    public GameObject menu;
    public RectTransform stamp;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI maxScoreText;
    public float distance;

    //public OpheliaStats stats;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer < maxTime)
        {
            menu.SetActive(false);
            timerSlider.value = timer / maxTime;

            if (Input.GetKeyUp(KeyCode.Space))
            {
                score++;
                scoreText.text = "Score: " + score;
                maxScoreText.text = UpdateHighScore(score);
                StartCoroutine(BounceImage(stamp, distance, 0.1f));
            }
        }
        else
        {
            timer = 68;
            menu.SetActive(true);
        }
    }

    public void StartGame()
    {
        UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(null);
        timer = 0;
        score = 0;
        scoreText.text = "Score: " + score;
        OpheliaStats.inst.ogDamage += 1;
    }

    public void UpdateDifficulty(int setting)
    {
        Debug.Log(setting);

        if (setting == 0)
            maxTime = 10;
        else if (setting == 1)
            maxTime = 30;
        else if (setting == 2)
            maxTime = 60;
    }

    string UpdateHighScore(int score)
    {
        if(maxTime == 10)
            if(score > maxScore[0])
                maxScore[0] = score;
        if (maxTime == 30)
            if (score > maxScore[1])
                maxScore[1] = score;
        if (maxTime == 60)
            if (score > maxScore[2])
                maxScore[2] = score;

        string output = "High Scores\n";
        output += "10s: " + maxScore[0];
        output += "\n30s: " + maxScore[1];
        output += "\n60s: " + maxScore[2];

        return output;
    }

    private bool isBouncing = false;
    public IEnumerator BounceImage(RectTransform image, float distance, float duration)
    {
        if (isBouncing) yield break;
        isBouncing = true;

        Vector3 start = image.anchoredPosition;
        Vector3 down = start - new Vector3(0, distance, 0);

        // Move down
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime / (duration / 2f); // First half of duration
            image.anchoredPosition = Vector3.Lerp(start, down, t);
            yield return null;
        }

        // Move back up
        t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime / (duration / 2f); // Second half
            image.anchoredPosition = Vector3.Lerp(down, start, t);
            yield return null;
        }

        image.anchoredPosition = start;
        isBouncing = false;
    }

}
