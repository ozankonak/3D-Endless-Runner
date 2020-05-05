using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
   #region Singleton Pattern

    public static AudioManager instance;

    private AudioSource source;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        source = GetComponent<AudioSource>();
    }

    #endregion

    [Header("Player Sounds")]
    [SerializeField] private AudioClip rotateSound;
    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private AudioClip crouchSound;
    [SerializeField] private AudioClip punchSound;
    [Header("Environment Sounds")]
    [SerializeField] private AudioClip coinSound;
    [SerializeField] private AudioClip waterSplashSound;
    [SerializeField] private AudioClip gameOverSound;
    [Header("UI Sounds")]
    [SerializeField] private AudioClip buttonClickSound;


    public void PlayRotateSound()
    {
        if (DataManager.instance.isMusicOn)
            source.PlayOneShot(rotateSound);
    }

    public void PlayJumpSound()
    {
        if (DataManager.instance.isMusicOn)
            source.PlayOneShot(jumpSound);
    }

    public void PlayCrouchSound()
    {
        if (DataManager.instance.isMusicOn)
            source.PlayOneShot(crouchSound);
    }

    public void PlayCoinSound()
    {
        if (DataManager.instance.isMusicOn)
            source.PlayOneShot(coinSound);
    }

    public void PlayWaterSplashSound()
    {
        if (DataManager.instance.isMusicOn)
        {
            source.PlayOneShot(waterSplashSound);
        }
    }

    public void PlayPunchSound()
    {
        if (DataManager.instance.isMusicOn)
            source.PlayOneShot(punchSound);
    }

    public void PlayGameOverSound()
    {
        if (DataManager.instance.isMusicOn)
            source.PlayOneShot(gameOverSound);
    }

    public void PlayButtonClickSound()
    {
        if (DataManager.instance.isMusicOn)
            source.PlayOneShot(buttonClickSound);
    }

}
