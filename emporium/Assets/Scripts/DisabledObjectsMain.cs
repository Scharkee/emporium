using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisabledObjectsMain : MonoBehaviour {

    public static DisabledObjectsMain Instance;

    public GameObject SubmitButton;
    public GameObject UnamePassInputField;
    public GameObject UnamePassText;
    public GameObject MainCanvas;
         


    // Use this for initialization
    void Start () {

        SubmitButton.SetActive(false);
        UnamePassInputField.SetActive(false);
        UnamePassText.SetActive(false);




    }


    void Awake()
    {
        Instance = this;
        SubmitButton = GameObject.Find("SubmitButton");
        UnamePassInputField = GameObject.Find("UnamePassInputField");
        UnamePassText = GameObject.Find("UnamePassText");

        MainCanvas = GameObject.Find("Canvas");

    }
}
