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
    }
}