using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UtilsNS;

namespace MenuNS
{
    public class Menu : MonoBehaviour
    {
        public AudioMixer sfxMixer;
        public AudioMixer musicMixer;
        [SerializeField]
        private Slider sfxPortraitSlider;
        [SerializeField]
        private Slider musicPortraitSlider;
        [SerializeField]
        private Slider sfxLandscapeSlider;
        [SerializeField]
        private Slider musicLandscapeSlider;

        public GameObject mainMenuPortraitCanvas;
        public GameObject mainMenuLandscapeCanvas;
        public GameObject settingsPortraitCanvas;
        public GameObject settingsLandscapeCanvas;

        public GameObject parentPortraitCanvas;
        public GameObject parentLandscapeCanvas;

        private GameObject portrait;
        private GameObject landscape;

        private float sfxSliderValue;
        private float musicSliderValue;

        private Utils utils;


		private void Start()
        {
            utils = new Utils();
            EnableMainMenu(true);
            SetActiveCanvas();
            LoadSettings();
        }

        private void Update()
	    {
            SetActiveCanvas();
        }

        public void LoadSettings()
        {
            // Get the currently saved slider values
            sfxSliderValue = PlayerPrefs.GetFloat("sfxSliderValue", 1f);
            musicSliderValue = PlayerPrefs.GetFloat("musicSliderValue", 1f);

            sfxMixer.SetFloat("volume", Mathf.Log10(sfxSliderValue) * 20);
            musicMixer.SetFloat("volume", Mathf.Log10(musicSliderValue) * 20);

            sfxLandscapeSlider.value = sfxPortraitSlider.value = sfxSliderValue;
            musicLandscapeSlider.value = musicPortraitSlider.value = musicSliderValue;
        }

        void SetActiveCanvas()
		{   
            if (mainMenuPortraitCanvas.activeInHierarchy || mainMenuLandscapeCanvas.activeInHierarchy)
            {
                EnableMainMenu(false);
            }
            else
            {
                EnableSettings(false);
                LoadSettings();
            }
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

        public void EnableMainMenu(bool buttonPressed)
        {
            portrait = mainMenuPortraitCanvas;
            landscape = mainMenuLandscapeCanvas;

            settingsPortraitCanvas.SetActive(false);
            settingsLandscapeCanvas.SetActive(false);

            if (utils.CheckIfOrientationUpdated(portrait, landscape, buttonPressed))
			{
                utils.CheckIfOrientationUpdated(parentPortraitCanvas, parentLandscapeCanvas, true);
            }
        }

        public void EnableSettings(bool buttonPressed)
		{
            portrait = settingsPortraitCanvas;
            landscape = settingsLandscapeCanvas;

            mainMenuPortraitCanvas.SetActive(false);
            mainMenuLandscapeCanvas.SetActive(false);

            if (utils.CheckIfOrientationUpdated(portrait, landscape, buttonPressed))
            {
                utils.CheckIfOrientationUpdated(parentPortraitCanvas, parentLandscapeCanvas, true);
            }
        }
    }
}
