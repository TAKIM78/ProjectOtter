using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip[] audioClips;

    private void Start()
    {
        //audioSource.clip = audioClips[0];
        //audioSource.loop = true;
        //audioSource.Play();
        
    }

    void PlayFirstClip()
    {

    }
}
