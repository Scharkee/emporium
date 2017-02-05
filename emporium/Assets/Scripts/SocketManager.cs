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
    int unix;
    

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

        

    }
	
	// Update is called once per frame
	void Update () {


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

    void DiscrepancyAction(SocketIOEvent evt)
    {
        //TODO: show diecrepency alert, mb shut down game even.


    }

    public int GetUnix()
    {
        int unixx;

      

        socket.Emit("GET_UNIX");

        while (unix==0)
        {
           //FIX THIS, test by pressing U ingame.
        }

        unixx = unix;
        unix = 0;

        return unixx;
    }

    void ReceiveUnix(SocketIOEvent evt)
    {
        unix = int.Parse(evt.data.GetField("unixTime").ToString());
    }





}
