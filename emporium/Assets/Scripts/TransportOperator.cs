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
        if (Database.Instance.TransportJobs.Length != 0)
        {
            foreach (TransportJob job in Database.Instance.TransportJobs) //jobs updateris
            {
                if (job.START_OF_TRANSPORTATION + job.LENGTH_OF_TRANSPORTATION >= DisabledObjectsGameScene.Instance.SocketManager.unix) //jobs done, send off for verification
                {
                    AskForTransportArrivalVerification(job);
                }
            }
        }
    }

    public void AssignTransportQueues(SocketIOEvent evt)
    {
        string evtStringRows = evt.data.ToString();

        StringBuilder builder = new StringBuilder(evtStringRows);///////////////////////////////////////////////////////
        builder.Replace("rows", "Items");/////////////////////////JSON string paruosiamas konvertavimui i class object array.
        string evtStringItems = builder.ToString(); //////////////////////////////////////////////////////////////////////

        Database.Instance.TransportJobs = JsonHelper.FromJson<TransportJob>(evtStringItems);
        HelperScripts.Instance.ReassignTransportJobs(Database.Instance.TransportJobs);
    }

    public void AskForTransportArrivalVerification(TransportJob job)
    {
        Dictionary<string, string> dic = new Dictionary<string, string>();
        dic["ID"] = job.ID.ToString();

        DisabledObjectsGameScene.Instance.socket.Emit("VERIFY_SOLD_PRODUCE", new JSONObject(dic));
    }
}