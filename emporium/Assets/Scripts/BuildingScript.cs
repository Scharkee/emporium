using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SocketIO;
using System.Text.RegularExpressions;
using System.Threading;

public class BuildingScript : MonoBehaviour
{



    //plant
    public bool TileGrown;


    //building
    public bool WorkDone;
    public bool WorkAssigned;
    public bool ContextOpen;

    AudioSource audiosource;

    public AudioClip yeh;
    public AudioClip noh;

    public GameObject SelectionGlowObject;



    //both
    SocketIOComponent socket;
    UIManager uiManager;

    public Tile thistile;
    public Building thistileInfo;
    SocketManager socman;
    public bool justSpawned = true;
    public bool colliderSet = false;
    public int idInTileDatabase; //norint pasiekti savo tile bendrame tile array
    private int idInTileInfoDatabase;  // norint pasiekti savo tile informacija bendrame BuildingInfo array

    public int idInActiveTiles;

    // Use this for initialization
    void Start()
    {



        socman = DisabledObjectsGameScene.Instance.managerialScripts.GetComponent<SocketManager>();
        uiManager = DisabledObjectsGameScene.Instance.managerialScripts.GetComponent<UIManager>();


        socket = DisabledObjectsGameScene.Instance.socket;

        audiosource = GetComponent<AudioSource>();






        AssignBuildingSpecificValues();
        TileGrown = false;
        WorkDone = false;

        ContextOpen = false;


        socket.On("RESET_TILE_GROWTH", ResetGrowth);
        socket.On("ASSIGN_TILE_WORK", AssignTileWork);




    }

    public bool Harvestable()
    {


        if (DisabledObjectsGameScene.Instance.tileSellScript.GetComponent<TileSellScript>().sellModeEnabled || DisabledObjectsGameScene.Instance.BuyMenuPanel.activeSelf || DisabledObjectsGameScene.Instance.alertPanel.activeSelf || DisabledObjectsGameScene.Instance.PressContextPanel.activeSelf || DisabledObjectsGameScene.Instance.SellingPanel.activeSelf)//something is on(sell mode, buy menu, press context panel etc.) Interacting with tile disabled for the time being.
        {


            return false;

        }
        else //nothing stopping user from interacting with the tile
        {
            return true;
        }


    }

    void OnMouseDown()
    {

        if (Harvestable())
        {
            if (thistileInfo.BUILDING_TYPE == 0)
            {
                if (TileGrown)
                {

                    if ((thistileInfo.TILEPRODUCERANGE_1 + thistileInfo.TILEPRODUCERANGE_2) / 2 > Database.Instance.Storage.TotalProduceStorage - Database.Instance.Storage.TakenProduceStorage)
                    {//nera vietos tile produce range VIDURKIUI, buna perkrauta jeigu RNG isrollintu didesni (highlightint raudonai ant HUD)
                        notifyOfProduceAmount("Not enough storage space!");

                    }
                    else
                    {

                        audiosource.clip = yeh;
                        audiosource.Play();

                        Debug.Log("harvesting plant");

                        VerifyHarvest();

                    }

                }
                else
                {
                    audiosource.clip = noh;
                    audiosource.Play();

                }

            }
            else if (thistileInfo.BUILDING_TYPE == 1)
            {

                if (WorkDone && WorkAssigned)
                {
                    Debug.Log("harvesting building");

                    VerifyHarvest();
                }
                else if (!WorkAssigned)
                {  // spaudziama ant neturincio darbo preso, ismesti job assign menu
                    Debug.Log("Assign job right now");
                    //ismetamaas meniu, todel sitas true iki value returno arba menu close*
                    ContextOpen = true;

                    ContextManager.Instance.StartPressContext(WorkAssigned);

                }
                else if (!WorkDone)
                {
                    // spaudziama ant nebaigusio spausti preso, ismesti context menu su stats
                    Debug.Log("Checking stats on press progress");

                    ContextManager.Instance.StartPressContext(WorkAssigned);

                }
            }
            else if (thistileInfo.BUILDING_TYPE == 2) //transporto dalykelis darbo net nera galima sakyt(nebent upgrades)
            {
                //  ContextManager.Instance.StartProduceSellingContext();


            }

        }





    }



