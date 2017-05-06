using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuMute : MonoBehaviour {

    public Sprite onImg,offImg;

    private bool muted=false;

    private void Start()
    {
        gameObject.GetComponent<Button>().image.sprite = offImg;
    }



    public void TheClick()
    {
       

        if (muted)
        {
       
            muted = false;
            DisabledObjectsMain.Instance.Menumusic.HaltBeats = false;
            DisabledObjectsMain.Instance.Menumusic.gameObject.GetComponent<AudioSource>().volume = 0.2f;
            gameObject.GetComponent<Button>().image.sprite = offImg;

        }
        else
        {
      
            muted = true;
            DisabledObjectsMain.Instance.Menumusic.HaltBeats = true;
            DisabledObjectsMain.Instance.Menumusic.gameObject.GetComponent<AudioSource>().volume = 0f;
            gameObject.GetComponent<Button>().image.sprite = onImg;
        }

    }
}
