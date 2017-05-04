using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SellListItem_maxButtonScript : MonoBehaviour
{


    public InputField inp;
    public string prodName;
    public Text price;
    public Text SalePanelTotalstext;
    private float lastValue = 0;



    public void TheClick()
    {
        inp.text = Database.Instance.Inventory[prodName].ToString();

    }

    private void Start()
    {
        SalePanelTotalstext = DisabledObjectsGameScene.Instance.SellingPanel.transform.FindChild("Selling_totals_panel").transform.FindChild("Selling_totalsPanel_total_edit").gameObject.GetComponent<Text>();

    }

    public void resetValues()
    {

        inp.text = "";
    }

    public void maxOutValues()
    {
        inp.text = Database.Instance.Inventory[prodName].ToString();
    }

    public void KeepAtMaxValues(string str)
    {
       
        //kad negaletu parduot daugiau negu turi.
        if (float.Parse(inp.text) > Database.Instance.Inventory[prodName])
        {
            inp.text = Database.Instance.Inventory[prodName].ToString();

        }


        //pritaikau price price taip pat apacioj
        float newprice = float.Parse(inp.text);
        float old = lastValue;
        //check if works
        price.text =""+(float.Parse(price.text)- old) +(newprice*Database.Instance.Prices[prodName]);
        SalePanelTotalstext.text = "" + (float.Parse(SalePanelTotalstext.text) - old) + (newprice * Database.Instance.Prices[prodName]);

        lastValue = newprice * Database.Instance.Prices[prodName];


    }


}
