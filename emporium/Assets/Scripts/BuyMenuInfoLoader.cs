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
            if (building.BUILDING_TYPE == 0)
            {
                try
                {
                    GameObject.Find("OPGridText_" + building.NAME + "_price_editable").GetComponent<Text>().text = building.PRICE.ToString();
                    TimeSpan ts = TimeSpan.FromSeconds(building.PROG_AMOUNT);

                    GameObject.Find("OPGridText_" + building.NAME + "_time_editable").GetComponent<Text>().text = string.Format("{0:D2}:{1:D2}:{2:D2}", ts.Hours, ts.Minutes, ts.Seconds);
                }
                catch
                {
                }
            }
            else if (building.BUILDING_TYPE == 1)
            {
                try
                {
                    GameObject.Find("OPGridText_" + building.NAME + "_price_editable").GetComponent<Text>().text = building.PRICE.ToString();
                    GameObject.Find("OPGridText_" + building.NAME + "_time_editable").GetComponent<Text>().text = "Depends on load.";
                }
                catch
                {
                }
            }
            else if (building.BUILDING_TYPE == 2)
            {
                try
                {
                    GameObject.Find("OPGridText_" + building.NAME + "_price_editable").GetComponent<Text>().text = building.PRICE.ToString();
                    GameObject.Find("OPGridText_" + building.NAME + "_time_editable").GetComponent<Text>().text = building.PROG_AMOUNT + " seconds.";
                }
                catch
                {
                }
            }
            else if (building.BUILDING_TYPE == 3) //TODO: fix visa sita bloka, perdaryt buyPanelButtonus kad butu text1 text2 kuriuos galima keisti pagal BUILDING_TYPE
            {
                try
                {
                    GameObject.Find("OPGridText_" + building.NAME + "_price_editable").GetComponent<Text>().text = building.PRICE.ToString();
                    GameObject.Find("OPGridText_" + building.NAME + "_time_editable").GetComponent<Text>().text = building.PROG_AMOUNT + " KG.";
                }
                catch
                {
                }
            }
            else if (building.BUILDING_TYPE == 3)
            {
                try
                {
                    GameObject.Find("OPGridText_" + building.NAME + "_price_editable").GetComponent<Text>().text = building.PRICE.ToString();
                    GameObject.Find("OPGridText_" + building.NAME + "_time_editable").GetComponent<Text>().text = building.PROG_AMOUNT + " L.";
                }
                catch
                {
                }
            }
        }
        DisabledObjectsGameScene.Instance.gridBuildings.SetActive(false);
        DisabledObjectsGameScene.Instance.BuyMenuPanel.SetActive(false);
    }
}