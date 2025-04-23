using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class ExplosiveBarrel : Entity
{
    public GameObject explosion;
    public Renderer meshRenderer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void TakeDamage(float damage)
    {
        health -= damage;
        health = Mathf.Min(health, maxHealth);
        AudioMgr.Instance.PlaySFX(damageSoundID, AudioMgr.Instance.sfxSource);
        if (health <= 0)
        {
            //enemyAni.SetTrigger("die");
            if (tag != "Player")
                GameStatsMgr.inst.enemiesKilled++;
            StartCoroutine(PlayVFXGraphAndDestroy(explosion));
        }
    }

    public IEnumerator PlayVFXGraphAndDestroy(GameObject vfxObject)
    {
        VisualEffect vfx = vfxObject.GetComponent<VisualEffect>();
        meshRenderer.enabled = false;

        if (vfx != null)
        {
            vfx.Play();

            if (vfx != null)
            {
                vfx.Play();

                float estimatedLifetime = 2f; 
                yield return new WaitForSeconds(estimatedLifetime);
            }

            Destroy(vfxObject);
        }
    }
}
