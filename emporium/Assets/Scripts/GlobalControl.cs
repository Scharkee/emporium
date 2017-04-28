using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GlobalControl : MonoBehaviour
{
    public string Uname;
    public string Pass;
    public int Logincount;
    public int Userlanguage;
    public Dictionary<string, string> currentLangDict;
    public static GlobalControl Instance;
    public bool ConnectedOnceNoDupeStatRequests = false;
    private bool firstLaunch = true;


    void Start()
    {
        Userlanguage = 0;
        Logincount = 1;

        Languages.initDicts();

        currentLangDict = Languages.english;
    }

    void Awake()
    {

        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }




    }

    public void reset()
    {

        Uname = null;
        Pass = null;
        Logincount = 1;
        Userlanguage = 0;
        firstLaunch = false;
        ConnectedOnceNoDupeStatRequests = false;
    }

    public void setLanguage(int lang)
    {
        if (Userlanguage == lang)
        {
        }
        else
        {

            Userlanguage = lang;

            switch (Userlanguage)
            {
                case 0:
                    currentLangDict = Languages.english;
                    break;
                case 1:
                    currentLangDict = Languages.lithuanian;
                    break;
                default:
                    Debug.Log("error in dict selection");
                    break;
            }

            if (SceneManager.GetActiveScene().name == "Main") // pakeista login screene (tik ten ir galima keisti is esmes)
            {
                GameObject.Find("LoginButtonText").GetComponent<Text>().text = currentLangDict["login"];
                GameObject.Find("tempLoginButtonText").GetComponent<Text>().text = currentLangDict["templog"];

            }


        }



    }

}