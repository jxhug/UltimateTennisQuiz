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
        private Slider sfxPortraitSlider;

        [SerializeField]
        private Slider musicPortraitSlider;

        [SerializeField]
        private Slider questionsPerGamePortraitSlider;

        [SerializeField]
        private Slider sfxLandscapeSlider;

        [SerializeField]
        private Slider musicLandscapeSlider;

        [SerializeField]
        private Slider questionsPerGameLandscapeSlider;

        [SerializeField]
        private TMP_Text portraiSfxVolumeText;

        [SerializeField]
        private TMP_Text portraitMusicVolumeText;

        [SerializeField]
        private TMP_Text portraitNumberQuestionsPerGameText;

        [SerializeField]
        private TMP_Text landscapeSfxVolumeText;

        [SerializeField]
        private TMP_Text landscapeMusicVolumeText;

        [SerializeField]
        private TMP_Text landscapeNumberQuestionsPerGameText;


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

        public static int questionsPerGameSliderValue;


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
            questionsPerGameSliderValue = Mathf.RoundToInt(PlayerPrefs.GetFloat("questionsPerGameSliderValue", 5f));

            sfxMixer.SetFloat("volume", Mathf.Log10(sfxSliderValue) * 20);
            musicMixer.SetFloat("volume", Mathf.Log10(musicSliderValue) * 20);

            sfxLandscapeSlider.value = sfxPortraitSlider.value = sfxSliderValue;
            musicLandscapeSlider.value = musicPortraitSlider.value = musicSliderValue;
            questionsPerGameLandscapeSlider.value = questionsPerGamePortraitSlider.value = questionsPerGameSliderValue;


            portraiSfxVolumeText.text = "SFX Volume: " + Mathf.RoundToInt(sfxSliderValue * 100) + "%";
            portraitMusicVolumeText.text = "Music Volume: " + Mathf.RoundToInt(musicSliderValue * 100) + "%";
            portraitNumberQuestionsPerGameText.text = "Questions Per Game: " + questionsPerGameSliderValue;

            landscapeSfxVolumeText.text = "SFX Volume: " + Mathf.RoundToInt(sfxSliderValue * 100) + "%";
            landscapeMusicVolumeText.text = "Music Volume: " + Mathf.RoundToInt(musicSliderValue * 100) + "%";
            landscapeNumberQuestionsPerGameText.text = "Questions Per Game: " + questionsPerGameSliderValue;
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
            portraiSfxVolumeText.text = ("SFX Volume: " + Mathf.RoundToInt(sfxSliderValue * 100) + "%");
            landscapeSfxVolumeText.text = ("SFX Volume: " + Mathf.RoundToInt(sfxSliderValue * 100) + "%");
        }

        public void SetMusicVolume(float musicSliderValue)
        {
            musicMixer.SetFloat("volume", Mathf.Log10(musicSliderValue) * 20);
            PlayerPrefs.SetFloat("musicSliderValue", musicSliderValue);
            portraitMusicVolumeText.text = "Music Volume: " + Mathf.RoundToInt(musicSliderValue * 100) + "%";
            landscapeMusicVolumeText.text = "Music Volume: " + Mathf.RoundToInt(musicSliderValue * 100) + "%";
        }

        public void SetNumberOfQuestionsPerGame(float questionsPerGameSliderValue)
        {
            Debug.Log(questionsPerGameSliderValue);
            PlayerPrefs.SetFloat("questionsPerGameSliderValue", questionsPerGameSliderValue);
            portraitNumberQuestionsPerGameText.text = "Questions Per Game: " + Mathf.RoundToInt(questionsPerGameSliderValue);
            landscapeNumberQuestionsPerGameText.text = "Questions Per Game: " + Mathf.RoundToInt(questionsPerGameSliderValue);
        }

        public static int GetNumberOfQuestionsPerGame()
		{
            return questionsPerGameSliderValue;
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