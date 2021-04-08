using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSelectMenu : MonoBehaviour
{
    public void play() 
    {
        SceneManager.LoadScene("Quiz - Singleplayer");
    }
}
