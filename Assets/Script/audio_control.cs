using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audio_control : MonoBehaviour
{
    public AudioClip audioClip;

    public void PlayEffect()
    {
        AudioSource audio = GetComponent<AudioSource>();
        audio.PlayOneShot(audioClip);
    }
}
