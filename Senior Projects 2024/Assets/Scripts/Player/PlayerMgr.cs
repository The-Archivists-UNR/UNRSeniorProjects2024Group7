//Script: PlayerMgr.cs
//Contributor: Liam Francisco
//Summary: Handles player aspcets like speed and health
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerMgr : MonoBehaviour
{
    public float baseSpeed;
    public float speed;
    public float baseHealth;

    public Entity player;

    public static PlayerMgr inst;

    public List<Interactable> interactables;

    public TextMeshProUGUI interactText;

    //Start is called before the first frame update
    void Awake()
    {
        inst = this;
        speed = baseSpeed;
        player.speed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        //player.speed = speed;
    }

    public IEnumerator SlowDown(float multiplier, float timeSlowed)
    {
        speed *= multiplier;
        yield return new WaitForSeconds(timeSlowed);
        Debug.Log("slow over");
        speed = baseSpeed;
    }
}
