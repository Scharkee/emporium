using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;
using UnityEngine.UI;

public class ProduceSelling : MonoBehaviour {


    SocketIOComponent socket;
	// Use this for initialization
	void Start () {

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


    }

    public void SaleClick()
    {


        //TODO: checkas ar yra pakamkamai in inventory
        Dictionary<string, string> sale = new Dictionary<string, string>();
        int salesNum = 0;

        if (GameObject.Find("SellListItem_juice_InputField_apples").GetComponent<InputField>().text!="")
        {
            sale[salesNum + "name"] = "obuoliai";
            sale[salesNum + "amount"] = GameObject.Find("SellListItem_juice_InputField_apples").GetComponent<InputField>().text;
            salesNum++;
        }
        if (GameObject.Find("SellListItem_produce_InputField_apples").GetComponent<InputField>().text!="")
        {
            sale[salesNum + "name"] = "obuoliai_sultys";
            sale[salesNum + "amount"] = GameObject.Find("SellListItem_produce_InputField_apples").GetComponent<InputField>().text;
            salesNum++;
        }
        if (GameObject.Find("SellListItem_produce_InputField_pears").GetComponent<InputField>().text!="")
        {
            sale[salesNum + "name"] = "kriauses";
            sale[salesNum + "amount"] = GameObject.Find("SellListItem_produce_InputField_pears").GetComponent<InputField>().text;
            salesNum++;
        }
        if (GameObject.Find("SellListItem_juice_InputField_pears").GetComponent<InputField>().text!="")
        {
            sale[salesNum + "name"] = "kriauses_sultys";
            sale[salesNum + "amount"] = GameObject.Find("SellListItem_juice_InputField_pears").GetComponent<InputField>().text;
            salesNum++;
        }
        if (GameObject.Find("SellListItem_produce_InputField_oranges").GetComponent<InputField>().text!="")
        {
            sale[salesNum + "name"] = "apelsinai";
            sale[salesNum + "amount"] = GameObject.Find("SellListItem_produce_InputField_oranges").GetComponent<InputField>().text;
            salesNum++;
        }
        if (GameObject.Find("SellListItem_juice_InputField_oranges").GetComponent<InputField>().text!="")
        {
            sale[salesNum + "name"] = "apelsinai_sultys";
            sale[salesNum + "amount"] = GameObject.Find("SellListItem_juice_InputField_oranges").GetComponent<InputField>().text;
            salesNum++;
        }
        if (GameObject.Find("SellListItem_produce_InputField_plums").GetComponent<InputField>().text!="")
        {
            sale[salesNum + "name"] = "slyvos";
            sale[salesNum + "amount"] = GameObject.Find("SellListItem_produce_InputField_plums").GetComponent<InputField>().text;
            salesNum++;
        }
        if (GameObject.Find("SellListItem_juice_InputField_plums").GetComponent<InputField>().text!="")
        {
            sale[salesNum + "name"] = "slyvos_sultys";
            sale[salesNum + "amount"] = GameObject.Find("SellListItem_juice_InputField_plums").GetComponent<InputField>().text;
            salesNum++;
        }
        if (GameObject.Find("SellListItem_produce_InputField_peaches").GetComponent<InputField>().text!="")
        {
            sale[salesNum + "name"] = "persikai";
            sale[salesNum + "amount"] = GameObject.Find("SellListItem_produce_InputField_peaches").GetComponent<InputField>().text;
            salesNum++;
        }
        if (GameObject.Find("SellListItem_juice_InputField_peaches").GetComponent<InputField>().text!="")
        {
            sale[salesNum + "name"] = "persikai_sultys";
            sale[salesNum + "amount"] = GameObject.Find("SellListItem_juice_InputField_peaches").GetComponent<InputField>().text;
            salesNum++;
        }
        if (GameObject.Find("SellListItem_produce_InputField_nectarines").GetComponent<InputField>().text!="")
        {
            sale[salesNum + "name"] = "nektarinai";
            sale[salesNum + "amount"] = GameObject.Find("SellListItem_produce_InputField_nectarines").GetComponent<InputField>().text;
            salesNum++;
        }
        if (GameObject.Find("SellListItem_juice_InputField_nectarines").GetComponent<InputField>().text!="")
        {
            sale[salesNum + "name"] = "nektarinai_sultys";
            sale[salesNum + "amount"] = GameObject.Find("SellListItem_juice_InputField_nectarines").GetComponent<InputField>().text;
            salesNum++;
        }
        if (GameObject.Find("SellListItem_produce_InputField_kiwis").GetComponent<InputField>().text!="")
        {
            sale[salesNum + "name"] = "kiviai";
            sale[salesNum + "amount"] = GameObject.Find("SellListItem_produce_InputField_kiwis").GetComponent<InputField>().text;
            salesNum++;
        }
        if (GameObject.Find("SellListItem_juice_InputField_kiwis").GetComponent<InputField>().text!="")
        {
            sale[salesNum + "name"] = "kiviai_sultys";
            sale[salesNum + "amount"] = GameObject.Find("SellListItem_juice_InputField_kiwis").GetComponent<InputField>().text;
            salesNum++;
        }



        //TODO: resetSellingPanel(); (grazint viska i 0 ir isjungt.)
        GameObject.Find("SellingButton").GetComponent<SellingButtonScript>().expandContract();



        //final things

        sale["Uname"] = Database.Instance.UserUsername.ToString();
        sale["salesNum"] = salesNum.ToString();

        AskForSaleVerification(sale);
    }
	
	
}
