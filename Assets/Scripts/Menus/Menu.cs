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

        public static int questionsPerGameDropdownIndex;


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
            musicToggle = PlayerPrefs.GetInt("musicToggle", 0);
            questionsPerGameDropdownIndex = PlayerPrefs.GetInt("questionsPerGameDropdownIndex", 5);
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

            // Ensure both portrait and landscape dropdowns contain the correct value
            questionsPerGamePortraitDropdown.value = questionsPerGameLandscapeDropdown.value = questionsPerGameDropdownIndex;
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

        public void SetNumberOfQuestionsPerGame(int index)
        {
            PlayerPrefs.SetInt("questionsPerGameDropdownIndex", index);
            questionsPerGameDropdownIndex = index;
        }

        public static int GetNumberOfQuestionsPerGame()
		{
            /* Note: this assumes that the dropdown values begin at 5 and
             * increase in steps of 1. If this isn't true then the following
             * line will need to changed. The label value can be read directly
             * from the dropdown as follows:
             * 
             * string text = questionsPerGamePortraitDropdown.options[index].value;
             * 
             * However this currently cannot be implemented for a static method.
             */
            return questionsPerGameDropdownIndex + 5;
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