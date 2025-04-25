using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Author: Fenn Edmonds

public class SpeedItem : MonoBehaviour
{
    public PlayerMgr playerMgr;
    public Entity entity;
    private float speedIncrease;
    public ItemRarity rarity;
    private bool active = true;
    private GameObject child;
    private Vector3 initPos;
    private float amp = .5f;
    private float freq = 3f;

    // Start is called before the first frame update
    void Start()
    {
        initPos = transform.position;
        //gameObject.SetActive(false);
        child = gameObject.transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
            transform.position = new Vector3(initPos.x, Mathf.Sin(Time.time * freq) * amp + initPos.y, initPos.z);
            // Translate object up and down on y axis
    }

    public void ItemAppear()
    {
        gameObject.SetActive(true);
        initPos = transform.position;
        active = true;
    }

    void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            switch (rarity)
            {
                case ItemRarity.Common:
                    speedIncrease = 1;
                break;

                case ItemRarity.Rare:
                    speedIncrease = 2;
                    break;

                case ItemRarity.Epic:
                    speedIncrease = 3;
                    break;

                case ItemRarity.Legendary:
                    speedIncrease = 4;
                    break;
            }

            playerMgr.baseSpeed = playerMgr.baseSpeed + speedIncrease;
            Destroy(gameObject);
        }
    }
}
