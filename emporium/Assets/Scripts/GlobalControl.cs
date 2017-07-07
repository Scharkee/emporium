using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GlobalControl : MonoBehaviour
{
    public string Uname;
    public string Pass;
    public int Logincount;
    public int Userlanguage;
    public Dictionary<string, string> currentLangDict = new Dictionary<string, string>();
    public static GlobalControl Instance;
    public bool ConnectedOnceNoDupeStatRequests = false;
    private bool firstLaunch = true;

    private void Start()
    {
        Userlanguage = 0;
        Logincount = 1;
    }

    private void Awake()
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
        Destroy(gameObject);
    }

    public void setLanguage(int lang)
    {
        Userlanguage = lang;
        if (!Languages.Instance.initiated)
        {
            Languages.Instance.initDicts();
        }

        switch (Userlanguage)
        {
            case 0:
                currentLangDict = Languages.Instance.english;
                break;

            case 1:
                currentLangDict = Languages.Instance.lithuanian;
                break;

            default:
                Debug.Log("error in dict selection");
                break;
        }

        //if (SceneManager.GetActiveScene().name == "Main") // pakeista login screene (tik ten ir galima keisti is esmes)
        //{
        //    GameObject.Find("LoginButtonText").GetComponent<Text>().text = currentLangDict["login"];
        //    GameObject.Find("tempLoginButtonText").GetComponent<Text>().text = currentLangDict["templog"];
        //}
    }
}