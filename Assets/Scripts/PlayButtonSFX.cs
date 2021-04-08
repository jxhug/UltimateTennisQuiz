using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayButtonSFX : MonoBehaviour
{
    public AudioSource AudioManager;
    public AudioClip ButtonPress;

    public void ClickSound() 
    {
        AudioManager.PlayOneShot(ButtonPress);
    }
}
