//Script: AOEWeapon.cs
//Contributor: Liam Francisco
//Summary: Handles the weapon for the "AOE" enemy type
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class AOEWeapon : Weapon
{
    // Start is called before the first frame update
    public GameObject projectile;
    AOE entity;
    public string attackSound;

    void Start()
    {
        entity = GetComponentInParent<AOE>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //creates the projectile GameObject and sets its velocity based on player position
    public override void StartAttack()
    {
        GameObject newProjectile = Instantiate(projectile, transform.position, Quaternion.identity);
        AOEProjectile proj = newProjectile.GetComponent<AOEProjectile>();
        Transform player;
        if (entity != null)
        {
            player = entity.player;
        }
        else
        {
            player = PlayerMgr.inst.player.transform;
        }
        proj.player = player;
        proj.direction = player.position - transform.position;
        proj.direction.Normalize();
        Vector2 startPos = new Vector2(transform.position.x, transform.position.z);
        Vector2 endPos = new Vector2(player.position.x, player.position.z);
        float dist = Vector2.Distance(startPos, endPos);
        float throwAngle = 45 * Mathf.Deg2Rad;
        proj.speed = Mathf.Sqrt(9.81f * dist /Mathf.Sin(2 * throwAngle));
        proj.direction.y = Mathf.Sin(throwAngle);
        proj.direction.Normalize();
        if(attackSound != null)
            AudioMgr.Instance.PlaySFX(attackSound);
    }
}
