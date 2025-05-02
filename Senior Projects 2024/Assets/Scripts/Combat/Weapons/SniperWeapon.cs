//Script: SniperWeapon.cs
//Contributor: Liam Francisco
//Summary: Handles the weapon for the "Sniper" enemy type
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperWeapon : Weapon
{
    public GameObject projectile; // prefab of the snipe projectile
    public Transform spawn; // spawn location of projectiles
    public string attackSound; // sound made when weapon is shot

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    //spawns projectile and sets dirction
    public override void StartAttack()
    {
        GameObject newProjectile = Instantiate(projectile, spawn.position, Quaternion.identity);
        Vector3 shot = PlayerMgr.inst.player.transform.position;
        shot.y = spawn.position.y;
        newProjectile.GetComponent<Projectile>().direction = shot - spawn.position;
        if(attackSound != null)
            AudioMgr.inst.PlaySFX(attackSound);
    }
}
