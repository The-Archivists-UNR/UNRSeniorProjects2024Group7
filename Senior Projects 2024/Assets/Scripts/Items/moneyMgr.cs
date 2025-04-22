using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

//Author: Fenn Edmonds

public class moneyMgr : MonoBehaviour
{

    public int currency;
    public TextMeshProUGUI moneyText;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        moneyText.text = "Tokens: " + currency;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateMoney(int newMoney)
    {
        currency = currency + newMoney;
        moneyText.text = "Tokens: " + currency;
    }
}
