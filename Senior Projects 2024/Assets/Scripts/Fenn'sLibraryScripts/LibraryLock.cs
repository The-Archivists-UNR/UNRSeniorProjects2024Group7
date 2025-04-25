using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LibraryLock : MonoBehaviour
{

    bool isFantasyCompleted = false;
    bool isNoireCompleted = false;
    bool isScifiCompeleted = false;
    [HideInInspector]
    public bool isBossOpen = false;
    
    public static LibraryLock inst;

    void Awake()
    {
        if (inst == null)
        {
            inst = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
            Destroy(this);
    }

    private void checkValidity()
    {
        if (isFantasyCompleted == true && isNoireCompleted == true && isScifiCompeleted == true)
            isBossOpen = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
