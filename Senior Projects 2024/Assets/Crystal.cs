using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : Entity
{
    bool hittable = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MakeHitable()
    {
        easeHealthSlider.gameObject.SetActive(true);
        healthSlider.gameObject.SetActive(true);
        hittable = true;
    }

    public override void TakeDamage(float damage)
    {
        if (hittable)
        {
            health -= damage;
            health = Mathf.Min(health, maxHealth);
            AudioMgr.inst.PlaySFX(damageSoundID, AudioMgr.inst.sfxSource);
            if (health <= 0)
            {
                FinalBossController.inst.IncreaseWaveFromCrystal();
                Destroy(gameObject);
            }
        }
    }
}
