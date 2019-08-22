using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public static AudioController instance;

    private AudioSource src;

	private void Start()
	{
        instance = this;
        src = GetComponent<AudioSource>();
	}

    public void Play (AudioClip _clip)
    {
        src.PlayOneShot(_clip);
    }
}
