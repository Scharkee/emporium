using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SellListItem_maxButtonScript : MonoBehaviour
{


    public InputField inp;
    public string prodName;



    public void TheClick()
    {
        inp.text = Database.Instance.Inventory[prodName].ToString();

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
        Debug.Log("WOO");
        //kad negaletu parduot daugiau negu turi.
        if (float.Parse(inp.text) > Database.Instance.Inventory[prodName])
        {
            inp.text = Database.Instance.Inventory[prodName].ToString();

        }



    }


}
