using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loader : MonoBehaviour
{
    // Start is called before the first frame update

    public NPCController kid;
    public NPCController ghost;
    public NPCController detective;

    void Start()
    {
        SaveMgr.inst.kid = kid;
        SaveMgr.inst.ghost = ghost;
        SaveMgr.inst.detective = detective;
        StartCoroutine(Load());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Load()
    {
        yield return new WaitForSeconds(1);
        SaveMgr.inst.LoadData();
    }
}
