using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;
using UtilsNS;

namespace MenuNS
{
    public class Menu : MonoBehaviour
    {
        public AudioMixer sfxMixer;

        public AudioMixer musicMixer;

        [SerializeField]
        private Toggle portraitSFXToggle;

        [SerializeField]
        private Toggle landscapeSFXToggle;

        [SerializeField]
        private Toggle portraitMusicToggle;

        [SerializeField]
        private Toggle landscapeMusicToggle;

        [SerializeField]
        private TMP_Dropdown questionsPerGamePortraitDropdown;

        [SerializeField]
        private TMP_Dropdown questionsPerGameLandscapeDropdown;


        public GameObject mainMenuPortraitCanvas;

        public GameObject mainMenuLandscapeCanvas;

        public GameObject settingsPortraitCanvas;
         
        public GameObject settingsLandscapeCanvas;


        public GameObject parentPortraitCanvas;

        public GameObject parentLandscapeCanvas;


        private GameObject portrait;

        private GameObject landscape;


        private int sfxToggle;

        private int musicToggle;

        public static int questionsPerGameDropdownValue;


        private Utils utils;


        private void Start()
        {
            utils = new Utils();
            EnableMainMenu(true);
            SetActiveCanvas();
            LoadSettings();
            SetMixers();
        }

        private void Update()
        {
            SetActiveCanvas();
        }

        public void LoadSettings()
        {
            sfxToggle = PlayerPrefs.GetInt("sfxToggle", 1);
            musicToggle = PlayerPrefs.GetInt("musicToggle", 1);
            questionsPerGameDropdownValue = PlayerPrefs.GetInt("questionsPerGameDropdownValue", 10);
        }

        public void SetMixers()
        {
            if (sfxToggle == 0)
            {
                sfxMixer.SetFloat("volume", -80);
            }
            else
            {
                sfxMixer.SetFloat("volume", 0);
            }

            if (musicToggle == 0)
            {
                musicMixer.SetFloat("volume", -80);
            }
            else
            {
                musicMixer.SetFloat("volume", 0);
            }
        }

        public void RefreshSettingsUI()
        {
            if (sfxToggle == 0)
			{
                if (Utils.currentOrientation == ScreenOrientation.Portrait)
                {
                    portraitSFXToggle.isOn = false;
                }
                else
                {
                    landscapeSFXToggle.isOn = false;
                }
            }
            else
			{
                if (Utils.currentOrientation == ScreenOrientation.Portrait)
                {
                    portraitSFXToggle.isOn = true;
                }
                else
                {
                    landscapeSFXToggle.isOn = true;
                }
            }

            if (musicToggle == 0)
			{
                if (Utils.currentOrientation == ScreenOrientation.Portrait)
                {
                    portraitMusicToggle.isOn = false;
                }
                else
                {
                    landscapeMusicToggle.isOn = false;
                }
            }
            else
			{
                if (Utils.currentOrientation == ScreenOrientation.Portrait)
                {
                    portraitMusicToggle.isOn = true;
                }
                else
                {
                    landscapeMusicToggle.isOn = true;
                }
            }

            questionsPerGamePortraitDropdown.value = questionsPerGameLandscapeDropdown.value = questionsPerGameDropdownValue;
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
                RefreshSettingsUI();
            }
        }

        public void SetSFXToggle(bool state)
        {
            if (state)
			{
                sfxToggle = 1;
                sfxMixer.SetFloat("volume", 0);
                PlayerPrefs.SetInt("sfxToggle", 1);
            }
            else
			{
                sfxToggle = 0;
                sfxMixer.SetFloat("volume", -80);
                PlayerPrefs.SetInt("sfxToggle", 0);
            }
        }

        public void SetMusicToggle(bool state)
        {
            if (state)
            {
                musicToggle = 1;
                musicMixer.SetFloat("volume", 0);
                PlayerPrefs.SetInt("musicToggle", 1);
            }
            else
            {
                musicToggle = 0;
                musicMixer.SetFloat("volume", -80);
                PlayerPrefs.SetInt("musicToggle", 0);
            }
        }

        public void SetNumberOfQuestionsPerGame(int value)
        {
            PlayerPrefs.SetInt("questionsPerGameDropdownValue", value + 5);
            questionsPerGameDropdownValue = value + 5;
        }

        public static int GetNumberOfQuestionsPerGame()
		{
            return questionsPerGameDropdownValue;
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
                // Orientation has changed so we need to update the *parent* canvas orientation
                utils.CheckIfOrientationUpdated(parentPortraitCanvas, parentLandscapeCanvas, true);
                RefreshSettingsUI();
            }
        }
    }
}