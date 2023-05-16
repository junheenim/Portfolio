using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public float masterVolume;
    public bool masterOn = true;

    public AudioSource bgmAudio;
    public float bgmVolume;
    public bool bgmOn = true;

    public AudioSource sfxAudio;
    public float sfxVolume;
    public bool sfxOn = true;

    
}
