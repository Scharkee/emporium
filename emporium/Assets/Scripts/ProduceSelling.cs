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

    public void AskForSaleVerification(Dictionary<string, string> sale)
    {
        socket.Emit("VERIFY_SOLD_PRODUCE", new JSONObject(sale));
    }

    public void AskForSaleJobAssignment(Dictionary<string, string> sale)
    {
        socket.Emit("VERIFY_SOLD_PRODUCE_STORE", new JSONObject(sale));
    }

    public void ReceiveSaleVerification(SocketIOEvent evt)
    {
        Debug.Log(float.Parse(evt.data.GetField("dollars").ToString()));
        Database.Instance.UserDollars = float.Parse(evt.data.GetField("dollars").ToString());
        DisabledObjectsGameScene.Instance.Inventory_Fruit_panel.GetComponent<InventoryPanel>().adjustValues();
    }

    public void ReceiveSaleJobAssignmentVerification(SocketIOEvent evt)
    {
        //assignint ID in database i ta job
    }

    public void SaleClick()
    {
        if (float.Parse(GameObject.Find("Selling_totalsPanel_total_edit").GetComponent<Text>().text) > Database.Instance.CurrentVehichle.amount) //netilps
        {
            GameAlerts.Instance.AlertWithMessage("Your current transport cannot support this amount of weight!");
        }
        else
        {
            //TransportJob newJob = new TransportJob();
            //newJob.START_OF_TRANSPORTATION =;
            //newJob.LENGTH_OF_TRANSPORTATION = Database.Instance.CurrentVehichle.time;
            //DisabledObjectsGameScene.Instance.managerialScripts.GetComponent<TransportOperator>().transportJobs.Add(newJob);

            //figure this out
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

        if (GameObject.Find("SellListItem_produce_InputField_apples").GetComponent<InputField>().text != "")
        {
            sale[salesNum + "name"] = "obuoliai";
            sale[salesNum + "amount"] = GameObject.Find("SellListItem_produce_InputField_apples").GetComponent<InputField>().text;
            salesNum++;
        }
        if (GameObject.Find("SellListItem_juice_InputField_apples").GetComponent<InputField>().text != "")
        {
            sale[salesNum + "name"] = "obuoliai_sultys";
            sale[salesNum + "amount"] = GameObject.Find("SellListItem_juice_InputField_apples").GetComponent<InputField>().text;
            salesNum++;
        }
        if (GameObject.Find("SellListItem_produce_InputField_pears").GetComponent<InputField>().text != "")
        {
            sale[salesNum + "name"] = "kriauses";
            sale[salesNum + "amount"] = GameObject.Find("SellListItem_produce_InputField_pears").GetComponent<InputField>().text;
            salesNum++;
        }
        if (GameObject.Find("SellListItem_juice_InputField_pears").GetComponent<InputField>().text != "")
        {
            sale[salesNum + "name"] = "kriauses_sultys";
            sale[salesNum + "amount"] = GameObject.Find("SellListItem_juice_InputField_pears").GetComponent<InputField>().text;
            salesNum++;
        }
        if (GameObject.Find("SellListItem_produce_InputField_oranges").GetComponent<InputField>().text != "")
        {
            sale[salesNum + "name"] = "apelsinai";
            sale[salesNum + "amount"] = GameObject.Find("SellListItem_produce_InputField_oranges").GetComponent<InputField>().text;
            salesNum++;
        }
        if (GameObject.Find("SellListItem_juice_InputField_oranges").GetComponent<InputField>().text != "")
        {
            sale[salesNum + "name"] = "apelsinai_sultys";
            sale[salesNum + "amount"] = GameObject.Find("SellListItem_juice_InputField_oranges").GetComponent<InputField>().text;
            salesNum++;
        }
        if (GameObject.Find("SellListItem_produce_InputField_plums").GetComponent<InputField>().text != "")
        {
            sale[salesNum + "name"] = "slyvos";
            sale[salesNum + "amount"] = GameObject.Find("SellListItem_produce_InputField_plums").GetComponent<InputField>().text;
            salesNum++;
        }
        if (GameObject.Find("SellListItem_juice_InputField_plums").GetComponent<InputField>().text != "")
        {
            sale[salesNum + "name"] = "slyvos_sultys";
            sale[salesNum + "amount"] = GameObject.Find("SellListItem_juice_InputField_plums").GetComponent<InputField>().text;
            salesNum++;
        }
        if (GameObject.Find("SellListItem_produce_InputField_peaches").GetComponent<InputField>().text != "")
        {
            sale[salesNum + "name"] = "persikai";
            sale[salesNum + "amount"] = GameObject.Find("SellListItem_produce_InputField_peaches").GetComponent<InputField>().text;
            salesNum++;
        }
        if (GameObject.Find("SellListItem_juice_InputField_peaches").GetComponent<InputField>().text != "")
        {
            sale[salesNum + "name"] = "persikai_sultys";
            sale[salesNum + "amount"] = GameObject.Find("SellListItem_juice_InputField_peaches").GetComponent<InputField>().text;
            salesNum++;
        }
        if (GameObject.Find("SellListItem_produce_InputField_nectarines").GetComponent<InputField>().text != "")
        {
            sale[salesNum + "name"] = "nektarinai";
            sale[salesNum + "amount"] = GameObject.Find("SellListItem_produce_InputField_nectarines").GetComponent<InputField>().text;
            salesNum++;
        }
        if (GameObject.Find("SellListItem_juice_InputField_nectarines").GetComponent<InputField>().text != "")
        {
            sale[salesNum + "name"] = "nektarinai_sultys";
            sale[salesNum + "amount"] = GameObject.Find("SellListItem_juice_InputField_nectarines").GetComponent<InputField>().text;
            salesNum++;
        }
        if (GameObject.Find("SellListItem_produce_InputField_kiwis").GetComponent<InputField>().text != "")
        {
            sale[salesNum + "name"] = "kiviai";
            sale[salesNum + "amount"] = GameObject.Find("SellListItem_produce_InputField_kiwis").GetComponent<InputField>().text;
            salesNum++;
        }
        if (GameObject.Find("SellListItem_juice_InputField_kiwis").GetComponent<InputField>().text != "")
        {
            sale[salesNum + "name"] = "kiviai_sultys";
            sale[salesNum + "amount"] = GameObject.Find("SellListItem_juice_InputField_kiwis").GetComponent<InputField>().text;
            salesNum++;
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