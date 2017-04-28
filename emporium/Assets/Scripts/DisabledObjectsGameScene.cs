using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;

public class DisabledObjectsGameScene : MonoBehaviour
{

    //issaugomi ivairus objektai kuriu reikia kitoms klasems. Islieka references net kai disablinami patys objektai
    public GameObject PressContextPanel;
    public GameObject LoadingPanel;
    public GameObject Inventory_Fruit_panel;
    public GameObject Tiles;
    public GameObject BuyMenuPanel;
    public GameObject Selector;
    public GameObject alertPanel;
    public GameObject StatsContextPanel;
    public GameObject gridPlants;
    public GameObject gridBuildings;
    public GameObject managerialScripts;
    public SocketIOComponent socket;
    public GameObject moneyEdit;
    public GameObject tileSellScript;
    public GameObject PlotSelectors;
    public GameObject SellButton;
    public GameObject BuyButton;
    public GameObject BuyMode;
    public GameObject SellingPanel;



    public static DisabledObjectsGameScene Instance;



    // Use this for initialization
    void Start()
    {

        alertPanel.SetActive(false);
        StatsContextPanel.SetActive(false);
        SellingPanel.SetActive(false);
        //  StartCoroutine(delayedDisable());





    }
    void Awake()
    {
        Instance = this;
        Inventory_Fruit_panel = GameObject.Find("Inventory_Fruit_panel");
        Inventory_Fruit_panel.SetActive(false);
        socket = GameObject.Find("SocketIO").GetComponent<SocketIOComponent>();
        PressContextPanel = GameObject.Find("PressContextPanel");
        LoadingPanel = GameObject.Find("LoadingPanel");
        SellingPanel = GameObject.Find("SellingContextPanel");
        Tiles = GameObject.Find("Tiles");
        BuyMenuPanel = GameObject.Find("BuyMenuPanel");
        Selector = GameObject.Find("Selector");
        alertPanel = GameObject.Find("alertPanel");
        managerialScripts = GameObject.Find("_ManagerialScripts");
        StatsContextPanel = GameObject.Find("StatsContextPanel");

        gridPlants = GameObject.Find("OptionGrid");
        gridBuildings = GameObject.Find("OptionGridBuildings");
        BuyMode = GameObject.Find("BuyMode_Selector");


        moneyEdit = GameObject.Find("MoneyEdit");
        tileSellScript = GameObject.Find("SellScript");
        PlotSelectors = GameObject.Find("PlotSelectors");

        BuyButton = GameObject.Find("BuyButton");
        SellButton = GameObject.Find("SellTileButton");
    }




}
