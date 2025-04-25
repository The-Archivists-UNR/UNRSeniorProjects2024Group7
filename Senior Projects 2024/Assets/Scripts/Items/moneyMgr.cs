using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

//Author: Fenn Edmonds

public class moneyMgr : MonoBehaviour
{

    public int currency;
    //public TextMeshProUGUI moneyText;
    public static moneyMgr inst;
    // Start is called before the first frame update
    void Awake()
    {
        if (inst == null)
        {
            inst = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
            Destroy(this);
        //moneyText.text = "Tokens: " + currency;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateMoney(int newMoney)
    {
        currency = currency + newMoney;
        //moneyText.text = "Tokens: " + currency;
    }
}
