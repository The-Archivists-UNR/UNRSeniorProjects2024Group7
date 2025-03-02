using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : Weapon
{
    public PlayerController entity;
    int numHits;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!entity.attacking)
        {
            numHits = 0;
        }
    }

    private void OnTriggerStay(Collider collider)
    {
        if (collider.gameObject.tag.Equals(damageTag) && entity.attacking && numHits == 0)
        {
            numHits++;
            collider.GetComponent<Entity>().TakeDamage(baseDamage);
            Debug.Log("player hit enemy");
        }
    }
}
