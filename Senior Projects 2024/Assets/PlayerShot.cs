using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShot : SniperWeapon
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void StartAttack()
    {
        GameObject newProjectile = Instantiate(projectile, spawn.position, Quaternion.identity);
        Vector3 shot = spawn.transform.position + spawn.transform.forward;
        shot.y = spawn.position.y;
        newProjectile.GetComponent<Projectile>().direction = shot - spawn.position;
        if (attackSound != null)
            AudioMgr.inst.PlaySFX(attackSound);
    }
}
