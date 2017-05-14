using System.Collections.Generic;
using UnityEngine;
using SocketIO;
using System.Text;

public class TransportOperator : MonoBehaviour
{
    // Use this for initialization
    private void Start()
    {
    }

    private void Update()
    {
        foreach (TransportJob job in Database.Instance.TransportJobs) //jobs updateris
        {
            if (job.START_OF_TRANSPORTATION + job.LENGTH_OF_TRANSPORTATION >= DisabledObjectsGameScene.Instance.SocketManager.unix) //jobs done, send off for verification
            {
                DisabledObjectsGameScene.Instance.managerialScripts.GetComponent<ProduceSelling>().AskForSaleVerification(job.sale);
            }
        }
    }

    public void AssignTransport()
    {
    }

    public void AssignTransportQueues(SocketIOEvent evt)
    {
        string evtStringRows = evt.data.ToString();

        StringBuilder builder = new StringBuilder(evtStringRows);///////////////////////////////////////////////////////
        builder.Replace("rows", "Items");/////////////////////////JSON string paruosiamas konvertavimui i class object array.
        string evtStringItems = builder.ToString(); //////////////////////////////////////////////////////////////////////

        Database.Instance.TransportJobs = JsonHelper.FromJson<TransportJob>(evtStringItems);//converting & assignment

        Debug.Log("we did it reddit");
    }

    public void AskForTransportArrivalVerification(TransportJob job)
    {
        //send out
    }
}