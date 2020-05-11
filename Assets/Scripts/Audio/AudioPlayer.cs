using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [System.Serializable]
    public class AudioClipNotFoundException : System.Exception
    {
        public AudioClipNotFoundException(string clipName) : base("No clip defined with name \"" + clipName + "\"") { }
    }
    [System.Serializable]
    public class AudioClipVolume
    {
        public string name;
        public AudioClip clip;
        [Range(0, 1)]
        public float volume = 1;
    }

    [Header("Settings")]
    public float FadeTime = 0.25f;

    public AudioClipVolume[] AudioClips;

    private Dictionary<string, AudioClipVolume> clips = new Dictionary<string, AudioClipVolume>();

    private Dictionary<string, AudioSource> loopSources = new Dictionary<string, AudioSource>();
    private List<AudioSource> oneShotSources = new List<AudioSource>();

    void Start()
    {
        foreach (var clip in AudioClips)
        {
            clips[clip.name] = clip;
        }
    }

    void Update()
    {
        foreach (var source in oneShotSources)
        {
            if (!source.isPlaying)
            {
                oneShotSources.Remove(source);
                Destroy(source);
            }
        }
    }

    public void PlayOnce(string name)
    {
        if (clips.ContainsKey(name))
        {
            AudioClipVolume clip = clips[name];
            var source = gameObject.AddComponent<AudioSource>();
            source.volume = clip.volume;
            source.clip = clip.clip;
            source.loop = false;
            oneShotSources.Add(source);
            source.Play();
        }
        else
        {
            throw new AudioClipNotFoundException(name);
        }
    }

    public void PlayLoop(string name)
    {
        PlayLoop(name, FadeTime);
    }

    public void PlayLoop(string name, float FadeTime)
    {
        if (clips.ContainsKey(name))
        {
            if (!loopSources.ContainsKey(name))
            {
                AudioClipVolume clip = clips[name];
                var source = gameObject.AddComponent<AudioSource>();
                source.clip = clip.clip;
                source.loop = true;
                loopSources[name] = source;
                source.Play();
                StartCoroutine(FadeIn(source, clip.volume, FadeTime));
            }
        }
        else
        {
            throw new AudioClipNotFoundException(name);
        }
    }

    public void StopLoop(string name)
    {
        StopLoop(name, FadeTime);
    }

    public void StopLoop(string name, float FadeTime)
    {
        if (clips.ContainsKey(name))
        {
            var source = loopSources[name];
            loopSources.Remove(name);
            StartCoroutine(FadeOut(source, FadeTime));
        }
        else throw new AudioClipNotFoundException(name);
    }

    IEnumerator FadeOut(AudioSource source, float FadeTime)
    {
        float startVol = source.volume;
        float t = 0;
        while (t < FadeTime)
        {
            source.volume = Mathf.Lerp(startVol, 0, t / FadeTime);
            yield return new WaitForEndOfFrame();
            t += Time.deltaTime;
        }
        Destroy(source);
    }

    IEnumerator FadeIn(AudioSource source, float volume, float FadeTime)
    {
        float t = 0;
        source.volume = 0;
        while (t < FadeTime)
        {
            source.volume = Mathf.Lerp(0, volume, t / FadeTime);
            yield return new WaitForEndOfFrame();
            t += Time.deltaTime;
        }
    }
}