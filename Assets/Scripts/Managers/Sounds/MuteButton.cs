using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuteButton : MonoBehaviour
{
    [SerializeField] protected GameObject offSprite; // The mute icon object

    void Start()
    {
        // Set initial audio volume and the state of the mute icon
        if (AudioListener.volume > 0)
        {
            SetAudioVolume(1);
            offSprite.SetActive(false);
        }
        else
        {
            SetAudioVolume(0);
            offSprite.SetActive(true);
        }
    }

    // Called when the user clicks on the button
    public void UpdateMute()
    {
        if (AudioListener.volume > 0)
        {
            // Mute the audio
            SetAudioVolume(0);
            offSprite.SetActive(true);  // Show mute icon
        }
        else
        {
            // Unmute the audio
            SetAudioVolume(1);
            offSprite.SetActive(false);  // Hide mute icon
        }
    }

    // Helper method to set the audio volume
    private void SetAudioVolume(float volume)
    {
        AudioListener.volume = volume;
    }
}
