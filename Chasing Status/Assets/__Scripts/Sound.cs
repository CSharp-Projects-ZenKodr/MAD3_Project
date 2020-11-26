using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
    [Range(0f, 1f)]
    public float volume;
    public bool isLoop;

    // Public for use in other class but not in unity-editor
    [HideInInspector]
    public AudioSource source;
}
