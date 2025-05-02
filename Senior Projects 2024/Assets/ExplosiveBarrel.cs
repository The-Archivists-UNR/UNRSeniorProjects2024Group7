using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class ExplosiveBarrel : Entity
{
    public GameObject explosion;
    public Renderer meshRenderer;
    public float radius;
    public float damage;
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
        AudioMgr.inst.PlaySFX(damageSoundID, AudioMgr.inst.sfxSource);
        if (health <= 0)
        {
            //enemyAni.SetTrigger("die");
            if (tag != "Player")
                GameStatsMgr.inst.enemiesKilled++;
            AudioMgr.inst.PlaySFX("Explosion");
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

            float estimatedLifetime = 2f; 
            yield return new WaitForSeconds(estimatedLifetime);
        }

        GameObject player = GameObject.FindWithTag("Player");
        List<GameObject> enemies = new List<GameObject>(GameObject.FindGameObjectsWithTag("Enemy"));
        enemies.Add(player);
        if(enemies.Contains(this.gameObject))
        {
            enemies.Remove(this.gameObject);
        }

        foreach(GameObject enemy in enemies)
        {
            if (Vector3.Distance(enemy.transform.position, vfx.transform.position) < radius)
                enemy.GetComponent<Entity>().TakeDamage(damage);
        }

        Destroy(vfxObject);
        //GetComponent<BoxCollider>().enabled = false;
        this.gameObject.SetActive(false);
    }
}
