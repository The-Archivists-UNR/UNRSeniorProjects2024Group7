using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Author: Fenn Edmonds
public class CoinScript : MonoBehaviour
{

    private int coinIncrease;
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
        child = gameObject.transform.GetChild(0).gameObject;
    }

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
        if (collision.gameObject.tag == "Player")
        {
            switch (rarity)
            {
                case ItemRarity.Common:
                    coinIncrease = 5;
                    break;

                case ItemRarity.Rare:
                    coinIncrease = 10;
                    break;

                case ItemRarity.Epic:
                    coinIncrease = 20;
                    break;

                case ItemRarity.Legendary:
                    coinIncrease = 30;
                    break;
            }

            moneyMgr.inst.UpdateMoney(coinIncrease);
            Destroy(gameObject);
        }
    }
}
