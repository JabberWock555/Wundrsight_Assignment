using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

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

        PlayMenuBGM();
    }

    [SerializeField] private AudioSource bgm;
    [SerializeField] private AudioSource sfx;

    [SerializeField] private AudioClip menuBGM;
    [SerializeField] private AudioClip gameBGM;
    [SerializeField] private AudioClip buttonClickSFX;
    [SerializeField] private AudioClip pauseSFX;
    [SerializeField] private AudioClip pizzaSFX;
    [SerializeField] private AudioClip tileSFX;


    public void PlayMenuBGM()
    {
        bgm.clip = menuBGM;
        bgm.loop = true;
        bgm.Play();

    }

    public void PlayGameBGM()
    {
        bgm.clip = gameBGM;
        bgm.loop = true;
        bgm.Play();
    }

    public void PlayButtonClickSFX() => sfx.PlayOneShot(buttonClickSFX);

    public void PlayPizzaSFX() => sfx.PlayOneShot(pizzaSFX);

    public void PlayTileSFX() => sfx.PlayOneShot(tileSFX);

    public void PlayPauseSFX() => sfx.PlayOneShot(pauseSFX);
}
