using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] SoundEffect[] sounds;
    public bool mute;
    public static SoundManager instance;
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        foreach (var sf in sounds)
        {
            var go = new GameObject();
            go.transform.parent = transform;
            sf.SetSourece(go.AddComponent<AudioSource>());
        }
    }

    public void Play(string effectName)
    {
        if (mute) return;
        var sound = sounds.FirstOrDefault((s) => s.name == effectName);
        if (sound is null) throw new Utils.GameException($"No audio source with name {effectName}");
        sound.Play();
    }
}

[Serializable]
public class SoundEffect
{
    public string name;
    [SerializeField] private AudioClip clip;
    [SerializeField] private float pitch = 1;
    [SerializeField] private float volume = 1;
    private AudioSource source;

    public void SetSourece(AudioSource s)
    {
        source = s;
        source.clip = clip;
        source.volume = volume;
        source.pitch = pitch;
    }

    public void Play()
    {
        source.Play();
    }
}