    public void RetrieveTileInfo()
    {
        int i = -1;

        while (thistileInfo.NAME != thistile.NAME)
        {

            i++;
            thistileInfo.NAME = Database.Instance.buildinginfo[i].NAME;



        }

        thistileInfo = Database.Instance.buildinginfo[i];
        idInTileInfoDatabase = i;

        justSpawned = false;


        idInActiveTiles = Database.Instance.ActiveTiles.IndexOf(gameObject);

        if (thistileInfo.BUILDING_TYPE == 2) //uzsiregistruojama kaip transportas.
        {//LOCK nenaudojamas, nes DB gali buti tik vienas transportas

            Database.Instance.CurrentVehichle.time = thistileInfo.PROG_AMOUNT;
            Database.Instance.CurrentVehichle.amount = float.Parse(thistileInfo.TILEPRODUCENAME);
            Database.Instance.CurrentVehichle.Name = thistile.NAME;
            Database.Instance.CurrentVehichle.IDinDB = idInTileDatabase;
            Database.Instance.CurrentVehichle.IDinActiveTiles = idInActiveTiles;


            DisabledObjectsGameScene.Instance.CurrentVehicle.currentVehichle.text = IDHelper.Instance.NameToRealName(Database.Instance.CurrentVehichle.Name);

            Debug.Log(Database.Instance.CurrentVehichle.Name);

        }
        else if (thistileInfo.BUILDING_TYPE == 3) //uzsiregistruojama kaip PRODUCE storage
        {

            lock (Database.Instance.ActiveProduceStorage)
            {

                Database.Instance.AddToMaxStorageAmounts(thistileInfo.PROG_AMOUNT,0);
                Database.Instance.ActiveProduceStorage.Add(this);

            }


        }
        else if (thistileInfo.BUILDING_TYPE == 4) //uzsiregistruojama kaip JUICE storage
        {
            lock (Database.Instance.ActiveJuiceStorage)
            {

                Database.Instance.AddToMaxStorageAmounts(thistileInfo.PROG_AMOUNT,1);
                Database.Instance.ActiveJuiceStorage.Add(this);

            }


        }


        //uzregistruojama, kad tile suvede visa reikiama informacija. 
        Interlocked.Increment(ref Database.Instance.TileSelfSignedAssignmentComplete);

    }


    private void VerifyHarvest()
    {

        Dictionary<string, string> data;
        data = new Dictionary<string, string>();

        data["Uname"] = Database.Instance.UserUsername;
        data["TileID"] = thistile.ID.ToString();

        if (thistileInfo.BUILDING_TYPE == 0)//plant
        {

            socket.Emit("VERIFY_COLLECT_TILE", new JSONObject(data));



        }
        else if (thistileInfo.BUILDING_TYPE == 1)//presas
        {
            socket.Emit("VERIFY_COLLECT_PRESS_WORK", new JSONObject(data));


        }

        if (thistileInfo.SINGLE_USE == 1) //vienkartinius destroyinam, taip pat istrinam ir Database.Instance.
        {
            //make call to node
            Database.Instance.ActiveTiles.Remove(gameObject);
            Destroy(gameObject);

        }



    }
    public void activateColliders(bool active)
    {
        GetComponent<BoxCollider>().enabled = active;


    }


    private void ResetGrowth(SocketIOEvent evt)
    {

        if (thistileInfo.BUILDING_TYPE == 0)//augalas
        {


            if (int.Parse(Regex.Replace(evt.data.GetField("tileID").ToString(), "[^0-9]", "")) == thistile.ID)
            {

                Database.Instance.Inventory[thistileInfo.TILEPRODUCENAME] = float.Parse(evt.data.GetField("currentProduceAmount").ToString()); //increasing ammount in inventory

                Database.Instance.AddToStoredAmounts(float.Parse(evt.data.GetField("harvestAmount").ToString()), 0);


                //tik augalai gali tureti multiple buildings per tile. Cia sunaikinami VISI vaisiai.

                foreach (Transform child in transform)
                {

                    if (child.name == thistile.NAME + "(Clone)" || child.name == thistile.NAME)
                    {//cia multi-tile shell modelis

                        foreach (Transform vais in child)
                        {
                            if (vais.name == thistile.NAME + "_vaisiai(Clone)")//vaisiai, destroy.
                            {
                                Destroy(vais.gameObject);

                            }

                        }



                    }

                }

                thistile.START_OF_GROWTH = int.Parse(Regex.Replace(evt.data.GetField("unixBuffer").ToString(), "[^0-9]", ""));


                TileGrown = false;




                notifyOfProduceAmount(evt.data.GetField("harvestAmount").ToString());



            }



        }
        else if (thistileInfo.BUILDING_TYPE == 1)//presas
        {
            //kaip presas reaguos, kai atsius resetint progresa
            //TODO


            if (int.Parse(Regex.Replace(evt.data.GetField("tileID").ToString(), "[^0-9]", "")) == thistile.ID)
            {


                Database.Instance.Inventory[thistile.WORK_NAME + "_sultys"] = float.Parse(evt.data.GetField("currentProduceAmount").ToString()); //increasing ammount in inventory 


                Database.Instance.AddToStoredAmounts(float.Parse(evt.data.GetField("juiceYield").ToString()), 1);


                try
                {
                    Destroy(transform.FindChild(thistile.NAME + "_done(Clone)").gameObject);
                }
                catch
                {

                }

                //TODO: pridet i ingame inventoriu IR sutvarkyt inventory expander (kad disablintu o ne nuimtu alpha iki 0)


                Database.Instance.tile[idInTileDatabase].START_OF_GROWTH = int.Parse(Regex.Replace(evt.data.GetField("unixBuffer").ToString(), "[^0-9]", ""));


                Database.Instance.tile[idInTileDatabase].BUILDING_CURRENT_WORK_AMOUNT = 0;

                Database.Instance.tile[idInTileDatabase].WORK_NAME = "";

                thistile = Database.Instance.tile[idInTileDatabase];

                WorkAssigned = false;
                WorkDone = false;

                transform.FindChild("PressCube").GetComponent<Renderer>().material.color = new Color(0.5f, 0.5f, 0.5f);

            }
        }
    }


