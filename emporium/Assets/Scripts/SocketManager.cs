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

        StartCoroutine(UnixUpdater());

        socket.On("LASTONLINE_PING", SendAutosaveVerify);
        socket.On("VERIFY", Verification);
        socket.On("DISCREPANCY", DiscrepancyAction);
        socket.On("RECEIVE_UNIX", ReceiveUnix);

        socket.On("BUILD_TILE", GameObject.Find("_ManagerialScripts").GetComponent<AssignTiles>().BuildTile);

        socket.On("NO_FUNDS", NoFundsAlert);

        socket.On("ADD_FUNDS", AddFunds);
        socket.On("UPDATE_PLOT_SIZE", UpdatePlotSize);


    }
	
	

    IEnumerator UnixUpdater()
    {
        while (true)
        {

            yield return new WaitForSeconds(1f);
            GetUnix();

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

  

    public void UpdatePlotSize(SocketIOEvent evt)
    {
        
        
        Debug.Log("2221");

        GameAlerts.AlertWithMessage("You have upgraded your plot size! Please log in again.");

        logOffWithDelay(2f);

    

        
    }

   



    void DiscrepancyAction(SocketIOEvent evt)
    {

        GameAlerts.AlertWithMessage("Desynchronization detected. Logging off...");
        logOffWithDelay(2f);

        //TODO: show diecrepency alert, mb shut down game even.


    }

   

    void ReceiveUnix(SocketIOEvent evt)
    {
        
        unix = int.Parse(Regex.Replace(evt.data.GetField("unixBuffer").ToString(), "[^0-9]", "")); //FIXME this is dumb

  
    }

    void NoFundsAlert(SocketIOEvent evt)
    {
        GameAlerts.AlertWithMessage("Not enough money!"); //TODO: finsih and test this. 


    }
    void AddFunds(SocketIOEvent evt)
    {
        int additive = int.Parse(evt.data.GetField("addFunds").ToString());
        Database.UserDollars += additive;



    }

    public void ExpandPlotsize()
    {
        Dictionary<string, string> data = new Dictionary<string, string>();
        data["Uname"] = GlobalControl.Uname;
        socket.Emit("VERIFY_EXPAND_PLOTSIZE", new JSONObject(data));

    }

    public IEnumerator logOffWithDelay(float delay)
    {

        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(0);
    }





}
