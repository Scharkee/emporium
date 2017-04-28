using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    Text dollarText;
    Text usernameText;
    Text apelsinaiText;

    public GameObject PressContextPanel;

    // Use this for initialization
    void Start()
    {

        DisablePanels();






    }

    // Update is called once per frame
    void Update()
    {

    }

    void Awake()
    {
        Instance = this;
        //grab all panels 
        PressContextPanel = GameObject.Find("PressContextPanel");

    }

    public void ChangeUIText(string TextObjName, string newtext)
    {
        Text text = GameObject.Find(TextObjName).GetComponent<Text>();

        text.text = newtext;


    }

    private void DisablePanels()
    {// disable all panels at start


        PressContextPanel.SetActive(false);
    }


}
