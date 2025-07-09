using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static bool playMusicOnStart = true;

    public AudioMixer audioMixer;

    [Header("Sliders")]
    public Slider masterSlider;
    public Slider musicSlider;
    public Slider sfxSlider;

    [Header("Referencias de audio")]
    public AudioSource musicSource;

    void Start()
    {
        masterSlider.value = 0.75f;
        musicSlider.value = 0.75f;
        sfxSlider.value = 0.75f;

        masterSlider.onValueChanged.AddListener(SetMasterVolume);
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);

        Volumes();

        if (musicSource != null && playMusicOnStart)
        {
            musicSource.loop = true;
            musicSource.Play();
        }
    }

    public void SetMasterVolume(float volume)
    {
        audioMixer.SetFloat("Master", Mathf.Log10(volume) * 20);
    }

    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("Music", Mathf.Log10(volume) * 20);
    }

    public void SetSFXVolume(float volume)
    {
        audioMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
    }

    public void Volumes()
    {
        audioMixer.SetFloat("Master", 0);
        audioMixer.SetFloat("Music", 0);
        audioMixer.SetFloat("SFX", 0);
    }
    public void StopMusic()
    {
        if (musicSource != null && musicSource.isPlaying)
        {
            musicSource.Stop();
        }
    }
}