using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
    public GameObject RealGround;
    public GameObject PlaceholderGround;
    public PriceManager pricemanager;
    public GameObject EconomyButton;
    public GameObject EconomyPanel;
    public GameObject CancelPanel;
    public SocketManager SocketManager;
    public CurrentVehicle CurrentVehicle;
    public CameraController Camcontroller;

    public Text ProduceStorageEdit;
    public Text JuicetorageEdit;

    public static DisabledObjectsGameScene Instance;



    // Use this for initialization
    void Start()
    {

        alertPanel.SetActive(false);
        StatsContextPanel.SetActive(false);
        SellingPanel.SetActive(false);
        EconomyPanel.SetActive(false);

        //  StartCoroutine(delayedDisable());





    }
    void Awake()
    {
        Instance = this;
        Inventory_Fruit_panel.SetActive(false);
        socket = GlobalControl.Instance.gameObject.GetComponent<SocketIOComponent>();
        Selector = GameObject.Find("Selector");


    }




}
