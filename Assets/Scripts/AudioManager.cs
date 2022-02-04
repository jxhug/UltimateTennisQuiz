using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;


    void Awake()
    {
        instance = this;
        DontDestroyOnLoad(transform.gameObject);
    }  
} 
