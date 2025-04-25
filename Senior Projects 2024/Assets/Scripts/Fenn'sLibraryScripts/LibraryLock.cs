using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LibraryLock : MonoBehaviour
{

    public bool isFantasyCompleted = false;
    public bool isNoireCompleted = false;
    public bool isScifiCompleted = false;
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
        if (isFantasyCompleted == true && isNoireCompleted == true && isScifiCompleted == true)
            isBossOpen = true;
    }

    public void UnlockNoire()
    {
        isNoireCompleted = true;
    }

    public void UnlockScifi()
    {
        isScifiCompleted = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
