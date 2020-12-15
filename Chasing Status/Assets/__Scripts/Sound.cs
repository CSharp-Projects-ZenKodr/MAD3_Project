using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

// Makes class Serializable
[System.Serializable]
public class Sound
{
    // Set of variables the class Sound contains to be used in AudioManagaer
    public string name;
    public AudioClip clip;
    [Range(0f, 1f)] // Sets volume range
    public float volume;
    public bool isLoop;
    public AudioMixerGroup audioGroup;

    // Public for use in other class but not in unity-editor
    [HideInInspector]
    public AudioSource source;
}
