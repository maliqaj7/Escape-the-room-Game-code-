using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Music AudioSources")]
    public List<AudioSource> musicSources = new List<AudioSource>();

    [Header("SFX Move AudioSources")]
    public List<AudioSource> sfxMovementSources = new List<AudioSource>();

    [Header("SFX Extra AudioSources")]
    public List<AudioSource> sfxSources = new List<AudioSource>();

    private int previousMusicIndex = -1;
    private AudioSource currentMusicSource;

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
        PlayRandomMusic();
    }

    // Play Random Music (not same as previous)
    public void PlayRandomMusic()
    {
        if (musicSources.Count == 0)
        {
            Debug.LogWarning("No music sources assigned in AudioManager.");
            return;
        }

        int newIndex;
        do
        {
            newIndex = Random.Range(0, musicSources.Count);
        }
        while (newIndex == previousMusicIndex && musicSources.Count > 1);

        previousMusicIndex = newIndex;

        if (currentMusicSource != null)
            currentMusicSource.Stop();

        currentMusicSource = musicSources[newIndex];
        currentMusicSource.Play();

        StartCoroutine(WaitForMusicEnd());
    }

    // Wait until current music finishes then play next random track
    private IEnumerator WaitForMusicEnd()
    {
        while (currentMusicSource != null && currentMusicSource.isPlaying)
        {
            yield return null;
        }

        yield return new WaitForSeconds(0.1f);
        PlayRandomMusic();
    }

    // Play SFX by index
    public void PlayMovementSFX(int index, bool _canLoop)
    {
        if (index < 0 || index >= sfxMovementSources.Count)
        {
            Debug.LogWarning("SFX index out of range!");
            return;
        }

        if (sfxMovementSources[index].isPlaying) { sfxMovementSources[index].Stop(); }
        sfxMovementSources[index].Play();
        sfxMovementSources[index].loop = _canLoop;
    }

    public void StopMovementFXs(bool _canLoop)
    {
        foreach (var item in sfxMovementSources)
        {
            item.loop = _canLoop;
            item.Stop();
        }
    }

    public void PlayRandomSFXs(int _index)
    {
        if (_index < 0 || _index >= sfxSources.Count)
        {
            Debug.LogWarning("SFX index out of range!");
            return;
        }

        sfxSources[_index].PlayOneShot(sfxSources[_index].clip);
    }

    public void StopAllSounds()
    {
        foreach (var item in musicSources)
        {
            item.Stop();
        }

        foreach (var item in sfxMovementSources)
        {
            item.loop = false;
            item.Stop();
        }

        foreach (var item in sfxSources)
        {
            item.Stop();
        }
    }
}
