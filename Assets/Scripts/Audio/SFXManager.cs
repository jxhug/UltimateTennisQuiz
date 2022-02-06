using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SFXManager : MonoBehaviour
{
    private static SFXManager instance;

    void Awake()
    {
        if (instance != null && instance.name == "ClickSoundEffect")
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(transform.gameObject);
        }
    }

    private void Update()
    {
        if (instance != null && instance.name == "ClickSoundEffect")
            {
                Destroy(instance);
            }
    }
}