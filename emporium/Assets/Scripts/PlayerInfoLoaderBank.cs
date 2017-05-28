using SocketIO;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfoLoaderBank : MonoBehaviour
{
    public string UserUsername;
    public int UserDollars;
    public int UserLastOnline;
    public int UserPlotSize;
    private GlobalControl globalcontr;

    private HelperScripts helperscript;
    private AssignTiles assigner;

    public static PlayerInfoLoaderBank Instance;
    private SocketIOComponent socket;

    // Use this for initialization
    private void Start()
    {
        assigner = gameObject.GetComponent<AssignTiles>();

        socket = DisabledObjectsGameScene.Instance.socket;

        globalcontr = GameObject.Find("GlobalObject").GetComponent<GlobalControl>();

        RetrieveStats();
        socket.On("RETRIEVE_STATS", ReceiveStats);
        socket.On("RECEIVE_TILES", ReceiveTileData);
        socket.On("RECEIVE_TILE_INFORMATION", ReceiveTileInformation);
        socket.On("RECEIVE_INVENTORY", ReceiveInventory);
    }

    private void Awake()
    {
        Instance = this;
    }

    private void LoadEverythingAndSetUI()
    {
        Text moneytext = DisabledObjectsGameScene.Instance.moneyEdit.GetComponent<Text>();
        Text usertext = GameObject.Find("PlayingAsEdit").GetComponent<Text>();

        moneytext.text = Database.Instance.UserDollars.ToString();
        usertext.text = GlobalControl.Instance.Uname;

        CameraController rotscript = Camera.main.GetComponent<CameraController>();
        rotscript.SetCurrentRotCenter(lygusnelygusPlot());//also sets ground transform

        Camera.main.transform.position = new Vector3(0, Database.Instance.UserPlotSize * 0.7f, Database.Instance.UserPlotSize * 1.2f);

        GameObject.Find("Ground").transform.localScale = new Vector3(Database.Instance.UserPlotSize, 1f, Database.Instance.UserPlotSize);
    }

    private bool lygusnelygusPlot()
    {
        bool lygnelyg = true;

        if (Database.Instance.UserPlotSize % 2 == 0) //lyginis plot dimensions
        {
            lygnelyg = true;
        }
        else if (Database.Instance.UserPlotSize % 2 == 1)//NElyginis plot dimensions
        {
            lygnelyg = false;
        }
        return lygnelyg;
    }

    private void GetStats(SocketIOEvent evt)
    {
        Dictionary<string, string> data = new Dictionary<string, string>();
        data["Uname"] = GlobalControl.Instance.Uname;

        if (globalcontr.ConnectedOnceNoDupeStatRequests)
        {
        }
        else
        {
            socket.Emit("GET_STATS", new JSONObject(data));  //get stats
            Debug.Log("asking for stats");
            globalcontr.ConnectedOnceNoDupeStatRequests = bool.Parse(evt.data.GetField("ConnectedOnceNoDupeStatRequests").ToString());
        }

        socket.Emit("CLIENT_DATA", new JSONObject(data));
    }

    private void RetrieveStats()
    {
        Dictionary<string, string> data = new Dictionary<string, string>();
        data["Uname"] = GlobalControl.Instance.Uname;

        if (globalcontr.ConnectedOnceNoDupeStatRequests)
        {
        }
        else
        {
            socket.Emit("GET_STATS", new JSONObject(data));  //get stats
            Debug.Log("asking for stats");
            globalcontr.ConnectedOnceNoDupeStatRequests = true;
        }

        socket.Emit("CLIENT_DATA", new JSONObject(data));
    }

    private void ReceiveStats(SocketIOEvent evt)
    {
        Database.Instance.UserUsername = GlobalControl.Instance.Uname;
        Database.Instance.UserDollars = float.Parse(evt.data.GetField("dollars").ToString());
        Database.Instance.UserPlotSize = int.Parse(evt.data.GetField("plotsize").ToString());

        string lastOnline = Regex.Replace(evt.data.GetField("lastonline").ToString(), "[^0-9]", "");
        Database.Instance.UserLastOnline = int.Parse(lastOnline);

        if (bool.Parse((evt.data.GetField("firstPlay").ToString())))
        {
            DisabledObjectsGameScene.Instance.managerialScripts.GetComponent<TutorialManager>().StartTutorial();
        }

        LoadEverythingAndSetUI();

        Dictionary<string, string> data = new Dictionary<string, string>();
        data["Uname"] = GlobalControl.Instance.Uname;

        socket.Emit("GET_TILE_INFORMATION", new JSONObject(data));
        socket.Emit("GET_TILE_DATA", new JSONObject(data));
        socket.Emit("GET_TRANSPORT_QUEUES", new JSONObject(data));
    }

    private void ReceiveTileData(SocketIOEvent evt)
    {
        assigner.AssignReceivedTiles(evt);
    }

    private void ReceiveTileInformation(SocketIOEvent evt)
    {
        assigner.AssignTileInformation(evt);
    }

    private void ReceiveInventory(SocketIOEvent evt)
    {
        assigner.AssignInventory(evt);
    }
}