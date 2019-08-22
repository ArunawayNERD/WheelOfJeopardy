using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioScript : MonoBehaviour
{
    public AudioClip thisClip;

    public AudioSource musicSource;

    // Start is called before the first frame update
    void Start()
    {
        this.musicSource.clip = thisClip;
    }

    public void PlayClip()
    {
        this.musicSource.Play();
    }
}
