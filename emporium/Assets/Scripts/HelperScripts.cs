using System.Collections.Generic;
using UnityEngine;

public class HelperScripts : MonoBehaviour
{
    public static HelperScripts Instance;

    private int testInt = 0;

    private void Start()
    {
    }

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("-------------testfunc-----------");
            testThing2();
        }
    }

    private void testThing()
    {
        Dictionary<string, string> dic = new Dictionary<string, string>();

        dic[0 + "amount"] = 10.ToString();
        dic[0 + "name"] = "kriauses";
        dic["salesNum"] = 1.ToString();
        dic["Uname"] = Database.Instance.UserUsername.ToString();

        TransportJob job = new TransportJob();
        job.DEST = "shop";
        job.START_OF_TRANSPORTATION = DisabledObjectsGameScene.Instance.SocketManager.unix;
        job.LENGTH_OF_TRANSPORTATION = int.Parse(Database.Instance.CurrentVehichle.time.ToString());

        Database.Instance.TransportJobList.Add(job);
        dic["Dest"] = "shop";
        dic["Transport"] = Database.Instance.CurrentVehichle.Name;
        dic["TransportID"] = Database.Instance.CurrentVehichle.ID.ToString();
        dic["IndexInJobList"] = Database.Instance.TransportJobList.IndexOf(job).ToString();

        Debug.Log("index is " + dic["IndexInJobList"]);

        DisabledObjectsGameScene.Instance.managerialScripts.GetComponent<ProduceSelling>().AskForSaleJobAssignment(dic);
    }

    private void testThing2()
    {
        GameAlerts.Instance.AlertWithMessage("testsetset");
    }

    public bool LyginisPlotsize()
    {
        bool lygnelyg = true;

        if (Database.Instance.UserPlotSize % 2 == 0) //lyginis plot dimensions
        {
            lygnelyg = true;
        }
        else if (Database.Instance.UserPlotSize % 2 == 1)//NElyginis plot dimensions
        {
            lygnelyg = false;
        }
        return lygnelyg;
    }

    public Dictionary<string, float> ReassignInventory(Inventory inv)
    {
        Dictionary<string, float> dic = new Dictionary<string, float>();

        //ADD NEW JUICES AND FRUITS

        dic.Add("kriauses", inv.kriauses);
        dic.Add("kriauses_sultys", inv.kriauses_sultys);
        dic.Add("apelsinai", inv.apelsinai);
        dic.Add("apelsinai_sultys", inv.apelsinai_sultys);
        dic.Add("arbuzai", inv.arbuzai);
        dic.Add("arbuzai_sultys", inv.arbuzai_sultys);
        dic.Add("bananai", inv.bananai);
        dic.Add("bananai_sultys", inv.bananai_sultys);
        dic.Add("obuoliai", inv.obuoliai);
        dic.Add("obuoliai_sultys", inv.obuoliai_sultys);
        dic.Add("slyvos", inv.slyvos);
        dic.Add("slyvos_sultys", inv.slyvos_sultys);
        dic.Add("vysnios", inv.vysnios);
        dic.Add("vysnios_sultys", inv.vysnios_sultys);
        dic.Add("persikai", inv.persikai);
        dic.Add("persikai_sultys", inv.persikai_sultys);
        dic.Add("kiviai", inv.kiviai);
        dic.Add("kiviai_sultys", inv.kiviai_sultys);
        dic.Add("nektarinai", inv.nektarinai);
        dic.Add("nektarinai_sultys", inv.nektarinai_sultys);

        return dic;
    }

    public void ReassignTransportJobs(TransportJob[] jobs)
    {
        foreach (TransportJob job in jobs)
        {
            Database.Instance.TransportJobList.Add(job);
        }
    }

    public void ReassignReceivedWorkers(Worker[] workers)
    {
        foreach (Worker worker in workers)
        {
            Database.Instance.HiredWorkerList.Add(worker);
        }
    }

    public void ReassignReceivedAvailableWorkers(Worker[] workers)
    {
        foreach (Worker worker in workers)
        {
            Database.Instance.AvailableWorkerList.Add(worker);
        }
    }

    public Dictionary<string, float> ReassignPrices(Prices[] pric)
    {
        Dictionary<string, float> dic = new Dictionary<string, float>();

        foreach (Prices price in pric)
        {
            dic.Add(price.NAME, price.PRICE);
        }

        return dic;
    }
}