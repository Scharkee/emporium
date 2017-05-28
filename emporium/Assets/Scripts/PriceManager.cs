using SocketIO;
using System.Text;
using UnityEngine;

public class PriceManager : MonoBehaviour
{
    private bool firstPricesTaken = false;

    public int priceUpdateInterval = 30; //CHANGE ON RELEASE

    public long nextUpdate = 9999999999999;

    private void Update()
    {
        if (DisabledObjectsGameScene.Instance.SocketManager.unix >= nextUpdate)
        {
            nextUpdate = DisabledObjectsGameScene.Instance.SocketManager.unix + priceUpdateInterval;
            retrievePrices();

            if (DisabledObjectsGameScene.Instance.SellingPanel.activeSelf)
            {
                DisabledObjectsGameScene.Instance.managerialScripts.GetComponent<ProduceSelling>().AdaptPrices();
            }

            if (DisabledObjectsGameScene.Instance.EconomyPanel.activeSelf)
            {
                DisabledObjectsGameScene.Instance.EconomyPanel.GetComponent<EconomyPanelScript>().Adapt();
            }
        }
    }

    public void StartPriceUpdates()
    {
        DisabledObjectsGameScene.Instance.pricemanager.retrievePrices();
        nextUpdate = DisabledObjectsGameScene.Instance.SocketManager.unix + priceUpdateInterval;
    }

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
            StartPriceUpdates();
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