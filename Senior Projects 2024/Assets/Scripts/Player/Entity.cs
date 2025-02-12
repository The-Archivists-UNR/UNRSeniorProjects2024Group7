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

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if(healthSlider.value != health)
        {
            healthSlider.value = health;
        }

        if(healthSlider.value != easeHealthSlider.value)
        {
            easeHealthSlider.value = Mathf.Lerp(easeHealthSlider.value, health, lerpSpeed);
        }

    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health < 0){
            enemyAni.SetTrigger("die");
            Destroy(gameObject);
        }
        
        else
            enemyAni.SetTrigger("damage");
    }
}
