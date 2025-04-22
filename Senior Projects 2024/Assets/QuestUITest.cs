using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class QuestUITest : MonoBehaviour
{

    public PanelMover QuestUI;
    public GameObject UIPanel;
    public bool testing = true;
    public Canvas myCanvas;
    Vector3 expectedPos;
    // Start is called before the first frame update
    void Start()
    {
        expectedPos = myCanvas.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(testing)
        {
            if(QuestUI.isVisible == true)
            {
                if(UIPanel.transform.position == expectedPos)
                {
                    Debug.Log("Correct Display");
                    testing = false;
                }
                else
                {
                    Debug.Log("Error");
                    testing = false;
                }
            }
        }
    }
}
