using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Author: Fenn Edmonds 

public class itemMgr : MonoBehaviour
{

    public List<GameObject> items;
    public List<GameObject> spawnedItems;
    public Entity entity;
    public PlayerMgr playerMgr;

    ItemRarity assignedRarity;
    Color assignedColor;
    // [HideInInspector]
    // public AttackItem attackItem;

    // [HideInInspector]
    // public HealthItem healthItem;

    // [HideInInspector]
    // public SpeedItem speedItem;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public GameObject ChooseItem()
    {
        int chosenRarity = Random.Range(1, 41);
        // ItemRarity assignedRarity;
        int chosenItem = Random.Range(0, 4);
        if (chosenRarity <= 20)
        {
            assignedRarity = ItemRarity.Common;
            assignedColor = Color.green;
        }
        if (chosenRarity >= 21 && chosenRarity <= 30)
        {
            assignedRarity = ItemRarity.Rare;
            assignedColor = Color.blue;
        }
        if (chosenRarity >= 31 && chosenRarity <= 37)
        {
            assignedRarity = ItemRarity.Epic;
            assignedColor = Color.magenta;
        }
        if (chosenRarity >= 38 && chosenRarity <= 40)
        {   
            assignedRarity = ItemRarity.Legendary;
            assignedColor = Color.yellow;
        }    

        switch(chosenItem)
        {
            case 0:
                AttackItem attackItem = items[0].GetComponent<AttackItem>();
                attackItem.rarity = assignedRarity;
                GameObject child = attackItem.transform.GetChild(0).gameObject;
                SpriteRenderer renderer = child.GetComponent<SpriteRenderer>();
                renderer.color = assignedColor;
                return items[0];
            case 1:
                SpeedItem speedItem = items[1].GetComponent<SpeedItem>();
                speedItem.rarity = assignedRarity;
                GameObject child1 = speedItem.transform.GetChild(0).gameObject;
                SpriteRenderer renderer1 = child1.GetComponent<SpriteRenderer>();
                renderer1.color = assignedColor;
                return items[1];
            case 2:
                HealthItem heartItem = items[2].GetComponent<HealthItem>();
                heartItem.rarity = assignedRarity;
                GameObject child2 = heartItem.transform.GetChild(0).gameObject;
                SpriteRenderer renderer2 = child2.GetComponent<SpriteRenderer>();
                renderer2.color = assignedColor;
                return items[2];
            case 3:
                CoinScript coinItem = items[3].GetComponent<CoinScript>();
                coinItem.rarity = assignedRarity;
                GameObject child3 = coinItem.transform.GetChild(0).gameObject;
                SpriteRenderer renderer3 = child3.GetComponent<SpriteRenderer>();
                renderer3.color = assignedColor;
                return items[3];

        }

        return null;

        
    }
}
