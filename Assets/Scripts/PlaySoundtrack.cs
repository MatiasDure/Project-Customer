using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundtrack : MonoBehaviour
{
    [SerializeField] private AudioClip[] audioClips;
    [SerializeField] private AudioSource audioSource;
    public static PlaySoundtrack instance { get; private set; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        ButtonBehaviour.OnMainMenuScene += PlayMainMenuSound;
        ButtonBehaviour.OnGameScene += PlayGameSound;
        PlayMainMenuSound();
    }

    private void PlayMainMenuSound()
    {
        audioSource.Stop();
        audioSource.clip = audioClips[0];
        audioSource.Play();
    }

    private void PlayGameSound()
    {
        audioSource.Stop();
        audioSource.clip = audioClips[1];
        audioSource.Play();
    }


    private void OnDestroy()
    {
        ButtonBehaviour.OnMainMenuScene -= PlayMainMenuSound;
        ButtonBehaviour.OnGameScene -= PlayGameSound;
    }
}
