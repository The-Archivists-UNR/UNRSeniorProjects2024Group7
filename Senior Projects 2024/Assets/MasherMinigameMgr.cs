using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class MasherMinigameMgr : MonoBehaviour
{
    public float timer = 0;
    public float maxTime = 0;
    public int score;
    public RectTransform stamp;
    public float distance;

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
            if(Input.GetKeyUp(KeyCode.Space))
            {
                score++;
                StartCoroutine(BounceImage(stamp, distance, 0.1f));
            }
        }
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

        // Wait 3 frames
        yield return null;
        yield return null;
        yield return null;

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
