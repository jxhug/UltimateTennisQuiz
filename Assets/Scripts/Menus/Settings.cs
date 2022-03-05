using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UtilsNS;

namespace SettingsNS
{
    public class Settings : MonoBehaviour
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
        private Slider sfxLandscapeSlider;
        [SerializeField]
        private Slider musicLandscapeSlider;

        public GameObject portraitCanvas;
        public GameObject landscapeCanvas;

        private float sfxSliderValue;
        private float musicSliderValue;

        private Utils utils;

        void Awake()
        {
            utils.UpdateOrientation(ref portraitCanvas, ref landscapeCanvas);
            LoadSettings();
        }

        private void Update()
	    {
            if (utils.UpdateOrientation(ref portraitCanvas, ref landscapeCanvas))
            {
                // switch to landscape as a test
                portraitCanvas.SetActive(false);
                landscapeCanvas.SetActive(true);
                LoadSettings();
            }
        }

        public void LoadSettings()
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
    }
}
