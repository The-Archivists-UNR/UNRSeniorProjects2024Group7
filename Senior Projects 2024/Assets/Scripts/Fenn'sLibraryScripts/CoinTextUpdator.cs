using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinTextUpdator : MonoBehaviour
{

    public TextMeshProUGUI coinText;

    // Start is called before the first frame update
    void Start()
    {
        coinText.text = moneyMgr.inst.currency.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateMoneyText()
    {
        coinText.text = moneyMgr.inst.currency.ToString();
    }
}
