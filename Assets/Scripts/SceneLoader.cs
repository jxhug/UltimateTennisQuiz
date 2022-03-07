using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneLoader : MonoBehaviour
{
    public void LoadSingleplayer()
    {
        PlayerSelect.numberPlayersInGame = 1;
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
}
