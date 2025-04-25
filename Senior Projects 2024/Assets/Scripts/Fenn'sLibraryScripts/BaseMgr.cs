using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BaseMgr : MonoBehaviour
{
    public TextMeshProUGUI attack;
    public TextMeshProUGUI hp;
    public TextMeshProUGUI speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        attack.text = OpheliaStats.inst.ogDamage.ToString();
        hp.text = OpheliaStats.inst.ogHP.ToString();
        speed.text = OpheliaStats.inst.ogSpeed.ToString();
    }
}
