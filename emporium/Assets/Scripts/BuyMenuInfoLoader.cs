using System;
using UnityEngine;
using UnityEngine.UI;

public class BuyMenuInfoLoader : MonoBehaviour
{
    public static BuyMenuInfoLoader Instance;

    // Use this for initialization
    private void Start()
    {
    }

    private void Awake()
    {
        Instance = this;
    }

    public void LoadBuyMenuInfo()
    {
        foreach (Building building in Database.Instance.buildinginfo)
        {
            if (building.BUILDING_TYPE == 0)//augalas
            {
                GameObject.Find("OPGridText_" + building.NAME + "_price_editable").GetComponent<Text>().text = building.PRICE.ToString();
                TimeSpan ts = TimeSpan.FromSeconds(building.PROG_AMOUNT);

                GameObject.Find("OPGridText_" + building.NAME + "_time_editable").GetComponent<Text>().text = string.Format("{0:D2}:{1:D2}:{2:D2}", ts.Hours, ts.Minutes, ts.Seconds);
                GameObject.Find("OPGridText_" + building.NAME + "_efic").GetComponent<Text>().text = "Yield: ";
                GameObject.Find("OPGridText_" + building.NAME + "_efic_editable").GetComponent<Text>().text = building.TILEPRODUCERANDOM1 + " - " + building.TILEPRODUCERANDOM2 + " KG";
            }
            else if (building.BUILDING_TYPE == 1) //presas
            {
                GameObject.Find("OPGridText_" + building.NAME + "_efic_editable").GetComponent<Text>().text = building.EFIC * 100 + "%";
                GameObject.Find("OPGridText_" + building.NAME + "_efic").GetComponent<Text>().text = "Efficiency:";

                GameObject.Find("OPGridText_" + building.NAME + "_price_editable").GetComponent<Text>().text = building.PRICE.ToString();
                GameObject.Find("OPGridText_" + building.NAME + "_time_editable").GetComponent<Text>().text = "1 KG/s.";
            }
            else if (building.BUILDING_TYPE == 2) //transporto priemone
            {
                GameObject.Find("OPGridText_" + building.NAME + "_price_editable").GetComponent<Text>().text = building.PRICE.ToString();
                GameObject.Find("OPGridText_" + building.NAME + "_time_editable").GetComponent<Text>().text = building.PROG_AMOUNT + " seconds per job.";
                GameObject.Find("OPGridText_" + building.NAME + "_time").GetComponent<Text>().text = "Job time:";
            }
            else if (building.BUILDING_TYPE == 3) //solid storage
            {
                GameObject.Find("OPGridText_" + building.NAME + "_price_editable").GetComponent<Text>().text = building.PRICE.ToString();
                GameObject.Find("OPGridText_" + building.NAME + "_time_editable").GetComponent<Text>().text = building.PROG_AMOUNT + " KG.";
                GameObject.Find("OPGridText_" + building.NAME + "_time").GetComponent<Text>().text = "Capacity:";
            }
            else if (building.BUILDING_TYPE == 4)//liquid storage
            {
                GameObject.Find("OPGridText_" + building.NAME + "_price_editable").GetComponent<Text>().text = building.PRICE.ToString();
                GameObject.Find("OPGridText_" + building.NAME + "_time_editable").GetComponent<Text>().text = building.PROG_AMOUNT + " L.";
                GameObject.Find("OPGridText_" + building.NAME + "_time").GetComponent<Text>().text = "Capacity:";
            }
        }

        //finishing up
        DisabledObjectsGameScene.Instance.gridBuildings.SetActive(false);
        DisabledObjectsGameScene.Instance.BuyMenuPanel.SetActive(false);
    }
}