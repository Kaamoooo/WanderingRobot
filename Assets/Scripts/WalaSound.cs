using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalaSound : MonoBehaviour
{
    /// <summary>
    /// 0:Warning
    /// 1:PositiveBeep
    /// 2:NegativeBeep
    /// 3:EscapeFailed
    /// 4:EscapeSucceed
    /// 5:VisionDecrease
    /// 6:GetCaptured
    /// 7:Initialte
    /// </summary>
    public AudioClip[] WalaSounds;
    public AudioSource WarningAudioSource;
    private AudioSource audioSource;


    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(WalaSounds[7]);
    }

    void Update()
    {
        if(GameController.IsGameOver)
        {
            WarningAudioSource.Stop();
        }
        if (GameController.IsWarning)
        {
            WarningAudioSource.volume = 0.36f *(1 - Mathf.Clamp01(GameController.MinDroidWalaDistance / GameController.MinWarningDistance));
        }
        if (GameController.IsWarning&&!WarningAudioSource.isPlaying) {
            WarningAudioSource.Play();
        }
        if(!GameController.IsWarning&&WarningAudioSource.isPlaying)
        {
            WarningAudioSource.Stop();
        }
    }
    public void PlayEscapeFailedSound()
    {
        audioSource.Stop();
        audioSource.PlayOneShot(WalaSounds[3]);
    }

    public void PlayEscapeSucceedSound()
    {
        audioSource.Stop();
        audioSource.PlayOneShot(WalaSounds[4]);
    }

    public void PlayVisionDecrease()
    {
        audioSource.PlayOneShot(WalaSounds[5]);
    }

    public void PlayPositiveBeep()
    {
        audioSource.PlayOneShot(WalaSounds[1]);
    }
    public void PlayNegativeBeep()
    {
        audioSource.PlayOneShot(WalaSounds[2]);
    }

    public void PlayCapturedSound()
    {
        audioSource.PlayOneShot(WalaSounds[6]);
    }
}
