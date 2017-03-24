using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisabledObjectsMain : MonoBehaviour {

    public static GameObject SubmitButton;
    public static GameObject UnamePassInputField;
    public static GameObject UnamePassText;

    // Use this for initialization
    void Start () {
        SubmitButton.SetActive(false);
        UnamePassInputField.SetActive(false);
        UnamePassText.SetActive(false);




    }


    void Awake()
    {
        SubmitButton = GameObject.Find("SubmitButton");
        UnamePassInputField = GameObject.Find("UnamePassInputField");
        UnamePassText = GameObject.Find("UnamePassText");



    }
}
