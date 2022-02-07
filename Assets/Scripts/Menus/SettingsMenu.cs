using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;


public class SettingsMenu : MonoBehaviour
{
    public AudioMixer sfxMixer;
    public AudioMixer musicMixer;
    public Slider sfxSlider;
    public Slider musicSlider;

    private float realSFXVolume;
    private float realMusicVolume;



    /*private void Start()
    {
        sfxSlider.value = PlayerPrefs.GetFloat("SavedSFXVolume", 0.75f);
        sfxMixer.SetFloat("volume", PlayerPrefs.GetFloat("SavedSFXVolume"));
    } */

    //Change volume of SFX with slider
    public void SetSFXVolume(float sfxVolume)
    {
        realSFXVolume = Mathf.Log10(sfxVolume) * 20;
        sfxMixer.SetFloat("volume", realSFXVolume);
        //PlayerPrefs.SetFloat("SavedSFXVolume", realSFXVolume);
        //PlayerPrefs.Save();
    }


    //Change volume of music with slider
    public void SetMusicVolume(float musicVolume)
    {
        realMusicVolume = Mathf.Log10(musicVolume) * 20;
        musicMixer.SetFloat("volume", realMusicVolume);
        //PlayerPrefs.SetFloat("SavedMusicVolume", realMusicVolume);
        //PlayerPrefs.Save();
    }

}
