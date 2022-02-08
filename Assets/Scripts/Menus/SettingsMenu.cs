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
        Debug.Log("sfxSlider.value: " + sfxSlider.value);
        sfxMixer.SetFloat("volume", Mathf.Log10(sfxSlider.value) * 20);
    } */

    //Change volume of SFX with slider
    public void SetSFXVolume(float sfxVolume)
    {
        realSFXVolume = Mathf.Log10(sfxVolume) * 20;
        sfxMixer.SetFloat("volume", realSFXVolume);

        //Debug.Log("SFX volume that will be to Player Prefs: " + realSFXVolume);
        //PlayerPrefs.SetFloat("SavedSFXVolume", realSFXVolume);
        //PlayerPrefs.Save();

        //float retrievedSFXVolume = PlayerPrefs.GetFloat("SavedSFXVolume", 0.75f);
        //Debug.Log("SFX volume retrieved from Player Prefs: " + retrievedSFXVolume);
    }


    //Change volume of music with slider
    public void SetMusicVolume(float musicVolume)
    {
        //Debug.Log("In SetMusicVolume");
        realMusicVolume = Mathf.Log10(musicVolume) * 20;
        musicMixer.SetFloat("volume", realMusicVolume);
        //PlayerPrefs.SetFloat("SavedMusicVolume", realMusicVolume);
        //PlayerPrefs.Save();
    }

}
