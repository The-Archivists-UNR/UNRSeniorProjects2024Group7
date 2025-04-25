using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingMinigameMgr : MonoBehaviour
{
    public Transform bobberTransform;
    bool biteWindowActive;
    bool playerReacted;
    bool playing;
    public GameObject menu;
    public GameObject menu2;
    //public OpheliaStats stats;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (biteWindowActive && Input.GetKeyDown(KeyCode.Space))
        {
            playerReacted = true;
            Debug.Log("Caught the fish!");
            OpheliaStats.inst.ogHP += 5;
            // Do something here like play animation or give reward
        }
    }

    public void StartFishing()
    {
        menu.SetActive(false);
        menu2.SetActive(false);
        StartCoroutine(FishingSequence());
    }

    IEnumerator FishingSequence()
    {
        yield return new WaitForSeconds(Random.Range(2f, 5f)); // Wait time before first nibble

        int nibbles = Random.Range(1, 3);
        for (int i = 0; i < nibbles; i++)
        {
            StartCoroutine(TriggerNibble(bobberTransform));
            yield return new WaitForSeconds(Random.Range(0.7f, 1.5f));
        }

        StartCoroutine(TriggerRealBite(bobberTransform)); ; // Big bob down
        biteWindowActive = true;
        float biteDuration = 0.5f;
        yield return new WaitForSeconds(biteDuration);

        if (!playerReacted)
        {
            Debug.Log("Missed the fish!");
        }

        biteWindowActive = false;
        menu.SetActive(true);
        menu2.SetActive(true);
    }

    public IEnumerator TriggerNibble(Transform bobber, float duration = 0.5f, float intensity = 10.1f)
    {
        Vector3 originalPos = bobber.localPosition;
        float timer = 0f;
        AudioMgr.Instance.PlaySFX("Bob");

        while (timer < duration)
        {
            // Sine wave motion for nibble
            float offsetY = Mathf.Sin(timer * Mathf.PI * 4) * intensity; // 4 = frequency (how many up-downs in duration)
            bobber.localPosition = originalPos + new Vector3(0, offsetY, 0);

            timer += Time.deltaTime;
            yield return null;
        }

        bobber.localPosition = originalPos;
    }

    public IEnumerator TriggerRealBite(Transform bobber, float dipAmount = 200.3f, float dipDuration = 0.1f, float returnDuration = 0.2f)
    {
        Vector3 originalPos = bobber.localPosition;
        Vector3 dippedPos = originalPos + new Vector3(0, -dipAmount, 0);

        float t = 0f;
        AudioMgr.Instance.PlaySFX("Bite");

        // Sudden downward motion
        while (t < dipDuration)
        {
            bobber.localPosition = Vector3.Lerp(originalPos, dippedPos, t / dipDuration);
            t += Time.deltaTime;
            yield return null;
        }
        bobber.localPosition = dippedPos;

        t = 0f;

        // Smooth bounce back up
        while (t < returnDuration)
        {
            // Ease-out effect for natural feel
            float eased = 1f - Mathf.Pow(1f - t / returnDuration, 2);
            bobber.localPosition = Vector3.Lerp(dippedPos, originalPos, eased);
            t += Time.deltaTime;
            yield return null;
        }

        bobber.localPosition = originalPos;
    }



}
