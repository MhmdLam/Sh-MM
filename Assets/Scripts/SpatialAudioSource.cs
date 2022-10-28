using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpatialAudioSource : MonoBehaviour
{
    public AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
}
