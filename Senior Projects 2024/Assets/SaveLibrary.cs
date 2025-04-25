using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLibrary : MonoBehaviour
{
    public NPCController kid;
    public NPCController ghost;
    public NPCController detective;
    public NPCController dude;
    // Start is called before the first frame update
    void Start()
    {
        AudioMgr.Instance.PlayMusic("Library");
        SaveMgr.inst.kid = kid;
        SaveMgr.inst.ghost = ghost;
        SaveMgr.inst.dude = dude;
        SaveMgr.inst.detective = detective;
        if (!SaveMgr.inst.gameData.savedBefore)
        {
            SaveMgr.inst.gameData.savedBefore = true;
        }
        else
        {
            SaveMgr.inst.LoadData();
        }
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
