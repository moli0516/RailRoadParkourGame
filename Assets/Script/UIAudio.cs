using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAudio : MonoBehaviour
{
    [Header("SFX")]
    [SerializeField] private AudioClip hoverSFX;
    [SerializeField] private AudioClip selectSFX;

    [Header("Speaker")]
    [SerializeField] private AudioSource sfxSpeaker;

    public void OnHover()
    {
        sfxSpeaker.PlayOneShot(hoverSFX);
    }

    public void OnSelect()
    {
        sfxSpeaker.PlayOneShot(selectSFX);
    }
}
