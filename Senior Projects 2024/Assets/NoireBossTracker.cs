using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTracker : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject boss;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (boss == null)
        {
            NewGameMgr.inst.bossDead = true;
            if (LibraryLock.inst != null)
            {
                LibraryLock.inst.isNoireCompleted = true;
            }
        }
    }
}
