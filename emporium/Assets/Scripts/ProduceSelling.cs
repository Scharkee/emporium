using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;
using UnityEngine.UI;

public class ProduceSelling : MonoBehaviour
{


    SocketIOComponent socket;
    // Use this for initialization
    void Start()
    {

        socket = DisabledObjectsGameScene.Instance.socket;
        socket.On("SALE_VERIFICATION", ReceiveSaleVerification);

    }

    public void AskForSaleVerification(Dictionary<string, string> sale)
    {
        socket.Emit("VERIFY_SOLD_PRODUCE", new JSONObject(sale));

    }
    public void ReceiveSaleVerification(SocketIOEvent evt)
    {

        Database.Instance.UserDollars = float.Parse(evt.data.GetField("dollars").ToString());
        DisabledObjectsGameScene.Instance.Inventory_Fruit_panel.GetComponent<InventoryPanel>().adjustValues();

    }

    public void SaleClick()
    {

        AskForSaleVerification(formSalePackage());

        resetSellingPanel();



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
            sale[salesNum + "amount"] = GameObject.Find("SellListItem_juice_InputField_apples").GetComponent<InputField>().text;
            salesNum++;
        }
        if (GameObject.Find("SellListItem_juice_InputField_apples").GetComponent<InputField>().text != "")
        {
            sale[salesNum + "name"] = "obuoliai_sultys";
            sale[salesNum + "amount"] = GameObject.Find("SellListItem_produce_InputField_apples").GetComponent<InputField>().text;
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
