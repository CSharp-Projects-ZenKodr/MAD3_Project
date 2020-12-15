using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Creates Array of Sounds
    public Sound[] sounds;

    // Awake is called before all other functons
    private void Awake()
    {
        // For each s in the array of sounds do
        foreach(Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>(); // Add AudioSource
            s.source.clip = s.clip; // Set clip
            s.source.volume = s.volume; // Set volume
            s.source.loop = s.isLoop; // Set Loop
            s.source.outputAudioMixerGroup = s.audioGroup; // Set AudioGroup for mixer
        }
        // Player games Main Theme
        PlaySound("MainTheme");
    }

    /**
     * Function is used to call a AudioSource by name and play its sound
     */
    public void PlaySound(string name)
    {
        // For each s in the array of sounds do
        foreach (Sound s in sounds)
        {
            // If s.name matches the name passed
            if (s.name == name)
            {
                s.source.Play(); // Play its sound
            }
        }
    }
}
