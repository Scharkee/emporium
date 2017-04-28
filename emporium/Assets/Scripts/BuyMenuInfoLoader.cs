using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class BuyMenuInfoLoader : MonoBehaviour
{

    public static BuyMenuInfoLoader Instance;

    // Use this for initialization
    void Start()
    {

    }

    void Awake()
    {
        Instance = this;


    }



    public void LoadBuyMenuInfo()
    {

        foreach (Building building in Database.Instance.buildinginfo)
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
        DisabledObjectsGameScene.Instance.gridBuildings.SetActive(false);
        DisabledObjectsGameScene.Instance.BuyMenuPanel.SetActive(false);
    }
}
