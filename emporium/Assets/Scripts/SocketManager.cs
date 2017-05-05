using UnityEngine;
using UnityEngine.UI;
using System.Collections;

using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;
using SocketIO;
using UnityStandardAssets.ImageEffects;

public class SocketManager : MonoBehaviour
{


    Database db;
    bool ver = false;
    public int unix;
    HelperScripts helperscript;


    SocketIOComponent socket;

    // Use this for initialization
    void Start()
    {

        db = gameObject.GetComponent<Database>();
        socket = DisabledObjectsGameScene.Instance.socket;
        GameObject go = GameObject.Find("SocketIO");

        socket = DisabledObjectsGameScene.Instance.socket.GetComponent<SocketIOComponent>();

        StartCoroutine(UnixUpdater());
        StartCoroutine(delayedPrices());

        socket.On("LASTONLINE_PING", SendAutosaveVerify);
        socket.On("VERIFY", Verification);
        socket.On("DISCREPANCY", DiscrepancyAction);
        socket.On("RECEIVE_UNIX", ReceiveUnix);

        socket.On("BUILD_TILE", DisabledObjectsGameScene.Instance.managerialScripts.GetComponent<AssignTiles>().BuildTile);

        socket.On("NO_FUNDS", NoFundsAlert);

        socket.On("ADD_FUNDS", AddFunds);
        socket.On("UPDATE_PLOT_SIZE", UpdatePlotSize);
        socket.On("UPGRADE_TILE",UpgradeTile);
        socket.On("RECEIVE_PRICES", ReceivePrices);

        
    }

    IEnumerator delayedPrices()
    {

        for (int i = 0; i < 5; i++)
        {
            yield return new WaitForSeconds(0.1f);

        }
        RetrievePrices();

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
        data["Uname"] = GlobalControl.Instance.Uname;

        socket.Emit("AUTOSAVE_PUSH_LASTLOGGED", new JSONObject(data));

    }

    IEnumerator WaitForVerify(int s)
    {

        yield return new WaitForSeconds(5);
        if (ver == false)
        {
            Debug.Log("bad verification!");

        }
    }

    void Verification(SocketIOEvent evt)
    {
        Debug.Log(evt);
        Debug.Log("Server verified client.");
        if (bool.Parse(evt.data.GetField("ver").ToString()))
        {
            Debug.Log("verification sent back false");
            ver = false;
        }
        else
        {//verification came back true;
            ver = true;
        }


    }



    public void UpdatePlotSize(SocketIOEvent evt)
    {
        ContextManager.Instance.CancelContext();

        GameAlerts.Instance.AlertWithMessage("You have upgraded your plot size! Please log in again.");
        StartCoroutine(logOffWithDelay(2f));

    }

    public void LogOffWithDelay(float delay)
    {
        ContextManager.Instance.CancelContext();
        StartCoroutine(logOffWithDelay(delay));

    }





    public void DiscrepancyAction(SocketIOEvent evt = default(SocketIOEvent)) //check ar veikia
    {

        GameAlerts.Instance.AlertWithMessage("Desynchronization detected. Logging off...");
        StartCoroutine(logOffWithDelay(2f));

        ContextManager.Instance.CancelContext();



    }

    private void UpgradeTile(SocketIOEvent evt)
    {
 
        int tileID = int.Parse(Regex.Replace(evt.data.GetField("tileID").ToString(), "[^0-9]", ""));

        DisabledObjectsGameScene.Instance.managerialScripts.GetComponent<AssignTiles>().UpgradeTile(tileID);
        Debug.Log("socketman past ID");

    }



    void ReceiveUnix(SocketIOEvent evt)
    {

        unix = int.Parse(Regex.Replace(evt.data.GetField("unixBuffer").ToString(), "[^0-9]", "")); //FIXME this is dumb


    }

    void NoFundsAlert(SocketIOEvent evt)
    {
        ContextManager.Instance.CancelContext();

        GameAlerts.Instance.AlertWithMessage("Not enough money!"); //TODO: finsih and test this. 


    }
    void AddFunds(SocketIOEvent evt)
    {
        float additive = float.Parse(evt.data.GetField("addFunds").ToString());

        Database.Instance.UserDollars += additive;



    }

    public void ExpandPlotsize()
    {
        Dictionary<string, string> data = new Dictionary<string, string>();
        data["Uname"] = GlobalControl.Instance.Uname;
        socket.Emit("VERIFY_EXPAND_PLOTSIZE", new JSONObject(data));

    }

    public void RetrievePrices()
    {
        Dictionary<string, string> data = new Dictionary<string, string>();
        data["Uname"] = GlobalControl.Instance.Uname;
        socket.Emit("GET_PRICES", new JSONObject(data));
        Debug.Log("asking for prices");

    }

    public void ReceivePrices(SocketIOEvent evt)
    {

        DisabledObjectsGameScene.Instance.pricemanager.ResolvePrices(evt);
    }

    public IEnumerator logOffWithDelay(float delay)
    {

        yield return new WaitForSeconds(delay);
        socket.Emit("DISCONNECT");
        GlobalControl.Instance.reset();
        SceneManager.LoadScene(0);

    }





}
