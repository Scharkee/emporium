using System;
using UnityEngine;
using UnityEngine.UI;

public class BuyMenuInfoLoader : MonoBehaviour
{
    public static BuyMenuInfoLoader Instance;
    private GameObject gridButtonPrefab, currentGridButton;

    // Use this for initialization
    private void Start()
    {
        gridButtonPrefab = Resources.Load("UI/B1") as GameObject;
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
                TimeSpan ts = TimeSpan.FromSeconds(building.PROG_AMOUNT);

                currentGridButton = Instantiate(gridButtonPrefab, DisabledObjectsGameScene.Instance.gridPlants.transform) as GameObject;
                currentGridButton.name = building.NAME;
                currentGridButton.GetComponent<UniversalBank>().produceName = building.TILEPRODUCENAME;
                currentGridButton.GetComponent<UniversalBank>().PurchaseName = building.NAME;

                //cia asigninam tile name

                currentGridButton.transform.Find("B1_name_block/B1_name_block_e").GetComponent<Text>().text = GlobalControl.Instance.currentLangDict[building.NAME];

                //cia asigninama informacija apie tile
                currentGridButton.transform.Find("B1_info_block/B1_info_block_item_1/B1_block1_item_1_e").GetComponent<Text>().text = "";
                currentGridButton.transform.Find("B1_info_block/B1_info_block_item_2/B1_block1_item_2_e").GetComponent<Text>().text = GlobalControl.Instance.currentLangDict["price"] + " : " + building.PRICE + "$";
                currentGridButton.transform.Find("B1_info_block/B1_info_block_item_3/B1_block1_item_3_e").GetComponent<Text>().text = GlobalControl.Instance.currentLangDict["growth_time"] + " : " + string.Format("{0:D2}:{1:D2}:{2:D2}", ts.Hours, ts.Minutes, ts.Seconds);
                currentGridButton.transform.Find("B1_info_block/B1_info_block_item_4/B1_block1_item_4_e").GetComponent<Text>().text = GlobalControl.Instance.currentLangDict["yield"] + " : " + building.TILEPRODUCERANDOM1 + "-" + building.TILEPRODUCERANDOM2 + " KG";
                currentGridButton.transform.Find("B1_info_block/B1_info_block_item_5/B1_block1_item_5_e").GetComponent<Text>().text = "";

                //paspaudimo delegatas
                currentGridButton.GetComponent<Button>().onClick.AddListener(delegate () { currentGridButton.GetComponent<UniversalBank>().BuyWithTileName(building.NAME); });
            }
            else if (building.BUILDING_TYPE == 1) //presas
            {
                currentGridButton = (Instantiate(gridButtonPrefab, DisabledObjectsGameScene.Instance.gridBuildings.transform) as GameObject).gameObject;

                currentGridButton.GetComponent<UniversalBank>().produceName = building.TILEPRODUCENAME;
                currentGridButton.GetComponent<UniversalBank>().PurchaseName = building.NAME;

                //cia asigninam tile name
                currentGridButton.transform.Find("B1_name_block/B1_name_block_e").GetComponent<Text>().text = GlobalControl.Instance.currentLangDict[building.NAME];

                //cia asigninama informacija apie tile
                currentGridButton.transform.Find("B1_info_block/B1_info_block_item_1/B1_block1_item_1_e").GetComponent<Text>().text = "";
                currentGridButton.transform.Find("B1_info_block/B1_info_block_item_2/B1_block1_item_2_e").GetComponent<Text>().text = GlobalControl.Instance.currentLangDict["price"] + " : " + building.PRICE.ToString() + "$";
                currentGridButton.transform.Find("B1_info_block/B1_info_block_item_3/B1_block1_item_3_e").GetComponent<Text>().text = GlobalControl.Instance.currentLangDict["speed"] + " : " + "1 KG/s.";
                currentGridButton.transform.Find("B1_info_block/B1_info_block_item_4/B1_block1_item_4_e").GetComponent<Text>().text = GlobalControl.Instance.currentLangDict["efic"] + " : " + building.EFIC * 100 + "%";
                currentGridButton.transform.Find("B1_info_block/B1_info_block_item_5/B1_block1_item_5_e").GetComponent<Text>().text = "";

                //paspaudimo delegatas
                currentGridButton.GetComponent<Button>().onClick.AddListener(delegate () { currentGridButton.GetComponent<UniversalBank>().BuyWithTileName(building.NAME); });
            }
            else if (building.BUILDING_TYPE == 2) //transporto priemone
            {
                currentGridButton = (Instantiate(gridButtonPrefab, DisabledObjectsGameScene.Instance.gridBuildings.transform) as GameObject).gameObject;

                currentGridButton.GetComponent<UniversalBank>().PurchaseName = building.NAME;
                //cia asigninam tile name
                currentGridButton.transform.Find("B1_name_block/B1_name_block_e").GetComponent<Text>().text = GlobalControl.Instance.currentLangDict[building.NAME];

                //cia asigninama informacija apie tile
                currentGridButton.transform.Find("B1_info_block/B1_info_block_item_1/B1_block1_item_1_e").GetComponent<Text>().text = "";
                currentGridButton.transform.Find("B1_info_block/B1_info_block_item_2/B1_block1_item_2_e").GetComponent<Text>().text = GlobalControl.Instance.currentLangDict["price"] + " : " + building.PRICE.ToString() + "$";
                currentGridButton.transform.Find("B1_info_block/B1_info_block_item_3/B1_block1_item_3_e").GetComponent<Text>().text = GlobalControl.Instance.currentLangDict["job_time"] + " : " + building.PROG_AMOUNT + " s/job.";
                currentGridButton.transform.Find("B1_info_block/B1_info_block_item_4/B1_block1_item_4_e").GetComponent<Text>().text = GlobalControl.Instance.currentLangDict["capacity"] + " : " + building.TILEPRODUCENAME + " KG";
                currentGridButton.transform.Find("B1_info_block/B1_info_block_item_5/B1_block1_item_5_e").GetComponent<Text>().text = "";

                //paspaudimo delegatas
                currentGridButton.GetComponent<Button>().onClick.AddListener(delegate () { currentGridButton.GetComponent<UniversalBank>().BuyWithTileName(building.NAME); });
            }
            else if (building.BUILDING_TYPE == 3) //solid storage
            {
                currentGridButton = (Instantiate(gridButtonPrefab, DisabledObjectsGameScene.Instance.gridBuildings.transform) as GameObject).gameObject;

                currentGridButton.GetComponent<UniversalBank>().PurchaseName = building.NAME;

                //cia asigninam tile name
                currentGridButton.transform.Find("B1_name_block/B1_name_block_e").GetComponent<Text>().text = GlobalControl.Instance.currentLangDict[building.NAME];

                //cia asigninama informacija apie tile
                currentGridButton.transform.Find("B1_info_block/B1_info_block_item_1/B1_block1_item_1_e").GetComponent<Text>().text = "";
                currentGridButton.transform.Find("B1_info_block/B1_info_block_item_2/B1_block1_item_2_e").GetComponent<Text>().text = GlobalControl.Instance.currentLangDict["price"] + " : " + building.PRICE.ToString() + "$";
                currentGridButton.transform.Find("B1_info_block/B1_info_block_item_3/B1_block1_item_3_e").GetComponent<Text>().text = GlobalControl.Instance.currentLangDict["capacity"] + " : " + building.PROG_AMOUNT + " KG";
                currentGridButton.transform.Find("B1_info_block/B1_info_block_item_4/B1_block1_item_4_e").GetComponent<Text>().text = "";
                currentGridButton.transform.Find("B1_info_block/B1_info_block_item_5/B1_block1_item_5_e").GetComponent<Text>().text = "";

                //paspaudimo delegatas
                currentGridButton.GetComponent<Button>().onClick.AddListener(delegate () { currentGridButton.GetComponent<UniversalBank>().BuyWithTileName(building.NAME); });
            }
            else if (building.BUILDING_TYPE == 4)//liquid storage
            {
                currentGridButton = (Instantiate(gridButtonPrefab, DisabledObjectsGameScene.Instance.gridBuildings.transform) as GameObject).gameObject;

                currentGridButton.GetComponent<UniversalBank>().PurchaseName = building.NAME;

                //cia asigninam tile name
                currentGridButton.transform.Find("B1_name_block/B1_name_block_e").GetComponent<Text>().text = GlobalControl.Instance.currentLangDict[building.NAME];

                //cia asigninama informacija apie tile
                currentGridButton.transform.Find("B1_info_block/B1_info_block_item_1/B1_block1_item_1_e").GetComponent<Text>().text = "";
                currentGridButton.transform.Find("B1_info_block/B1_info_block_item_2/B1_block1_item_2_e").GetComponent<Text>().text = GlobalControl.Instance.currentLangDict["price"] + " : " + building.PRICE.ToString() + "$";
                currentGridButton.transform.Find("B1_info_block/B1_info_block_item_3/B1_block1_item_3_e").GetComponent<Text>().text = GlobalControl.Instance.currentLangDict["capacity"] + " : " + building.PROG_AMOUNT + " L";
                currentGridButton.transform.Find("B1_info_block/B1_info_block_item_4/B1_block1_item_4_e").GetComponent<Text>().text = "";
                currentGridButton.transform.Find("B1_info_block/B1_info_block_item_5/B1_block1_item_5_e").GetComponent<Text>().text = "";

                //paspaudimo delegatas
                currentGridButton.GetComponent<Button>().onClick.AddListener(delegate () { currentGridButton.GetComponent<UniversalBank>().BuyWithTileName(building.NAME); });
            }
        }

        //finishing up
        DisabledObjectsGameScene.Instance.gridBuildings.SetActive(false);
        DisabledObjectsGameScene.Instance.BuyMenuPanel.SetActive(false);
    }
}