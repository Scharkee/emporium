using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundStorage : MonoBehaviour
{
    public AudioClip[] cachings;

    public AudioClip caching() //iskvieciama kai reika paleist pinigu garsa. Parenka random garsa.
    {
        AudioClip sound = cachings[0];
        int rand = Random.Range(0, cachings.Length - 1);
        sound = cachings[rand];
        return sound;
    }

    public void playSound(AudioClip clip)
    {
        gameObject.GetComponent<AudioSource>().clip = clip;
        gameObject.GetComponent<AudioSource>().Play();
    }
}