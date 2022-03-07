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
            EnableMainMenu();
            SetProperCanvas();
            SwitchParentCanvases();
            LoadSettings();
        }

        private void Update()
	    {
            SetProperCanvas();
            SwitchParentCanvases();
        }

        void SetProperCanvas()
		{
            if (IsMainMenuActive())
            {
                portrait = mainMenuPortraitCanvas;
                landscape = mainMenuLandscapeCanvas;
            }
            else
            {
                portrait = settingsPortraitCanvas;
                landscape = settingsLandscapeCanvas;
            }
            utils.UpdateOrientation(portrait, landscape);
            LoadSettings();
        }
    
        private bool IsMainMenuActive()
		{
            if (mainMenuPortraitCanvas.activeInHierarchy == true || mainMenuLandscapeCanvas.activeInHierarchy == true)
			{
                return true;
			}
			else
			{
                return false;
			}
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

        public void EnableSettings()
		{
            portrait = settingsPortraitCanvas;
            landscape = settingsLandscapeCanvas;

            mainMenuPortraitCanvas.SetActive(false);
            mainMenuLandscapeCanvas.SetActive(false);

            utils.SetActiveOrientation(portrait, landscape);
        }

        public void EnableMainMenu()
		{
            portrait = mainMenuPortraitCanvas;
            landscape = mainMenuLandscapeCanvas;

            settingsPortraitCanvas.SetActive(false);
            settingsLandscapeCanvas.SetActive(false);

            utils.SetActiveOrientation(portrait, landscape);
        }

        void SwitchParentCanvases()
		{
            utils.SetActiveOrientation(parentPortraitCanvas, parentLandscapeCanvas);
        }
    }
}
