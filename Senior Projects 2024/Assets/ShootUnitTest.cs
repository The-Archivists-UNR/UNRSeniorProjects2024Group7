using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootUnitTest : MonoBehaviour
{
    public GameObject tommyGunEnemy;
    public float timer = 0;
    public bool testing = true;
    
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(tommyGunEnemy).SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (testing)
        {
            timer += Time.deltaTime;
            if(PlayerMgr.inst.player.health < PlayerMgr.inst.player.maxHealth)
            {
                Debug.Log("Hit");
                testing = false;
            }
            if(timer > 10)
            {
                Debug.Log("fail");
                testing = false;
            }
        }
    }
}
