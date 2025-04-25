using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLibrary : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AudioMgr.Instance.PlayMusic("Library");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Save()
    {
        SaveMgr.inst.SaveData();
    }
}
