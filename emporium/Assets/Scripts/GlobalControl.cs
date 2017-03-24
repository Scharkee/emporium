﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GlobalControl : MonoBehaviour
{
    public static string Uname;
    public static string Pass;
    public static int Logincount;
    public static int Userlanguage;
    private Dictionary<string,string> currentLangDict;
    public static GlobalControl Instance;
    public bool ConnectedOnceNoDupeStatRequests = false;

    void Start()
    {
        Userlanguage = 0;
        Logincount = 1;

        Languages.initDicts();
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

            if (SceneManager.GetActiveScene().name == "Main") // pakeista login screene
            {
                GameObject.Find("LoginButtonText").GetComponent<Text>().text = currentLangDict["login"];
                GameObject.Find("tempLoginButtonText").GetComponent<Text>().text = currentLangDict["templog"];

            }
       

        }
       


    }

}