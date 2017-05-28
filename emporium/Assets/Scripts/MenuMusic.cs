using UnityEngine;
using UnityStandardAssets.ImageEffects;

public class MenuMusic : MonoBehaviour
{
    public int specStat1 = 700, specStat2 = 750; // this is the spectrum data position.
    private float spectrumScale, curveEnhancer = 950f;

    public AudioClip current;

    public bool HaltBeats = false;

    private AudioSource audiosrc;

    // Use this for initialization
    private void Start()
    {
        audiosrc.clip = current;
        audiosrc.Play();
    }

    private void Update()
    {
    }

    private void FixedUpdate()
    {
        float[] spectrum = audiosrc.GetSpectrumData(1024, 1, FFTWindow.BlackmanHarris);// you can use other calculations than blackmanharris.

        spectrumScale = 1f + ((spectrum[specStat1] + spectrum[specStat2]) * curveEnhancer);

        if (HaltBeats)
        {
            spectrumScale = 1f;
        }

        DisabledObjectsMain.Instance.titleText.transform.localScale = Vector3.Lerp(DisabledObjectsMain.Instance.titleText.transform.localScale, new Vector3(Mathf.Clamp(spectrumScale, 1, 1.3f), Mathf.Clamp(spectrumScale, 1, 1.3f), Mathf.Clamp(spectrumScale, 1, 1.3f)) * spectrumScale, Time.deltaTime * spectrumScale);
        Camera.main.GetComponent<Fisheye>().strengthX = Mathf.Lerp(Camera.main.GetComponent<Fisheye>().strengthX, (spectrumScale - 1) / 2, 0.3f);
        Camera.main.GetComponent<Fisheye>().strengthY = Mathf.Lerp(Camera.main.GetComponent<Fisheye>().strengthY, (spectrumScale - 1) / 2, 0.3f);

        DisabledObjectsMain.Instance.titleText.GetComponent<TitleText>().colorChangeAdditive = (spectrumScale - 1) * 2;
    }

    private void Awake()
    {
        audiosrc = gameObject.GetComponent<AudioSource>();
    }
}