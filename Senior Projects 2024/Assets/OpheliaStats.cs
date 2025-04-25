using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Author: Fenn Edmonds

public class OpheliaStats : MonoBehaviour
{

    float savedAttackStat;
    float savedSpeedStat;
    float savedHealthStat;

    public float ogHP = 100;
    public float ogSpeed = 15;
    public float ogDamage = 15;

    public float HpPercent = 1 ;
    public float SpeedPercent = 1;
    public float AttackPercent = 1;

    public static OpheliaStats inst;

    // Awake is called before the first frame update
    void Awake()
    {
        if (inst == null)
        {
            inst = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
            Destroy(this);
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void AppplyStats(Entity player, Weapon weapon)
    {
        player.maxHealth = ogHP * HpPercent;
        player.health = player.maxHealth;
        player.speed = ogSpeed * SpeedPercent;
        weapon.baseDamage = ogDamage * AttackPercent;

    }
}
