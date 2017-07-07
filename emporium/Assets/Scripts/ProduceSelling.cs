using SocketIO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProduceSelling : MonoBehaviour
{
    private Prices newPrices;
    private Prices oldprices;
    public ListItemPrice totalWeightCache;
    public Text SalePanelTotalstext;
    private GameObject salePanelItemPrefab, currentListItem;
    private GameObject ecoPanelItemPrefab, currentEcoListItem;

    private SocketIOComponent socket;

    // Use this for initialization
    private void Start()
    {
        salePanelItemPrefab = Resources.Load("UI/Selling_ListItem") as GameObject;
        ecoPanelItemPrefab = Resources.Load("UI/EconomyPanel_ListItem") as GameObject;
        socket = DisabledObjectsGameScene.Instance.socket;
        socket.On("SALE_VERIFICATION", ReceiveSaleVerification);
        socket.On("SALE_JOB_VERIFICATION", ReceiveSaleJobAssignmentVerification);
    }

    public void AdaptPrices()
    {
        DisabledObjectsGameScene.Instance.SellingPanel.transform.BroadcastMessage("AdaptListingPrices");
    }

    public void GeneratePanels() //skirta sugeneruoti salePanel + economy panel
    {
        foreach (KeyValuePair<string, float> item in Database.Instance.Prices)
        {
            if (item.Key.Contains("_sultys")) //sultys nesiskaito sitam spawninime, reikia tik produce names
            {
            }
            else
            {
                //SPAWNING ENTRY FOR SELLING PANEL
                currentListItem = Instantiate(salePanelItemPrefab, DisabledObjectsGameScene.Instance.Selling_Salepanel.transform) as GameObject;
                currentListItem.GetComponent<UniversalBank>().produceName = item.Key;
                currentListItem.transform.Find("SellListItem_name").GetComponent<Text>().text = Languages.Instance.currentLanguage[item.Key];

                //SPAWNING ENTRY FOR ECONOMY PANEL
                currentListItem = Instantiate(ecoPanelItemPrefab, DisabledObjectsGameScene.Instance.EconomyPanel_panel.transform) as GameObject;
                currentListItem.GetComponent<EconomyPanelListItem>().bankProduce.produceName = item.Key;
                currentListItem.GetComponent<EconomyPanelListItem>().bankJuice.produceName = item.Key + "_sultys";
                currentListItem.transform.Find("EconomyPanelListItem_name").GetComponent<Text>().text = Languages.Instance.currentLanguage[item.Key];
            }
        }
    }

    public void AskForSaleJobAssignment(Dictionary<string, string> sale)
    {
        socket.Emit("VERIFY_SOLD_PRODUCE_STORE", new JSONObject(sale));
    }

    public void ReceiveSaleVerification(SocketIOEvent evt)
    {
        Database.Instance.UserDollars = float.Parse(evt.data.GetField("dollars").ToString());
        DisabledObjectsGameScene.Instance.Inventory_Fruit_panel.GetComponent<InventoryPanel>().adjustValues();
    }

    public void ReceiveSaleJobAssignmentVerification(SocketIOEvent evt)
    {
        //assignint ID in database i ta job
        Database.Instance.TransportJobList[int.Parse(evt.data.GetField("IndexInJobList").ToString())].ID = int.Parse(evt.data.GetField("ID").ToString());
    }

    public void SaleClick()
    {
        if (DisabledObjectsGameScene.Instance.SellingPanel.transform.Find("Selling_totals_panel").GetComponent<ListItemPrice>().CurrentWeightTotal > Database.Instance.CurrentVehichle.amount) //netilps
        {
            GameAlerts.Instance.AlertWithMessage(Languages.Instance.currentLanguage["transport_cannot_support_weight"]);
        }
        else
        {
            Debug.Log("packing0");
            Dictionary<string, string> dic = formSalePackage();
            TransportJob job = new TransportJob();
            job.DEST = "shop";
            job.START_OF_TRANSPORTATION = DisabledObjectsGameScene.Instance.SocketManager.unix;
            job.LENGTH_OF_TRANSPORTATION = int.Parse(Database.Instance.CurrentVehichle.time.ToString());

            Debug.Log("packing1");
            Database.Instance.TransportJobList.Add(job);
            dic["Dest"] = "shop";
            dic["Transport"] = Database.Instance.CurrentVehichle.Name;
            dic["TransportID"] = Database.Instance.CurrentVehichle.ID.ToString();
            dic["IndexInJobList"] = Database.Instance.TransportJobList.IndexOf(job).ToString();

            Debug.Log("packing2");

            DisabledObjectsGameScene.Instance.managerialScripts.GetComponent<ProduceSelling>().AskForSaleJobAssignment(dic);
            resetSellingPanel();
        }
    }

    public void CancelContext()
    {
        resetSellingPanel();
    }

    public void SellAllClick()
    {
        DisabledObjectsGameScene.Instance.SellingPanel.BroadcastMessage("maxOutValues");
    }

    public void ResetClick()
    {
        DisabledObjectsGameScene.Instance.SellingPanel.BroadcastMessage("resetValues");
    }

    private Dictionary<string, string> formSalePackage()
    {
        Dictionary<string, string> sale = new Dictionary<string, string>();
        int salesNum = 0;

        foreach (Transform listItem in DisabledObjectsGameScene.Instance.Selling_Salepanel.transform)
        {
            if (listItem.Find("SellListItem_produce/SellListItem_produce_InputField").gameObject.GetComponent<InputField>().text != "")
            {
                sale[salesNum + "name"] = listItem.GetComponent<UniversalBank>().produceName;
                sale[salesNum + "amount"] = GameObject.Find("SellListItem_produce_InputField").GetComponent<InputField>().text;
                salesNum++;
            }
            if (listItem.Find("SellListItem_juice/SellListItem_juice_InputField").gameObject.GetComponent<InputField>().text != "")
            {
                sale[salesNum + "name"] = listItem.GetComponent<UniversalBank>().produceName + "_sultys";
                sale[salesNum + "amount"] = GameObject.Find("SellListItem_juice_InputField").GetComponent<InputField>().text;
                salesNum++;
            }
        }

        sale["Uname"] = Database.Instance.UserUsername.ToString();
        sale["salesNum"] = salesNum.ToString();

        return sale;
    }

    private void resetSellingPanel()
    {
        //savarankiskai issitrina visi values pries uzdaryma
        DisabledObjectsGameScene.Instance.SellingPanel.BroadcastMessage("resetValues");

        GameObject.Find("SellingButton").GetComponent<SellingButtonScript>().expandContract();
    }
}