using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using UnityStandardAssets.ImageEffects;


public class nameInputScript : MonoBehaviour {
    //apsibreziami pag. objektai
    GameObject mainBox;
    GameObject Logintext;
    GameObject inputfieldBox;
    GameObject TextObject;
    Transform canvas;
    GameObject inputfield;
    GameObject inputfield1;
    GameObject unametextobj;

    GameObject Identpanel;
    LoginCheck logincheck;
    DataInserter datains;
    GameObject Networkman;
    GameObject GlobalObj;
    GlobalControl globcontr;
    GameObject globalobje;
    GlobalControl globalcontrol;


    //for fading text
    public TextMesh textmesh;
    public Color def;
    float fadd = 1f;


    public bool create = false;



    // Use this for initialization
    void Start () {
       
        mainBox = transform.FindChild("mainBox").gameObject;
      //  Logintext = transform.FindChild("NameText").FindChild("Text").gameObject;
   //     inputfieldBox = transform.FindChild("inputfieldBox").gameObject;
      //  TextObject = transform.FindChild("inputfieldBox").gameObject;
        canvas = GameObject.Find("Canvas").transform;

        if (inputfield == null) {
           
            inputfield = Instantiate(Resources.Load("nameInputField", typeof(GameObject))) as GameObject;
            inputfield.transform.SetParent(canvas, false);
        }
        StartCoroutine(TextFader(GameObject.Find("UnamePassText")));



    }



    // Update is called once per frame
    void Update () {
        
	
	}

    IEnumerator TextFader(GameObject txtobject)
    {
        def = txtobject.GetComponent<TextMesh>().color;
        Debug.Log("doin the fade");
        

        if (def.a < 1f)
        {
            while (def.a < 1f)
            {
                //fadeoutas
                yield return new WaitForSeconds(0.001f);
                def.a = def.a + 0.02f;
                txtobject.GetComponent<TextMesh>().color = def;
               


            }
        }else if (def.a > 0.1f)
        {
            while (def.a > 0)
            {
                //fadeoutas 
                yield return new WaitForSeconds(0.001f);
                def.a = def.a - 0.02f;
                txtobject.GetComponent<TextMesh>().color = def;

            }
        }

        

        
    }


      public void InstPassField()
    {

        GameObject.Find("UnamePassText").GetComponent<TextMesh>().text = "Enter your password";
        
        inputfield = Instantiate(Resources.Load("nameInputField", typeof(GameObject))) as GameObject;
        inputfield.transform.SetParent(canvas, false);


        globalobje = GameObject.Find("GlobalObject");
        globalcontrol = globalobje.GetComponent<GlobalControl>();

       
        if(globalcontrol.Pass != null)
        {
            Destroy(GameObject.Find("nameInputField(Clone)"));
        }
        else
        {
            Destroy(GameObject.Find("nameInputField(Clone)"));
        }

        if (globalcontrol.Logincount == 3)
        {
            Destroy(GameObject.Find("nameInputField(Clone)"));
            GlobalObj = GameObject.Find("GlobalObject");
            globcontr = GlobalObj.GetComponent<GlobalControl>();
            Identpanel = GameObject.Find("IdentifierPanel");
            logincheck = Identpanel.GetComponent<LoginCheck>();
            datains = Identpanel.GetComponent<DataInserter>();

            logincheck.inputusername = globalcontrol.Uname;
            logincheck.inputpassword = globalcontrol.Pass;

            //if username not taken yet, then create user MAKE NORMAL SCRIPT 

            logincheck.LogInDupeCh();
            

            StartCoroutine(WaitforWWWreturnDupe());

            
            
        }
        else
        {
            inputfield1 = Instantiate(Resources.Load("nameInputField", typeof(GameObject))) as GameObject;
            inputfield1.transform.SetParent(canvas, false);

        }

    }

    IEnumerator WaitforWWWreturnDupe()
    {
        
        while (logincheck.wwwreturnDupeStatus == "none")
        {
            yield return null;
            Debug.Log("still no reliable string");
           // TextFader(GameObject.Find("ConnectingText")); TODO: expand on this shit. Make connecting... =  blinking text kinda
        }
        //  Debug.Log( wwwret);
        LoginOrCreate();
        
    }

    IEnumerator WaitforWWWreturnLoginVer()
    {
        logincheck.wwwreturnloginverify = "none";
        Debug.Log("coroutine started");
        while (logincheck.wwwreturnloginverify == "none")
        {
            yield return null;
           
            Debug.Log("still no reliable string");
        }
        //  Debug.Log( wwwret);
        
        ProceedToGameScene();

    }

    IEnumerator LVLLoadCamEffect()
    {
        
        Bloom bloom = Camera.main.GetComponent<Bloom>();
        while (bloom.bloomThreshold > 0.01)
        {
            //fadeoutas
            yield return new WaitForSeconds(0.001f);
            bloom.bloomThreshold = bloom.bloomThreshold - 0.01f;
        }
    }

    void LoginOrCreate()
    {

        if (logincheck.wwwreturnDupeStatus == "user already exists")
        {
            Debug.Log("user in DB, logging in");

            logincheck.LogInCh();

            

            StartCoroutine(WaitforWWWreturnLoginVer());

            
        }
        else if (logincheck.wwwreturnDupeStatus == "user not found")
        {
            Debug.Log("user not found in DB, inititing creation");
            datains.CreateUser(globalcontrol.Uname, globalcontrol.Pass);
            Debug.Log("User created");
            ProceedToGameScene();
        }
    }

    void ProceedToGameScene()
    {
        if(logincheck.wwwreturnloginverify=="login success")
        {
            StartCoroutine(LVLLoadCamEffect());
            SceneManager.LoadScene("GameScene");
        }
        else if(logincheck.wwwreturnloginverify =="password incorrect")
        {
            
            Reaskforlogininfo();
        }else if (logincheck.wwwreturnloginverify == "user not found")
        {
            Debug.Log("Error, bad return from server");
        }
        else
        {
            Debug.Log("Error, bad return from server");
        }
    }


    void Reaskforlogininfo()
    {
        Debug.Log("REASKING FOR LOGIN INFO");
        globalcontrol.Pass = null;
        globalcontrol.Uname = null;
        globalcontrol.Logincount = 1;



        StartCoroutine(TextFader(GameObject.Find("UnamePassText")));
        StartCoroutine(TextFader(GameObject.Find("WrongPassText")));
        StartCoroutine(waitforabit());

        if (GameObject.Find("nameInputField(Clone)") != null)
        {
            Destroy(GameObject.Find("nameInputField(Clone)"));
            Debug.Log("destroyinjg a dupe inputfield    ");
         
        }
        




       // inputfield = Instantiate(Resources.Load("nameInputField", typeof(GameObject))) as GameObject;
       // inputfield.transform.SetParent(canvas, false);







    }
    IEnumerator waitforabit()
    {
        yield return new WaitForSeconds(0.5f);

        GameObject.Find("UnamePassText").GetComponent<TextMesh>().text = "Enter your username";
        StartCoroutine(TextFader(GameObject.Find("UnamePassText")));
        StartCoroutine(TextFader(GameObject.Find("WrongPassText")));
        

    }

 






}
