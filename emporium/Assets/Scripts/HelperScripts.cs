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
            testThing();
        }
    }

    private void testThing()
    {
        GameAlerts.Instance.AlertWithMessage(testInt.ToString());
        testInt++;
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