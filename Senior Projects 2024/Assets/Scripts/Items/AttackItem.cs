using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Author: Fenn Edmonds

public class AttackItem : MonoBehaviour
{
    public Weapon weapon;
    public int damageIncrease;
    public ItemRarity rarity;
    private float maxHeight;
    private float minHeight;
    private float xPos;
    private bool active = true;

    // Start is called before the first frame update
    void Start()
    {
        //gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(active == true)
        {
            // Rotate the object around its local z axis at 1 degree per second
            Vector3 direction = new Vector3(0, 0, 20);
            Quaternion targetRotation = Quaternion.Euler(direction);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, Time.deltaTime * 20f);
            Debug.Log("This is working");

            // Translate object up and down on y axis
        }
    }

    void FindPos()
    {
        xPos = gameObject.transform.position.x;
        maxHeight = xPos + 10;
        minHeight = xPos - 10;
    }

    public void ItemAppear()
    {
        gameObject.SetActive(true);
        FindPos();
        active = true;
    }

    public void CollectItem()
    {
        switch (rarity)
        {
            case ItemRarity.Common:
                damageIncrease = 3;
            break;

            case ItemRarity.Rare:
                damageIncrease = 5;
                break;

            case ItemRarity.Epic:
                damageIncrease = 7;
                break;

            case ItemRarity.Legendary:
                damageIncrease = 10;
                break;
        }

        weapon.baseDamage = weapon.baseDamage + damageIncrease;
        gameObject.SetActive(false);
    }
}
