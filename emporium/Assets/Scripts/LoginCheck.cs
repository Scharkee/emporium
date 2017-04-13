using UnityEngine;
using UnityEngine.UI;
using System.Collections;

using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;
using SocketIO;
using UnityStandardAssets.ImageEffects;


public class LoginCheck : MonoBehaviour {
    public string inputusername;
    public string inputpassword;
    Dictionary<string, string> data;


    public SocketIOComponent socket;

    // Use this for initialization
    void Start () {
		GameObject go = GameObject.Find ("SocketIO");
		socket = go.GetComponent<SocketIOComponent>();
		socket.On("PASS_CHECK_CALLBACK", OnLoginCheckCallback);
   


    }


 //TODO: check for dupe username

    public IEnumerator CheckDBForDupeUN(string username)
    {  
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(CheckLoginDetails(GlobalControl.Instance.Uname, GlobalControl.Instance.Pass));
    }
    

    IEnumerator CheckLoginDetails(string username, string password)
    {
        data = new Dictionary<string, string>();
        Debug.Log("sending "+ username+ password);
        data["Uname"] = username;
        data["Upass"] = password;
        yield return new WaitForSeconds(0.1f);
        socket.Emit("CHECK_LOGIN", new JSONObject(data));
    }

    private void OnLoginCheckCallback(SocketIOEvent evt)
    {
		
	

		LoginorCreate (int.Parse(evt.data.GetField ("passStatus").ToString()) );
    }



    public void LogInCh(string un, string pass)
    {
        StartCoroutine(CheckLoginDetails(un,pass));
        
   
    }
    

	string  JsonToString( string target, string s){

		string[] newString = Regex.Split(target,s);

		return newString[1];

	}

	void LoginorCreate(int stat){
		if (stat == 0) {  //pass incorrect
            Reaskforlogininfo();
        } else if (stat == 1) { //pass correct
            proceedToGameScene();

        } else if (stat == 2) { //user not in DB

            Debug.Log("user not found in DB, inititing creation");
            socket.Emit("CREATE_USER", new JSONObject(data)); // mb doesnt work iunno
            proceedToGameScene();
        }else if(stat == 3)
        {
            //user already logged in from another PC.
            //TODO: Flash "logged in already" here, and reask for password
     
        }


	}
	void proceedToGameScene(){
        StartCoroutine(LVLLoadCamEffect());
        SceneManager.LoadScene("GameScene");
    }
    void Reaskforlogininfo() {

        Debug.Log("REASKING FOR LOGIN INFO");
        GlobalControl.Instance.Pass = null;
        GlobalControl.Instance.Uname = null;
        GlobalControl.Instance.Logincount = 1;

        StartCoroutine(TextFader(GameObject.Find("UnamePassText")));
        StartCoroutine(TextFader(GameObject.Find("WrongPassText")));
        StartCoroutine(waitforabitandfadeagain());
    }

    IEnumerator waitforabitandfadeagain()
    {
        yield return new WaitForSeconds(0.5f);

        GameObject.Find("UnamePassText").GetComponent<Text>().text = "Enter your username:";
        StartCoroutine(TextFader(GameObject.Find("UnamePassText")));
        StartCoroutine(TextFader(GameObject.Find("WrongPassText")));
    }
    IEnumerator LVLLoadCamEffect()
    {
      
        while (Globals.Instance.cameraBloom.bloomThreshold > 0.01)
        {
            //fadeoutas
            yield return new WaitForSeconds(0.001f);
            Globals.Instance.cameraBloom.bloomThreshold -= 0.01f;
        }
    }
    IEnumerator TextFader(GameObject txtobject)
    {
        Color def;
        def = txtobject.GetComponent<Text>().color;
        Debug.Log("doin the fade");

        if (def.a < 1f)
        {
            while (def.a < 1f)
            {
                //fadeoutas
                yield return new WaitForSeconds(0.001f);
                def.a = def.a + 0.02f;
                txtobject.GetComponent<Text>().color = def;
            }
        }
        else if (def.a > 0.1f)
        {
            while (def.a > 0)
            {
                //fadeoutas 
                yield return new WaitForSeconds(0.001f);
                def.a = def.a - 0.02f;
                txtobject.GetComponent<Text>().color = def;
            }
        }
    }


}
