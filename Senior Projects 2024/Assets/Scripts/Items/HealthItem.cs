using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthItem : MonoBehaviour
{
    public PlayerMgr playerMgr;
    public Entity entity;
    private float healthRestore;
    public ItemRarity rarity;
    private Vector3 initPos;
    private float amp = .5f;
    private float freq = 3f;
    private GameObject child;
    private bool active = true;


    // Start is called before the first frame update
    void Start()
    {
        //gameObject.SetActive(false);
        initPos = transform.position;
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
                    healthRestore = 10;
                break;

                case ItemRarity.Rare:
                    healthRestore = 25;
                    break;

                case ItemRarity.Epic:
                    healthRestore = 50;
                    break;

                case ItemRarity.Legendary:
                    healthRestore = 70;
                    break;
            }

            entity.maxHealth = entity.maxHealth + healthRestore;
            entity.health = entity.health + healthRestore;
            Destroy(gameObject);
            //child.SetActive(false);
        }
    }
}
