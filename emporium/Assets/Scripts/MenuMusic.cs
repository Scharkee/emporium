using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.ImageEffects;

public class MenuMusic : MonoBehaviour
{



    public int specStat1 = 700, specStat2 = 750; // this is the spectrum data position.
    private float spectrumScale, curveEnhancer = 950f;


    public AudioClip current;


    private AudioSource audiosrc;

    // Use this for initialization
    void Start()
    {
        audiosrc.clip = current;
        audiosrc.Play();
    }



    // Update is called once per frame
    void Update()
    {
        float[] spectrum = audiosrc.GetSpectrumData(1024, 1, FFTWindow.BlackmanHarris);// you can use other calculations than blackmanharris.

        spectrumScale = 1f + ((spectrum[specStat1] + spectrum[specStat2]) * curveEnhancer);

        DisabledObjectsMain.Instance.titleText.transform.localScale = Vector3.Lerp(DisabledObjectsMain.Instance.titleText.transform.localScale, new Vector3(spectrumScale, spectrumScale, spectrumScale), 0.1f);
        Camera.main.GetComponent<Fisheye>().strengthX = Mathf.Lerp(Camera.main.GetComponent<Fisheye>().strengthX, (spectrumScale - 1) / 2, 0.2f);
        Camera.main.GetComponent<Fisheye>().strengthY = Mathf.Lerp(Camera.main.GetComponent<Fisheye>().strengthY, (spectrumScale - 1) / 2, 0.2f);

        DisabledObjectsMain.Instance.titleText.GetComponent<TitleText>().colorChangeAdditive = (spectrumScale - 1) * 2;

    }


    void Awake()
    {
        audiosrc = gameObject.GetComponent<AudioSource>();
    }

}
