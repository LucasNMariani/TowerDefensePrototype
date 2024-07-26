using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private AudioClip towerShootSFX;
    [SerializeField]
    private AudioClip enemiesDeathSFX;
    [SerializeField] AudioSource _torretAudioSource;
    [SerializeField] AudioSource _enemyAudioSource;
    [SerializeField] AudioSource _audioPlayer;
    public static AudioManager instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        //audioPlayer = GetComponent<AudioSource>();
        _torretAudioSource.clip = towerShootSFX;
        _enemyAudioSource.clip = enemiesDeathSFX;
    }

    public void TowerShootAudio()
    {
        _torretAudioSource.Play();
    }

    public void DeathEnemiesAudio()
    {
        _enemyAudioSource.Play();
    }

    public void PlayAudio(AudioClip soundToPlay)
    {
        _audioPlayer.clip = soundToPlay;
        _audioPlayer.Play();
    }

}
