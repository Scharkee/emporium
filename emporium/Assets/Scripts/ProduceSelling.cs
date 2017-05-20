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

    private SocketIOComponent socket;

    // Use this for initialization
    private void Start()
    {
        socket = DisabledObjectsGameScene.Instance.socket;
        socket.On("SALE_VERIFICATION", ReceiveSaleVerification);
        socket.On("SALE_JOB_VERIFICATION", ReceiveSaleJobAssignmentVerification);
    }

    public void AdaptPrices()
    {
        DisabledObjectsGameScene.Instance.SellingPanel.transform.BroadcastMessage("AdaptListingPrices");
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
        if (float.Parse(GameObject.Find("Selling_totalsPanel_total_edit").GetComponent<Text>().text) > Database.Instance.CurrentVehichle.amount) //netilps
        {
            GameAlerts.Instance.AlertWithMessage("Your current transport cannot support this amount of weight!");
        }
        else
        {
            Dictionary<string, string> dic = formSalePackage();
            TransportJob job = new TransportJob();
            job.DEST = "shop";
            job.START_OF_TRANSPORTATION = DisabledObjectsGameScene.Instance.SocketManager.unix;
            job.LENGTH_OF_TRANSPORTATION = int.Parse(Database.Instance.CurrentVehichle.time.ToString());

            Database.Instance.TransportJobList.Add(job);
            dic["Dest"] = "shop";
            dic["Transport"] = Database.Instance.CurrentVehichle.Name;
            dic["TransportID"] = Database.Instance.CurrentVehichle.ID.ToString();
            dic["IndexInJobList"] = Database.Instance.TransportJobList.IndexOf(job).ToString();

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
            if (GameObject.Find("SellListItem_produce_InputField_" + listItem.GetComponent<UniversalBank>().produceName).GetComponent<InputField>().text != "")
            {
                sale[salesNum + "name"] = listItem.GetComponent<UniversalBank>().produceName;
                sale[salesNum + "amount"] = GameObject.Find("SellListItem_produce_InputField_" + listItem.GetComponent<UniversalBank>().produceName).GetComponent<InputField>().text;
                salesNum++;
            }
            if (GameObject.Find("SellListItem_juice_InputField_" + listItem.GetComponent<UniversalBank>().produceName).GetComponent<InputField>().text != "")
            {
                sale[salesNum + "name"] = listItem.GetComponent<UniversalBank>().produceName + "_sultys";
                sale[salesNum + "amount"] = GameObject.Find("SellListItem_juice_InputField_" + listItem.GetComponent<UniversalBank>().produceName).GetComponent<InputField>().text;
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