using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UtilsNS;


public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private AudioMixer sfxMixer;
    [SerializeField]
    private AudioMixer musicMixer;
    [SerializeField]
    private Slider sfxPortraitSlider;
    [SerializeField]
    private Slider musicPortraitSlider;
    [SerializeField]
    private GameObject mainMenuPortraitScreen;
    [SerializeField]
    private GameObject settingsMenuPortraitScreen;
    [SerializeField]
    private Slider sfxLandscapeSlider;
    [SerializeField]
    private Slider musicLandscapeSlider;
    [SerializeField]
    private GameObject mainMenuLandscapeScreen;
    [SerializeField]
    private GameObject settingsMenuLandscapeScreen;

    [SerializeField]
    private GameObject portraitCanvas;
    [SerializeField]
    private GameObject landscapeCanvas;

    private DeviceOrientation currentOrientation, lastOrientation;

    private float sfxSliderValue;
    private float musicSliderValue;

    private Utils utils;


    void Start()
    {
        currentOrientation = lastOrientation = Input.deviceOrientation;
        Screen.orientation = ScreenOrientation.AutoRotation;
        utils.OrientationChanged(currentOrientation, ref portraitCanvas, ref landscapeCanvas);
        LoadSettings();
    }

	private void Update()
	{
        currentOrientation = Input.deviceOrientation;
        if (currentOrientation != lastOrientation)
		{
            utils.OrientationChanged(currentOrientation, ref portraitCanvas, ref landscapeCanvas);
            lastOrientation = currentOrientation;
        }
    }

	void LoadSettings()
    {
        // Get the currently saved slider values
        sfxSliderValue = PlayerPrefs.GetFloat("sfxSliderValue", 1f);
        musicSliderValue = PlayerPrefs.GetFloat("musicSliderValue", 1f);

        sfxLandscapeSlider.value = sfxPortraitSlider.value = sfxSliderValue;
        musicLandscapeSlider.value = musicPortraitSlider.value = musicSliderValue;

        sfxMixer.SetFloat("volume", Mathf.Log10(sfxSliderValue) * 20);
        musicMixer.SetFloat("volume", Mathf.Log10(musicSliderValue) * 20);
    }

    public void SetSFXVolume(float sfxSliderValue)
    {
        sfxMixer.SetFloat("volume", Mathf.Log10(sfxSliderValue) * 20);
        PlayerPrefs.SetFloat("sfxSliderValue", sfxSliderValue);
    }

    public void SetMusicVolume(float musicSliderValue)
    {
        musicMixer.SetFloat("volume", Mathf.Log10(musicSliderValue) * 20);
        PlayerPrefs.SetFloat("musicSliderValue", musicSliderValue);
    }

    public void EnableLandscapeMainMenu()
    {
        settingsMenuLandscapeScreen.SetActive(false);
        mainMenuLandscapeScreen.SetActive(true);
    }       
    public void EnableLandscapeSettingsMenu()
    {
        mainMenuLandscapeScreen.SetActive(false);
        settingsMenuLandscapeScreen.SetActive(true);
        LoadSettings();
    }
    public void EnablePortraitMainMenu()
    {
        settingsMenuPortraitScreen.SetActive(false);
        mainMenuPortraitScreen.SetActive(true);
    }
    public void EnablePortraitSettingsMenu()
    {
        mainMenuPortraitScreen.SetActive(false);
        settingsMenuPortraitScreen.SetActive(true);
        LoadSettings();
    }
}
