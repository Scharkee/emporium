using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class BuyMenuInfoLoader : MonoBehaviour {

 

	// Use this for initialization
	void Start () {
     
	}
	

    public static void LoadBuyMenuInfo()
    {
       
        foreach (Building building in Database.buildinginfo)
        {

            if (building.BUILDING_TYPE == 0)
            {
                
              
                GameObject.Find("OPGridText_" + building.NAME + "_price_editable").GetComponent<Text>().text = building.PRICE.ToString();
                TimeSpan ts = TimeSpan.FromSeconds(building.PROG_AMOUNT);
               
                GameObject.Find("OPGridText_" + building.NAME + "_time_editable").GetComponent<Text>().text = string.Format("{0:D2}:{1:D2}:{2:D2}", ts.Hours, ts.Minutes, ts.Seconds);



            }
            else
            {

          
                GameObject.Find("OPGridText_" + building.NAME + "_price_editable").GetComponent<Text>().text = building.PRICE.ToString();
                GameObject.Find("OPGridText_" + building.NAME + "_time_editable").GetComponent<Text>().text = "Depends on load.";
            }
 
        }
        DisabledObjectsGameScene.gridBuildings.SetActive(false);
        DisabledObjectsGameScene.BuyMenuPanel.SetActive(false);
    }
}