    private void AssignTileWork(SocketIOEvent evt)
    {




        if (int.Parse(Regex.Replace(evt.data.GetField("tileID").ToString(), "[^0-9]", "")) == thistile.ID)
        {

            Debug.Log("GOT THE CALLBACK ");



            Database.Instance.tile[idInTileDatabase].START_OF_GROWTH = int.Parse(Regex.Replace(evt.data.GetField("unixBuffer").ToString(), "[^0-9]", ""));
            Database.Instance.tile[idInTileDatabase].BUILDING_CURRENT_WORK_AMOUNT = int.Parse(Regex.Replace(evt.data.GetField("currentWorkAmount").ToString(), "[^0-9]", ""));
            Database.Instance.tile[idInTileDatabase].WORK_NAME = Regex.Replace(evt.data.GetField("currentWorkName").ToString(), "[^a-z]", "");

            thistile = Database.Instance.tile[idInTileDatabase];

            WorkAssigned = true;
            WorkDone = false;

            thistile = Database.Instance.tile[idInTileDatabase];

            //start some sort of work effects.

            transform.FindChild("PressCube").GetComponent<Renderer>().material.color = new Color(1f, 0.1f, 0.2f);




        }



    }


    void AssignBuildingSpecificValues()
    {

        if (thistileInfo.BUILDING_TYPE == 1) // cia pastatas, reikia priskirtii pastatui specifisku parametrus
        {


            if (thistile.WORK_NAME == "nieko" || thistile.WORK_NAME == "")
            {
                WorkAssigned = false;

            }
            else
            {

                WorkAssigned = true;
            }


        }
        else if (thistileInfo.BUILDING_TYPE == 2)
        {

            WorkAssigned = false;
            WorkDone = false;

        }

    }




    public void ReceiveWork(PressWorkPKG pkg) //parejo broadcastas, kazkam duota darbo
    {
        if (ContextOpen) //sitoj tile buvo atidarytas context, todel imam info
        {
            Debug.Log("got work");
            ContextOpen = false;
            AskForWork(pkg.workName, pkg.workAmount);

            ContextManager.Instance.CloseAndResetPressContext();



        }
    }

    private void AskForWork(string workName, int workAmount)
    {

        Dictionary<string, string> data;
        data = new Dictionary<string, string>();

        data["Uname"] = Database.Instance.UserUsername;
        data["TileID"] = thistile.ID.ToString();
        data["WorkName"] = workName;
        data["WorkAmount"] = workAmount.ToString();


        if (thistileInfo.BUILDING_TYPE == 1)//last verification
        {
            socket.Emit("TILE_ASSIGN_WORK", new JSONObject(data));


        }
        else
        {
            Debug.Log("--------------ELUL AUGALAS PRASO DARBO ------------");
            socman.DiscrepancyAction(); //cast discrepancy
        }



    }

    private void notifyOfProduceAmount(string text)
    {

        GameObject alert = Instantiate(Resources.Load("produceAmountText"), new Vector3(thistile.X, 0f, thistile.Z), Quaternion.identity, transform) as GameObject;
        alert.GetComponent<TextMesh>().text = text;






    }


}
