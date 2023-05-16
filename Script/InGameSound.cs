using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
public class InGameSound : MonoBehaviour
{
    public GameObject optionUI;

    public AudioMixer mixer;

    public Slider masterVolume;
    public Slider bgm;
    public Slider sfx;

    public Toggle masterbutON;
    public Toggle masterbutOFF;

    public Toggle bgmbutON;
    public Toggle bgmbutOFF;

    public Toggle sfxbutON;
    public Toggle sfxbutOFF;

    public void Start()
    {
        gameObject.SetActive(false);
        masterVolume.value = SoundManager.instance.masterVolume;
        bgm.value = SoundManager.instance.bgmVolume;
        sfx.value = SoundManager.instance.sfxVolume;
        if (SoundManager.instance.masterOn)
        {
            masterbutON.isOn = true;
            masterbutOFF.isOn = false;
        }
        else
        {
            masterbutON.isOn = false;
            masterbutOFF.isOn = true;
        }
        if (SoundManager.instance.bgmOn)
        {
            bgmbutON.isOn = true;
            bgmbutOFF.isOn = false;
        }
        else
        {
            bgmbutON.isOn = false;
            bgmbutOFF.isOn = true;
        }
        if (SoundManager.instance.sfxOn)
        {
            sfxbutON.isOn = true;
            sfxbutOFF.isOn = false;
        }
        else
        {
            sfxbutON.isOn = false;
            sfxbutOFF.isOn = true;
        }
    }
    public void Return()
    {
        optionUI.SetActive(true);
        gameObject.SetActive(false);
    }

    // ¸¶½ºÅÍ º¼·ý
    public void MasterVouleOn()
    {
        AudioListener.volume = masterVolume.value;
        masterVolume.interactable = true;
        SoundManager.instance.masterOn = true;
    }
    public void MasterVouleOff()
    {
        AudioListener.volume = 0;
        masterVolume.interactable = false;
        SoundManager.instance.masterOn = false;
    }
    public void MasterVouleControl()
    {
        AudioListener.volume = masterVolume.value;
        SoundManager.instance.masterVolume = masterVolume.value;
    }
    // BGM
    public void BGMOn()
    {
        mixer.SetFloat("BGM", bgm.value);
        bgm.interactable = true;
        SoundManager.instance.bgmOn = true;
    }
    public void BGMOff()
    {
        mixer.SetFloat("BGM", -80);
        bgm.interactable = false;
        SoundManager.instance.bgmOn = true;
    }
    public void BGMVouleControl()
    {
        if (bgm.value <= -40f)
        {
            mixer.SetFloat("BGM", -80);
        }
        else
        {
            mixer.SetFloat("BGM", bgm.value);
        }
        SoundManager.instance.bgmVolume = bgm.value;
    }
    // SFX
    public void SFXOn()
    {
        mixer.SetFloat("SFX", sfx.value);
        sfx.interactable = true;
        SoundManager.instance.sfxOn = true;
    }
    public void SFXOff()
    {
        mixer.SetFloat("SFX", -80);
        sfx.interactable = false;
        SoundManager.instance.sfxOn = false;
    }
    public void SFXVouleControl()
    {
        if (sfx.value <= -40f)
        {
            mixer.SetFloat("SFX", -80);
        }
        else
        {
            mixer.SetFloat("SFX", sfx.value);
            SoundManager.instance.sfxVolume = sfx.value;
        }
    }
}
