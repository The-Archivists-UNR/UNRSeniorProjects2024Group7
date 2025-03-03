using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class saveSelect : MonoBehaviour
{
    public static saveSelect inst;
    public int saveSlotNum = 1;

    public List<GameObject> slots;

    // Start is called before the first frame update
    void Start()
    {
        inst = this;
        UIHover firstSlot = slots[1].GetComponent<UIHover>();
        firstSlot.onClick();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
