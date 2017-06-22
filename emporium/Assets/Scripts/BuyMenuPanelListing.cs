using UnityEngine;

public class BuyMenuPanelListing : MonoBehaviour
{
    public AudioSource audio;

    private void OnMouseOver() // bloops kai eina per mygtukus
    {
        audio.Play();
    }

    private void Start()
    {
        //audio = DisabledObjectsGameScene.Instance.ListingAudio
    }
}