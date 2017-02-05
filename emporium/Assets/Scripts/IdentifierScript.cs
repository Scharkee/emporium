using UnityEngine;
using System.Collections;

using UnityEngine;
using UnityEngine.UI;


public class IdentifierScript : MonoBehaviour {

    //apsibreziami pag. objektai



    GameObject Identpanel;
    LoginCheck logincheck;
    GameObject Networkman;
    GameObject GlobalObj;
    GlobalControl globcontr;
    GameObject globalobje;
    GlobalControl globalcontrol;
    LoginCheck logcheck;


    //for fading text
    public TextMesh textmesh;
    public Color def;
    float fadd = 1f;


    public bool create = false;


    //new


    InputField inputfplaceholder;
    Text Unamepasstext;

    // Use this for initialization
    void Start () {
      //TODO:  inputf = GameObject.Find("logintextfield").GetComponent<InputField>();
        globalobje = GameObject.Find("GlobalObject");
        globalcontrol = globalobje.GetComponent<GlobalControl>();
        logcheck = gameObject.GetComponent<LoginCheck>();

    }
	
	// Update is called once per frame
	void Update () {
	
	}

   

    public void setPlayerInfo(string val)
    {

        InputField inpf = GameObject.Find("UnamePassInputField").GetComponent<InputField>();


        Debug.Log(val);

        if (GlobalControl.Logincount == 1)
        {
            GlobalControl.Uname = val;
            GlobalControl.Logincount++;

        }
        else if (GlobalControl.Logincount == 2)
        {
            GlobalControl.Pass = val;
            GlobalControl.Logincount++;
            Debug.Log(GlobalControl.Logincount);

            GameObject.Find("UnamePassInputField").GetComponent<InputField>().text = string.Empty;
            
            logcheck.LogInCh(GlobalControl.Uname, GlobalControl.Pass);


            //TODO: keep flashing connecting until the scene changes?

        }


        inpf.text = string.Empty;
  
        inpf.ActivateInputField();
        inpf.Select();

    }
}
