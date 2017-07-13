using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickEngine : MonoBehaviour
{
    public static ClickEngine Instance;

    public AudioClip click2;
    public AudioSource source;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        source = GetComponent<AudioSource>();
        source.clip = click2;
    }

    public void Click()
    {
        source.Play();
    }
}