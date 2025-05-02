//Script: Entity.cs
//Contributor: Liam Francisco
//Summary: Class held by all entities (Players or Enemies). Handles aspcets like speed and health
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Entity : MonoBehaviour
{
    public float maxHealth;
    public float health;
    public float speed;
    public Slider healthSlider;
    public Slider easeHealthSlider;
    private float lerpSpeed = 0.05f;
    public List<Weapon> weapons;
    public Animator enemyAni;
    public string damageSoundID;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        healthSlider.value = health;
        healthSlider.maxValue = maxHealth;
        easeHealthSlider.value = health;
        easeHealthSlider.maxValue = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if(healthSlider != null)
        {
            if (healthSlider.value != health)
            {
                healthSlider.value = health;
            }

            if (healthSlider.value != easeHealthSlider.value)
            {
                easeHealthSlider.value = Mathf.Lerp(easeHealthSlider.value, health, lerpSpeed);
            }

        }
    }

    public virtual void TakeDamage(float damage)
    {
        health -= damage;
        health = Mathf.Min(health, maxHealth);
        AudioMgr.inst.PlaySFX(damageSoundID, AudioMgr.inst.sfxSource);
        if (health <= 0){
            //enemyAni.SetTrigger("die");
            if (tag != "Player")
                GameStatsMgr.inst.enemiesKilled++;
            Destroy(gameObject);
        }
        //else
            //enemyAni.SetTrigger("damage"); 
    }

    public void PaperCrown(float avalue)
    {
        //this might need to be in combat
    }

    public void PocketWatch(float svalue)
    {
        svalue = 1;
        speed += svalue;

    }

    public void LeadMan(float dvalue)
    {
        dvalue = 10;
        //damage -= dvalue;
    }

    public void Crystal(float mhvalue)
    {
        mhvalue = 30;
        maxHealth += mhvalue;
    }
}
