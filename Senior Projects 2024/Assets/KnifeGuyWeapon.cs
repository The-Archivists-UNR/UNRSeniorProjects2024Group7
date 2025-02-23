using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeGuyWeapon : Weapon
{
    public float attackingTime;
    public NoireKnifeAI entity;
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

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag.Equals("Player") && entity.attacking && numHits == 0)
        {
            numHits++;
            Debug.Log("hit");
        }
    }
}
