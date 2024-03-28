using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    private AudioSource bgmSource;
    private AudioSource effectSource;

    // BGM
    public AudioClip mainMenuBgmClip;
    public AudioClip gameplayBGMClip;

    // Sound Effect
    public AudioClip buttonClickSound;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        bgmSource = gameObject.AddComponent<AudioSource>();
        effectSource = gameObject.AddComponent<AudioSource>();

        bgmSource.clip = mainMenuBgmClip;
        bgmSource.loop = true;
        bgmSource.Play();
    }

    public void SwitchGameplayBGM()
    {
        bgmSource.Stop();
        bgmSource.clip = gameplayBGMClip;
        bgmSource.Play();
    }

    public void SwitchMainMenuBGM()
    {
        bgmSource.Stop();
        bgmSource.clip = mainMenuBgmClip;
        bgmSource.Play();
    }

    public void PlayButtonClickSound()
    {
        effectSource.PlayOneShot(buttonClickSound);
    }

    public void SetBGMVolume(float value)
    {
        bgmSource.volume = value;
    }

    public void SetEffectsVolume(float value)
    {
        effectSource.volume = value;
    }

    public float GetBGMVolume()
    {
        return bgmSource.volume;
    }

    public float GetEffectsVolume()
    {
        return effectSource.volume;
    }

}
