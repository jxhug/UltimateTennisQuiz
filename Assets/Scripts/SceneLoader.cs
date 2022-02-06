using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    public void LoadSettings()
    {
        SceneManager.LoadScene("SettingsMenu");
    }
    public void LoadSingleplayer()
    {
        SceneManager.LoadScene("SingleplayerQuiz");
    }
    public void LoadHome()
    {
        SceneManager.LoadScene("HomeMenu");
    }
}
