using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneLoader : MonoBehaviour
{
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
