using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuSave : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeName(string name)
    {
       SaveMgr.inst.SetPlayerName(name);
    }
}
