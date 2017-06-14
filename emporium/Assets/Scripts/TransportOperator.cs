using System.Collections.Generic;
using UnityEngine;
using SocketIO;
using System.Text;
using System;

public class TransportOperator : MonoBehaviour
{
    // Use this for initialization
    private void Start()
    {
    }

    private void Update()
    {
        Database.Instance.CurrentVehichle.status = "Status: idle";
        DisabledObjectsGameScene.Instance.TransportStatus.text = Database.Instance.CurrentVehichle.status;

        if (Database.Instance.TransportJobList.Count != 0)
        {
            foreach (TransportJob job in Database.Instance.TransportJobList) //jobs updateris
            {
                string finishedString;
                if (job.START_OF_TRANSPORTATION + job.LENGTH_OF_TRANSPORTATION <= DisabledObjectsGameScene.Instance.SocketManager.unix && !job.AskedForVerif) //jobs done, send off for verification
                {
                    Debug.Log("ok asking for verif");
                    AskForTransportArrivalVerification(job);
                    finishedString = 0.ToString();
                    job.AskedForVerif = true;
                }
                else
                {
                    TimeSpan ts = TimeSpan.FromSeconds((job.START_OF_TRANSPORTATION + job.LENGTH_OF_TRANSPORTATION) - DisabledObjectsGameScene.Instance.SocketManager.unix);

                    finishedString = string.Format("{0:D2}:{1:D2}:{2:D2}", ts.Hours, ts.Minutes, ts.Seconds);
                }

                //TIK VIENAS JOB sitoj implementacijoj gali but!
                Database.Instance.CurrentVehichle.status = "busy | Time left: " + finishedString;
                DisabledObjectsGameScene.Instance.TransportStatus.text = Database.Instance.CurrentVehichle.status;
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
        Debug.Log(Database.Instance.TransportJobList[0].LENGTH_OF_TRANSPORTATION);
    }

    public void AskForTransportArrivalVerification(TransportJob job)
    {
        Database.Instance.TransportJobList.Remove(job);
        Dictionary<string, string> dic = new Dictionary<string, string>();
        dic["ID"] = job.ID.ToString();
        dic["Uname"] = Database.Instance.UserUsername;

        DisabledObjectsGameScene.Instance.socket.Emit("VERIFY_SOLD_PRODUCE", new JSONObject(dic));
    }
}