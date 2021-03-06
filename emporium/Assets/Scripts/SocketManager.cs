﻿using SocketIO;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SocketManager : MonoBehaviour
{
    private bool ver = false;
    public int unix;
    private HelperScripts helperscript;

    private SocketIOComponent socket;

    // Use this for initialization
    private void Start()
    {
        socket = DisabledObjectsGameScene.Instance.socket;

        StartCoroutine(UnixUpdater());
        StartCoroutine(delayedPrices());

        socket.On("VERIFY", Verification);
        socket.On("DISCREPANCY", DiscrepancyS);
        socket.On("RECEIVE_UNIX", ReceiveUnix);

        socket.On("BUILD_TILE", DisabledObjectsGameScene.Instance.managerialScripts.GetComponent<AssignTiles>().BuildTile);

        socket.On("NO_FUNDS", NoFundsAlert);

        socket.On("ADD_FUNDS", AddFunds);
        socket.On("UPDATE_PLOT_SIZE", UpdatePlotSize);
        socket.On("UPGRADE_TILE", UpgradeTile);
        socket.On("RECEIVE_PRICES", ReceivePrices);
        socket.On("RECEIVE_TRANSPORT_QUEUE", ReceiveTransportQueues);
        socket.On("RECEIVE_WORKERS", ReceiveWorkers);
        socket.On("RECEIVE_AVAILABLE_WORKERS", ReceiveAvailableWorkers);

        socket.On("WORKER_HIRED_CONF", handleWorkerHiredConfirmation);

        //insta calls for starting up
        RetrievePrices();
    }

    private IEnumerator delayedPrices()
    {
        for (int i = 0; i < 5; i++)
        {
            yield return new WaitForSeconds(0.1f);
        }
        RetrievePrices();
    }

    private IEnumerator UnixUpdater()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            GetUnix();
        }
    }

    private void GetUnix()
    {
        socket.Emit("GET_UNIX");
    }

    private IEnumerator WaitForVerify(int s)
    {
        yield return new WaitForSeconds(5);
        if (ver == false)
        {
            Debug.Log("bad verification!");
        }
    }

    private void Verification(SocketIOEvent evt)
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

        GameAlerts.Instance.AlertWithMessage(Languages.Instance.currentLanguage["plotsize_upgraded"]);
        StartCoroutine(logOffWithDelay(2f));
    }

    public void LogOffWithDelay(float delay)
    {
        ContextManager.Instance.CancelContext();
        StartCoroutine(logOffWithDelay(delay));
    }

    public void DiscrepancyAction() //check ar veikia
    {
        GameAlerts.Instance.AlertWithMessage(Languages.Instance.currentLanguage["desync_detected"]);
        StartCoroutine(logOffWithDelay(1f));

        ContextManager.Instance.CancelContext();
    }

    public void DiscrepancyS(SocketIOEvent evt) //check ar veikia
    {
        GameAlerts.Instance.AlertWithMessage(evt.data.GetField("reasonString").ToString());
        StartCoroutine(logOffWithDelay(2f));

        ContextManager.Instance.CancelContext();
    }

    private void UpgradeTile(SocketIOEvent evt)
    {
        int tileID = int.Parse(Regex.Replace(evt.data.GetField("tileID").ToString(), "[^0-9]", ""));

        DisabledObjectsGameScene.Instance.managerialScripts.GetComponent<AssignTiles>().UpgradeTile(tileID);
        Debug.Log("socketman past ID");
    }

    private void ReceiveUnix(SocketIOEvent evt)
    {
        unix = int.Parse(Regex.Replace(evt.data.GetField("unixBuffer").ToString(), "[^0-9]", "")); //FIXME this is dumb
    }

    private void NoFundsAlert(SocketIOEvent evt)
    {
        ContextManager.Instance.CancelContext();

        GameAlerts.Instance.AlertWithMessage(Languages.Instance.currentLanguage["not_enough_money"]); //TODO: finsih and test this.
    }

    private void AddFunds(SocketIOEvent evt)
    {
        float additive = float.Parse(evt.data.GetField("addFunds").ToString());

        Database.Instance.UserDollars += additive;
    }

    private void handleWorkerHiredConfirmation(SocketIOEvent evt)
    {
        //pridet prie hired workers listo + spawn entry in hired worker tab
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
    }

    public void ReceivePrices(SocketIOEvent evt)
    {
        DisabledObjectsGameScene.Instance.pricemanager.ResolvePrices(evt);
    }

    public void ReceiveTransportQueues(SocketIOEvent evt)
    {
        DisabledObjectsGameScene.Instance.TransportOperator.AssignTransportQueues(evt);
    }

    public void ReceiveWorkers(SocketIOEvent evt)
    {
        DisabledObjectsGameScene.Instance.WorkerManager.AssignReceivedWorkers(evt);
    }

    public void ReceiveAvailableWorkers(SocketIOEvent evt)
    {
        DisabledObjectsGameScene.Instance.WorkerManager.AssignAvailableReceivedWorkers(evt);
    }

    public IEnumerator logOffWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        GlobalControl.Instance.reset();
        socket.reconnectDelay = 1;
        socket.Close();
        socket.Connect();
        SceneManager.LoadScene(0);
    }
}