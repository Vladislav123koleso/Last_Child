using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSound : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;

    public void PlaySound()
    {
        if (audioSource != null)
        {
            audioSource.Play();
        }
    }
}
