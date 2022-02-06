using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer SFXMixer;
    public AudioMixer MusicMixer;

    //Change volume of SFX with slider
    public void SetSFXVolume(float SFXVolume)
    {
        SFXMixer.SetFloat("volume", Mathf.Log10 (SFXVolume) * 20);
    }

    //Change volume of music with slider
    public void SetMusicVolume(float MusicVolume)
    {
        MusicMixer.SetFloat("volume", MusicVolume);
    }

}
