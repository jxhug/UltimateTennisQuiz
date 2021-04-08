using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class SceneLoader : MonoBehaviour
{   
    public Animator Transition;
    public float TransitionTime = 1f;

   public void LoadPlayerSelect()
    {
        StartCoroutine(LoadScene("PlayerSelectMenu"));
    }
    public void LoadSettings()
    {
        StartCoroutine(LoadScene("SettingsMenu"));
    }
    public void LoadSingleplayer()
    {
        StartCoroutine(LoadScene("SingleplayerQuiz"));
    }
    public void LoadHome()
    {
        StartCoroutine(LoadScene("HomeMenu"));
    }

    IEnumerator LoadScene(string LevelName) 
    {
        Transition.SetTrigger("Start");

        yield return new WaitForSeconds(TransitionTime);

        SceneManager.LoadScene(LevelName);
    }
}
