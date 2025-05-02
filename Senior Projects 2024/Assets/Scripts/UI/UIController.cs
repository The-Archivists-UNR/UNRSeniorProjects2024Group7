using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
public class UIController : MonoBehaviour
{
    public Slider _musicSlider, _sfxSlider;
    public AudioMixer mixer;

    private void Start()
    {
        if (PlayerPrefs.HasKey("musicVolume"))
        {
            LoadVolume();
        }
        else
        {
            MusicVolume();
        }

        if (PlayerPrefs.HasKey("sfxVolume"))
        {
            LoadVolume();
        }
        else
        {
            SfxVolume();
        }

    }
    public void ToggleMusic()
    {
        AudioMgr.inst.ToggleMusic();
    }
 
    public void ToggleSFX()
    {
        AudioMgr.inst.ToggleSFX();
    }
    public void MusicVolume()
    {
        float volume = _musicSlider.value;
        AudioMgr.inst.MusicVolume(_musicSlider.value);
        PlayerPrefs.SetFloat("musicVolume", volume);
    }

    public void SfxVolume()
    {
        float volume = _sfxSlider.value;
        AudioMgr.inst.sfxVolume(_sfxSlider.value);
        PlayerPrefs.SetFloat("sfxVolume", volume);
    }


    private void LoadVolume()
    {
        _musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        _sfxSlider.value = PlayerPrefs.GetFloat("sfxVolume");
        MusicVolume();
        SfxVolume();
    }
}
