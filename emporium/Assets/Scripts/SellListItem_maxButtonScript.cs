using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SellListItem_maxButtonScript : MonoBehaviour {


    public InputField inp;
    public string name;

	// Use this for initialization
	void Start () {
		
	}


    public void TheClick()
    {
        inp.text = Database.Instance.Inventory[name].ToString();



    }
	
	
}
