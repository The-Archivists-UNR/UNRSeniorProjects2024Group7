using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class URPSwitchScript : MonoBehaviour
{
    public RenderPipelineAsset NoireURP;
    public void SwitchToNoire()
    {
        GraphicsSettings.renderPipelineAsset = NoireURP;
    }

    public void SwitchToStandard()
    {
        GraphicsSettings.renderPipelineAsset = null;
    }
    // Start is called before the first frame update
    void Start()
    {
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "(Scene 4) Noire")
        {
            SwitchToNoire();
        }
        else if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "(Scene 2) Library")
        {
            SwitchToStandard();
        }
        else
        {
            SwitchToStandard();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
