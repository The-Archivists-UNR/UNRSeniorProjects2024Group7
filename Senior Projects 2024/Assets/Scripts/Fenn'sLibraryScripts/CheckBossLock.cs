using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckBossLock : MonoBehaviour
{
    public BookSwitch bookSwitch;

    // Start is called before the first frame update
    void Start()
    {
        if (LibraryLock.inst.isBossOpen == true)
        {
            bookSwitch.enabled = true;
        }
        else
        {
            bookSwitch.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider otherColldier)
    {
        if (otherColldier.tag == "Player")
        {
            if (LibraryLock.inst.isBossOpen == true)
            {
                bookSwitch.enabled = true;
            }
            else
            {
                bookSwitch.enabled = false;
            }
        }
    }
}
