using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstNPCRelation : MonoBehaviour
{

    public static InstNPCRelation inst;

    public JulieBuff jBuff;
    public SamBuff sBuff;
    public KirkBuff kBuff;
    public BonnieBuff bBuff;

    // Awake is called before the first frame update
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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
