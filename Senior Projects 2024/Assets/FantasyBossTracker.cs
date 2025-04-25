using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FantasyBossTracker : MonoBehaviour
{
    public NewRoom NewRoom;
    bool added = false;
    GameObject boss;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (NewRoom.enemies.Count > 0 && !added)
        {
            boss = NewRoom.enemies[0];
            added = true;
        }

        if (boss == null && added)
        {
            NewGameMgr.inst.bossDead = true;
            if (LibraryLock.inst != null)
            {
                LibraryLock.inst.isFantasyCompleted = true;
            }
        }

    }
}
