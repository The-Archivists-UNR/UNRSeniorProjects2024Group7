using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpheliaStats : MonoBehaviour
{
    public PlayerMgr playerMgr;
    public Entity entity;
    public Weapon weapon;

    public float changedHealth;
    public float changedSpeed;
    public float changedDamage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void updateStats()
    {
        entity.maxHealth = changedHealth;
        playerMgr.baseSpeed = playerMgr.baseSpeed * changedSpeed;
        weapon.baseDamage = weapon.baseDamage * changedDamage;
    }
}
