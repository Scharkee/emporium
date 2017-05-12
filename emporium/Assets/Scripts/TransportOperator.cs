using System.Collections.Generic;
using UnityEngine;

public class TransportOperator : MonoBehaviour
{
    public List<TransportJob> transportJobs = new List<TransportJob>();

    // Use this for initialization
    private void Start()
    {
    }

    private void Update()
    {
        foreach (TransportJob job in transportJobs)
        {
            if (job.time <= 0) //jobs done, send off for verification
            {
                DisabledObjectsGameScene.Instance.managerialScripts.GetComponent<ProduceSelling>().AskForSaleVerification(job.sale);
            }
            else
            {
                job.time -= Time.deltaTime;
            }
        }
    }

    public void AssignTransport()
    {
    }
}