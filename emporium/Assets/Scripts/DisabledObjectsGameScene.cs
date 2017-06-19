using SocketIO;
using UnityEngine;
using UnityEngine.UI;

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
    public GameObject Sellbutton_sounds;
    public GameObject Sellbutton_options;
    public GameObject TabGrids, TabButtons;
    public SocketManager SocketManager;

    public CameraController Camcontroller;
    public TransportOperator TransportOperator;
    public WorkerManager WorkerManager;
    public GameObject Selling_Salepanel;
    public GameObject Inventory_Juice_Panel;
    public GameObject Inventory_Produce_Panel;
    public GameObject EconomyPanel_panel;

    public GameObject BuyMenuBuildingTabBtn, BuyMenuPlantTabBtn;

    public Text ProduceStorageEdit;
    public Text JuicetorageEdit;

    public Text TransportCurrent;
    public Text TransportStatus;
    public Text TransportName;

    public static DisabledObjectsGameScene Instance;

    // Use this for initialization
    private void Start()
    {
        alertPanel.SetActive(false);
        StatsContextPanel.SetActive(false);
        SellingPanel.SetActive(false);
        EconomyPanel.SetActive(false);

        //  StartCoroutine(delayedDisable());
    }

    private void Awake()
    {
        Instance = this;
        Inventory_Fruit_panel.SetActive(false);
        socket = GlobalControl.Instance.gameObject.GetComponent<SocketIOComponent>();
        Selector = GameObject.Find("Selector");
    }
}