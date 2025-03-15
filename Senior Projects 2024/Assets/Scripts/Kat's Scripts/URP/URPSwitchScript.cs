using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class URPSwitchScript : MonoBehaviour
{
    public RenderPipelineAsset NoireURP;
    public RenderPipelineAsset TempLibraryURP;
    public RenderPipelineAsset FantasyURP;
    //public RenderPipelineAsset SciFiURP;
    public void SwitchToNoire()
    {
        GraphicsSettings.renderPipelineAsset = NoireURP;
    }

    public void SwitchToLibrary()
    {
        GraphicsSettings.renderPipelineAsset = TempLibraryURP;
    }

    public void SwitchToFantasy()
    {
        GraphicsSettings.renderPipelineAsset = FantasyURP;
    }

    //public void SwitchToSciFi()
    //{
    //    GraphicsSettings.renderPipelineAsset = SciFiURP;
   // }

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
            SwitchToLibrary();
        }
        else if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "(Scene 3) Fantasy")
        {
            SwitchToFantasy();
        }
        //else if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "(Scene 5) SciFi")
        //{
        // SwitchToSciFi();
        //}
        else
        {
            SwitchToLibrary();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
