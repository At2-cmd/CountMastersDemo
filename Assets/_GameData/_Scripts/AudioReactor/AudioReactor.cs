using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioReactor : MonoBehaviour
{
    private static AudioReactor _instance;
    public AudioLibrary _lib;
    public static AudioLibrary lib;
    public static bool isSoundActive = true;
    public AudioSource crowdRunningAudioSource;

    private static Dictionary<AudioGroup, float> _playtimes = new Dictionary<AudioGroup, float>();

    private void OnEnable()
    {
        EventManager.Instance.OnGameStarted += OnGameStartedHandler;
        EventManager.Instance.OnFightStarted += OnFightStartedHandler;
        EventManager.Instance.OnFightWon += OnFightWonHandler;
        EventManager.Instance.OnFinishPointReached += OnFinishPointReachedHandler;
    }

    private void OnGameStartedHandler()
    {
        crowdRunningAudioSource.Play();
    }
    private void OnFightWonHandler()
    {
        crowdRunningAudioSource.Play();
    }

    private void OnFightStartedHandler(Vector3 obj)
    {
        crowdRunningAudioSource.Stop();
    }
    private void OnFinishPointReachedHandler()
    {
        crowdRunningAudioSource.Stop();
    }

    private void Awake()
    {
        _instance = this;
        lib = _lib;
    }

    public static void Play(AudioGroup audioGroup)
    {
        if (!isSoundActive)
            return;

        if (audioGroup.cooldown > 0 && _playtimes.TryGetValue(audioGroup, out float playtime))
        {
            if (Time.time - playtime < audioGroup.cooldown)
                return;
        }

        _playtimes[audioGroup] = Time.time;
        _instance.StartCoroutine(_instance.PlayAudio(audioGroup.Get_Clip(), audioGroup.Get_Vol(), audioGroup.Get_Pitch()));
    }

    public static void Play(AudioGroup audioGroup, float volume)
    {
        if (!isSoundActive)
            return;

        if (audioGroup.cooldown > 0 && _playtimes.TryGetValue(audioGroup, out float playtime))
        {
            if (Time.time - playtime < audioGroup.cooldown)
                return;
        }

        _playtimes[audioGroup] = Time.time;
        _instance.StartCoroutine(_instance.PlayAudio(audioGroup.Get_Clip(), volume, audioGroup.Get_Pitch()));
    }

    public static void PlayClip(AudioClip audioClip, float volume = 1.0f, float pitch = 1.0f)
    {
        if (isSoundActive)
            _instance.StartCoroutine(_instance.PlayAudio(audioClip, volume, pitch));
    }

    private IEnumerator PlayAudio(AudioClip clip, float volume, float pitch)
    {
        AudioSource audioSource = _instance.gameObject.AddComponent<AudioSource>();
        audioSource.volume = volume;
        audioSource.pitch = pitch;
        audioSource.clip = clip;
        audioSource.Play();
        yield return new WaitForSecondsRealtime(clip.length + Time.deltaTime);
        Destroy(audioSource);
    }
}
