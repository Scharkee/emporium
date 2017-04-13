using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;

public class DisabledObjectsGameScene : MonoBehaviour {

    //issaugomi ivairus objektai kuriu reikia kitoms klasems. Islieka references net kai disablinami patys objektai
    public static GameObject PressContextPanel;
    public static GameObject LoadingPanel;
    public static GameObject Inventory_Fruit_panel;
    public static GameObject Tiles;
    public static GameObject BuyMenuPanel;
    public static GameObject Selector;
    public static GameObject alertPanel;
    public static GameObject StatsContextPanel;
    public static GameObject gridPlants;
    public static GameObject gridBuildings;
    public static GameObject managerialScripts;
    public static SocketIOComponent socket;
    public static GameObject moneyEdit;
    public static GameObject tileSellScript;
    public static GameObject PlotSelectors;



    // Use this for initialization
    void Start () {

        alertPanel.SetActive(false);
        StatsContextPanel.SetActive(false);
        //  StartCoroutine(delayedDisable());





    }
    void Awake()
    {
        socket = GameObject.Find("SocketIO").GetComponent<SocketIOComponent>();
        PressContextPanel = GameObject.Find("PressContextPanel");
        LoadingPanel = GameObject.Find("LoadingPanel");
        Inventory_Fruit_panel = GameObject.Find("Inventory_Fruit_panel");
        Tiles= GameObject.Find("Tiles");
        BuyMenuPanel = GameObject.Find("BuyMenuPanel");
        Selector = GameObject.Find("Selector");
        alertPanel = GameObject.Find("alertPanel");
        managerialScripts= GameObject.Find("_ManagerialScripts");
        StatsContextPanel = GameObject.Find("StatsContextPanel");

        gridPlants = GameObject.Find("OptionGrid");
        gridBuildings = GameObject.Find("OptionGridBuildings");
       

        moneyEdit = GameObject.Find("MoneyEdit");
        tileSellScript = GameObject.Find("SellScript");
        PlotSelectors = GameObject.Find("PlotSelectors");


    }

    IEnumerator delayedDisable() //should test if works against bad 
    {

        while (DisabledObjectsGameScene.LoadingPanel)
        {
            yield return new WaitForSeconds(0.1f);
           

        }

        GameObject.Find("InventoryDropDown").GetComponent<CanvasGroup>().alpha = 1f;
        Inventory_Fruit_panel.SetActive(false);
    }



}
