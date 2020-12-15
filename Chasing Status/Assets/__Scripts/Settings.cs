using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Settings : MonoBehaviour
{
    // Gets reference to the audioMixer
    public AudioMixer audioMixer;

    /**
     * Takes in a float parameter and sets the music volume to that parameter
     */
    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("musicVolume", volume);
    }
    /**
     * Takes in a float parameter and sets the sound effects volume to that parameter
     */
    public void SetEffectVol(float volume)
    {
        audioMixer.SetFloat("sfxVolume", volume);
    }

}
