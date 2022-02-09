using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;


public class SettingsMenu : MonoBehaviour
{
    [SerializeField]
    private AudioMixer sfxMixer;
    [SerializeField]
    private AudioMixer musicMixer;
    [SerializeField]
    private Slider sfxSlider;
    [SerializeField]
    private Slider musicSlider;
    [SerializeField]
    private GameObject mainMenuScreen;
    [SerializeField]
    private GameObject settingsMenuScreen;

    public float sfxSliderValue;
    public float musicSliderValue;
    private float savedSFXVolume;
    private float savedMusicVolume;


    private void Start()
    {
        LoadValues();
        mainMenuScreen.SetActive(true);
        settingsMenuScreen.SetActive(false);
    }

    void LoadValues()
    {
        savedSFXVolume = PlayerPrefs.GetFloat("sfxSavedVolume", sfxSliderValue);
        savedMusicVolume = musicSlider.value = PlayerPrefs.GetFloat("musicSavedVolume", musicSliderValue);
        sfxSlider.value = savedSFXVolume;
        musicSlider.value = savedMusicVolume;
        sfxMixer.SetFloat("volume", Mathf.Log10(savedSFXVolume) * 20);
        musicMixer.SetFloat("volume", Mathf.Log10(savedMusicVolume) * 20);
    }

    public void SetSFXVolume(float sfxVolume)
    {
        sfxSliderValue = sfxVolume;
        sfxMixer.SetFloat("volume", Mathf.Log10(sfxVolume) * 20);
        PlayerPrefs.SetFloat("sfxSavedVolume", sfxSliderValue);
    }

    public void SetMusicVolume(float musicVolume)
    {
        musicSliderValue = musicVolume;
        musicMixer.SetFloat("volume", Mathf.Log10(musicVolume) * 20);
        PlayerPrefs.SetFloat("musicSavedVolume", musicSliderValue);
    }

    public void EnableMainMenu()
    {
        settingsMenuScreen.SetActive(false);
        mainMenuScreen.SetActive(true);
    }       
    public void EnableSettingsMenu()
    {
        mainMenuScreen.SetActive(false);
        settingsMenuScreen.SetActive(true);
    }
}
