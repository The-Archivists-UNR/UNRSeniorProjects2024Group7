using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BaseMgr : MonoBehaviour
{
    public TextMeshProUGUI attack;
    public TextMeshProUGUI hp;
    public TextMeshProUGUI speed;
    public OpheliaStats stats;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        attack.text = stats.ogDamage.ToString();
        hp.text = stats.ogHP.ToString();
        speed.text = stats.ogSpeed.ToString();
    }
}
