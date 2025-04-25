using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckBossLock : MonoBehaviour
{
    public LibraryLock libraryLock;
    public BookSwitch bookSwitch;
    // Start is called before the first frame update
    void Start()
    {
        if (libraryLock.isBossOpen == true)
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
}
