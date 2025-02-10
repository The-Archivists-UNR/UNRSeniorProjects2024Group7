using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;

public class GameStatsMgr : MonoBehaviour
{
    public int enemiesKilled;
    public static GameStatsMgr inst;
    // Start is called before the first frame update
    void Awake()
    {
        if(inst == null)
        {
            inst = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
            Destroy(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
