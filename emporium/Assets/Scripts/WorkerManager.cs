using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;
using System.Text;

public class WorkerManager : MonoBehaviour
{
    // Use this for initialization
    private void Start()
    {
    }

    public void AssignReceivedWorkers(SocketIOEvent evt)
    {
        string evtStringRows = evt.data.ToString();

        StringBuilder builder = new StringBuilder(evtStringRows);///////////////////////////////////////////////////////
        builder.Replace("rows", "Items");/////////////////////////JSON string paruosiamas konvertavimui i class object array.
        string evtStringItems = builder.ToString(); //////////////////////////////////////////////////////////////////////

        Database.Instance.ReceivedWorkers = JsonHelper.FromJson<Worker>(evtStringItems);
        HelperScripts.Instance.ReassignReceivedWorkers(Database.Instance.ReceivedWorkers);

        Debug.Log(Database.Instance.ReceivedWorkers);
    }

    public void AssignAvailableReceivedWorkers(SocketIOEvent evt)
    {
        string evtStringRows = evt.data.ToString();

        StringBuilder builder = new StringBuilder(evtStringRows);///////////////////////////////////////////////////////
        builder.Replace("rows", "Items");/////////////////////////JSON string paruosiamas konvertavimui i class object array.
        string evtStringItems = builder.ToString(); //////////////////////////////////////////////////////////////////////

        Database.Instance.ReceivedAvailableWorkers = JsonHelper.FromJson<Worker>(evtStringItems);
        HelperScripts.Instance.ReassignReceivedAvailableWorkers(Database.Instance.ReceivedAvailableWorkers);

        Debug.Log(Database.Instance.ReceivedAvailableWorkers);

        Debug.Log("ok, got workers");
    }

    public void WorkerPanelToggle()
    {
        ClickEngine.Instance.Click();

        Globals.Instance.UIBloomActive(!DisabledObjectsGameScene.Instance.Worker_panel.activeSelf);
        DisabledObjectsGameScene.Instance.Worker_panel.SetActive(!DisabledObjectsGameScene.Instance.Worker_panel.activeSelf);

        if (DisabledObjectsGameScene.Instance.BuyMenuPanel.activeSelf) //buymenu panel is currently open
        {
            StartCoroutine(DisabledObjectsGameScene.Instance.BuyButton.GetComponent<BuyButtonScript>().BuyMenuPanelCloser());
        }
        if (Globals.Instance.cameraUp && DisabledObjectsGameScene.Instance.BuyMode.GetComponent<BuyMode>().enabled) //buy mode is enabled. Cancel buy mode.
        {
            DisabledObjectsGameScene.Instance.BuyMode.GetComponent<BuyMode>().DisableBuyMode(false);
        }
        if (DisabledObjectsGameScene.Instance.tileSellScript.GetComponent<TileSellScript>().sellModeEnabled)
        {
            DisabledObjectsGameScene.Instance.tileSellScript.GetComponent<TileSellScript>().sellModeEnabled = false;
            DisabledObjectsGameScene.Instance.tileSellScript.GetComponent<TileSellScript>().ApplyModeTransition(false);
        }
        if (DisabledObjectsGameScene.Instance.SellingPanel.activeSelf)
        {
            DisabledObjectsGameScene.Instance.SellingPanel.SetActive(false);
        }
        if (DisabledObjectsGameScene.Instance.EconomyPanel.activeSelf)
        {
            DisabledObjectsGameScene.Instance.EconomyPanel.SetActive(false);
        }
    }

    public void CancelContext()
    {
        gameObject.SetActive(false);
    }

    public void PopulateWorkerPanel()
    {
        if (Database.Instance.HiredWorkerList.Count > 0) //yra workeriu pasamdytu
        {
            //istrinam suggestion mygtuka
            Destroy(DisabledObjectsGameScene.Instance.WorkerPanel_panel_hired_getSomeWorkersButton);
        }
    }
}