using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;
using System.Text.RegularExpressions;
using System.Text;

public class PriceManager : MonoBehaviour
{


    private bool firstPricesTaken = false;



    // Update is called once per frame


    public void retrievePrices()
    {

        DisabledObjectsGameScene.Instance.managerialScripts.GetComponent<SocketManager>().RetrievePrices();

    }
    public void ResolvePrices(SocketIOEvent evt)
    {


        string evtStringRows = evt.data.ToString();


        StringBuilder builder = new StringBuilder(evtStringRows);///////////////////////////////////////////////////////
        builder.Replace("rows", "Items");/////////////////////////JSON string paruosiamas konvertavimui i class object array.
        string evtStringItems = builder.ToString(); //////////////////////////////////////////////////////////////////////


        if (!firstPricesTaken)
        {
            firstPricesTaken = true;
            Database.Instance.prices = JsonHelper.FromJson<Prices>(evtStringItems);
            Database.Instance.oldPrices = Database.Instance.prices; //nes pries tai isvis nebuvo prices.

            Database.Instance.Prices = HelperScripts.Instance.ReassignPrices(Database.Instance.prices);
            Database.Instance.Oldprices = Database.Instance.Prices;
            DisabledObjectsGameScene.Instance.EconomyPanel.GetComponent<EconomyPanelScript>().StartPriceUpdates();

        }
        else
        {

            Database.Instance.Oldprices = Database.Instance.Prices;  //pagriebiam pries atnaujinant, kad parodytume difference
            Database.Instance.prices = JsonHelper.FromJson<Prices>(evtStringItems);
            Database.Instance.Prices = HelperScripts.Instance.ReassignPrices(Database.Instance.prices);
        }

        DisabledObjectsGameScene.Instance.EconomyPanel.GetComponent<EconomyPanelScript>().Adapt();

    }

    public Prices SalePanelAdaptingPrices()
    {


        return Database.Instance.prices[0];
    }


}
