using UnityEngine;
using UnityEngine.UI;
using System.Collections;

using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;
using SocketIO;
using UnityStandardAssets.ImageEffects;

public class SocketManager : MonoBehaviour {

    GlobalControl globalcontr;
    Database db;
    bool ver = false;
    public int unix;
    HelperScripts helperscript;
    

    SocketIOComponent socket;
    
    // Use this for initialization
    void Start () {

        db = gameObject.GetComponent<Database>();

        GameObject go = GameObject.Find("SocketIO");
        socket = go.GetComponent<SocketIOComponent>();
        globalcontr = GameObject.Find("GlobalObject").GetComponent<GlobalControl>();

   
        socket.On("LASTONLINE_PING", SendAutosaveVerify);
        socket.On("VERIFY", Verification);
        socket.On("DISCREPANCY", DiscrepancyAction);
        socket.On("RECEIVE_UNIX", ReceiveUnix);

        socket.On("BUILD_TILE", GameObject.Find("_ManagerialScripts").GetComponent<AssignTiles>().BuildTile);


        StartCoroutine(UnixUpdater());

        

    }
	
	// Update is called once per frame
	void Update () {


    }

    IEnumerator UnixUpdater()
    {
        while (true)
        {
            GetUnix();
            yield return new WaitForSeconds(1f);


        }


    }

    void GetUnix()
    {
        socket.Emit("GET_UNIX");

    }


 
    
    void SendAutosaveVerify(SocketIOEvent evt)
    {
        Dictionary<string, string> data = new Dictionary<string, string>();
        data["Uname"] = GlobalControl.Uname;
        Debug.Log("Received lastlogged ping from server");
        socket.Emit("AUTOSAVE_PUSH_LASTLOGGED", new JSONObject(data));

    }

    IEnumerator WaitForVerify(int s)
    {

        yield return new WaitForSeconds(5);
        if( ver == false)
        {
            Debug.Log("bad verification!");

        }
    }

    void Verification(SocketIOEvent evt)
    {
        Debug.Log(evt);
        Debug.Log("Server verified client.");
        if (bool.Parse(evt.data.GetField("ver").ToString())) { 
            Debug.Log("verification sent back false");
            ver = false;
        }else
        {//verification came back true;
            ver = true;
        }
        

    }

    public void UpdateUI(SocketIOEvent evt)
    {

        Text moneytext = GameObject.Find("MoneyEdit").GetComponent<Text>();
        Text usertext = GameObject.Find("PlayingAsEdit").GetComponent<Text>();

        
        int dollars = int.Parse(evt.data.GetField("dollars").ToString());
        

        moneytext.text = Database.UserDollars.ToString();
        Debug.Log("setting dollar UI text to " + dollars);
        usertext.text = GlobalControl.Uname;
        Debug.Log("setting username UI text to " + GlobalControl.Uname);


    }

    public void UpdatePlotSize(SocketIOEvent evt)
    {

        RotationScript rotscript = GameObject.Find("Main Camera").GetComponent<RotationScript>();
        rotscript.SetCurrentRotCenter(helperscript.LyginisPlotsize());//also sets ground transform

        GameObject.Find("Ground").transform.localScale = new Vector3(Database.UserPlotSize, 0.1f, Database.UserPlotSize);
    }

    public void VerifyHarvest(int ID)
    {
        //LEFTOFF: send to server and send back fruit delete order + update fruit count somewhere. Also new columns in stats (oranges,apples, t.t).

    }



    void DiscrepancyAction(SocketIOEvent evt)
    {
        //TODO: show diecrepency alert, mb shut down game even.


    }

   

    void ReceiveUnix(SocketIOEvent evt)
    {
        Debug.Log(evt);
     


      
        unix = int.Parse(Regex.Replace(evt.data.GetField("unixBuffer").ToString(), "[^0-9]", "")); //FIXME this is dumb

  
    }





}
