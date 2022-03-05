using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneLoader : MonoBehaviour
{
    public void LoadQuiz()
    {
        SceneManager.LoadScene("Quiz");
    }
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void LoadPlayerSelect()
    {
        SceneManager.LoadScene("PlayerSelect");
    }
    public void LoadSettings()
    {
        SceneManager.LoadScene("Settings");
    }
}
