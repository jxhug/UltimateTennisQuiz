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
    public void LoadQuiz()
    {
        SceneManager.LoadScene("Quiz");
    }
    public void LoadHome()
    {
        SceneManager.LoadScene("HomeMenu");
    }
    public void LoadPlayerSelect()
    {
        SceneManager.LoadScene("PlayerSelect");
    }
}
