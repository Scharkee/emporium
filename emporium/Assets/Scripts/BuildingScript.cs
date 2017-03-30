using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SocketIO;
using System.Text.RegularExpressions;
using System;

public class BuildingScript : MonoBehaviour {



    //plant
    public bool TileGrown;

    //building
    public bool WorkDone;
    public bool WorkAssigned;
    public bool ContextOpen;




    //both
    SocketIOComponent socket;
    UIManager uiManager;

    public Tile thistile;
    public Building thistileInfo;
    SocketManager socman;

    public int idInTileDatabase; //norint pasiekti savo tile bendrame tile array
    private int idInTileInfoDatabase;  // norint pasiekti savo tile informacija bendrame BuildingInfo array



    // Use this for initialization
    void Start () {

       
        GameObject managerial = GameObject.Find("_ManagerialScripts");
        socman = managerial.GetComponent<SocketManager>();
        uiManager = managerial.GetComponent<UIManager>();

        GameObject go = GameObject.Find("SocketIO");
        socket = go.GetComponent<SocketIOComponent>();

        CheckForGrowthCompletion();

        AssignTileValues();
        StartCoroutine(CheckForGrowthCompletionRepeat());
        RetrieveTileInfo();
        AssignBuildingSpecificValues();
        TileGrown = false;
        WorkDone = false;
        
        ContextOpen = false;
       

        socket.On("RESET_TILE_GROWTH", ResetGrowth);
        socket.On("ASSIGN_TILE_WORK", AssignTileWork);

        Debug.Log(thistile.NAME+" "+thistileInfo.BUILDING_TYPE);
        

    }
	
	// Update is called once per frame
	void Update () {
      
	}


    IEnumerator CheckForGrowthCompletionRepeat()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            CheckForGrowthCompletion();


            



        }
    }


    void OnMouseDown()
    {

        if (thistileInfo.BUILDING_TYPE == 0) { 
        if (TileGrown)
        {

            Debug.Log("harvesting plant");

            VerifyHarvest();
        }

        }else if(thistileInfo.BUILDING_TYPE == 1)
        {

            if (WorkDone && WorkAssigned)
            {
                Debug.Log("harvesting building");

                VerifyHarvest();
            }else if (!WorkAssigned)
            {  // spaudziama ant neturincio darbo preso, ismesti job assign menu
                Debug.Log("Assign job right now");
                //ismetamaas meniu, todel sitas true iki value returno arba menu close*
                ContextOpen = true;

                ContextManager.StartPressContext(WorkAssigned);

            }
            else if (!WorkDone)
            {  
                // spaudziama ant nebaigusio spausti preso, ismesti context menu su stats
                Debug.Log("Checking stats on press progress");

                ContextManager.StartPressContext(WorkAssigned);

            }
        }




    }



    void CheckForGrowthCompletion()
    {

        if(thistileInfo.BUILDING_TYPE == 0) { // augalas
        int prog = thistile.START_OF_GROWTH + thistileInfo.PROG_AMOUNT;

        if (socman.unix >= prog)
        {

            TileGrown = true;


            if (transform.FindChild(thistile.NAME + "_vaisiai(Clone)"))
            {
               
            }else
            {


                GameObject vaisiaiPrefab = Resources.Load("Plants/done/" + thistile.NAME + "_vaisiai") as GameObject;

                GameObject vaisiai = Instantiate(vaisiaiPrefab, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z), gameObject.transform.rotation, gameObject.transform) as GameObject;

            }
          
      
          }

        }else if (thistileInfo.BUILDING_TYPE == 1) // presas
        {

            int prog = thistile.START_OF_GROWTH + thistileInfo.PROG_AMOUNT*(thistile.BUILDING_CURRENT_WORK_AMOUNT/100);

            Debug.Log(socman.unix+" and "+prog);
            Debug.Log("work assigned = "+WorkAssigned);
            if (socman.unix >= prog && WorkAssigned)
            {

                WorkDone = true; //TODO: maybe change to building type of thing


                if (transform.FindChild(thistile.NAME + "_done(Clone)"))
                {

                }
                else
                {


                    GameObject DonePrefab = Resources.Load("Plants/done/" + thistile.NAME + "_done") as GameObject;

                    GameObject done = Instantiate(DonePrefab, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z), gameObject.transform.rotation, gameObject.transform) as GameObject;

                }


            }


        }



    }

    void AssignTileValues()
    {
        //?????
    }



    void RetrieveTileInfo()
    {
        int i = -1;
        while (thistileInfo.NAME != thistile.NAME)
        {
            i++;
            thistileInfo.NAME = Database.buildinginfo[i].NAME;
            Debug.Log(Database.buildinginfo[i].NAME);


        }

        

        thistileInfo = Database.buildinginfo[i];
        idInTileInfoDatabase = i;

     
    }


    private void VerifyHarvest()
    {
        
        Dictionary<string, string> data;
        data = new Dictionary<string, string>();
   
        data["Uname"] = Database.UserUsername;
        data["TileID"] = thistile.ID.ToString();

        if (thistileInfo.BUILDING_TYPE == 0)//plant
        {
            socket.Emit("VERIFY_COLLECT_TILE", new JSONObject(data));

        }else if (thistileInfo.BUILDING_TYPE == 1)//presas
        {
            socket.Emit("VERIFY_COLLECT_PRESS_WORK", new JSONObject(data));


        }



    }

    private void ResetGrowth(SocketIOEvent evt)
    {

        if(thistileInfo.BUILDING_TYPE == 0)//augalas
        {


        if (int.Parse(Regex.Replace(evt.data.GetField("tileID").ToString(), "[^0-9]", "")) == thistile.ID)
        {

            uiManager.ChangeUIText(thistileInfo.TILEPRODUCENAME + "_Editable", evt.data.GetField("currentProduceAmount").ToString()); //setting text to represent kilo's
            Debug.Log("ressetting tile ");


            Destroy(transform.FindChild(thistile.NAME + "_vaisiai(Clone)").gameObject);
            Debug.Log("destroying fruits: " + thistile.NAME + "_vaisiai(Clone)");


            Database.tile[idInTileDatabase].START_OF_GROWTH = int.Parse(Regex.Replace(evt.data.GetField("unixBuffer").ToString(), "[^0-9]", ""));

            TileGrown = false;


            thistile = Database.tile[idInTileDatabase];



        }
      


        }else if(thistileInfo.BUILDING_TYPE == 1)//presas
        {
            //kaip presas reaguos, kai atsius resetint progresa
            //TODO


            if (int.Parse(Regex.Replace(evt.data.GetField("tileID").ToString(), "[^0-9]", "")) == thistile.ID)
            {
               


                Database.tile[idInTileDatabase].START_OF_GROWTH = int.Parse(Regex.Replace(evt.data.GetField("unixBuffer").ToString(), "[^0-9]", ""));
                Database.tile[idInTileDatabase].BUILDING_CURRENT_WORK_AMOUNT= int.Parse(evt.data.GetField("currentWorkAmmount").ToString());
                Database.tile[idInTileDatabase].WORK_NAME = Regex.Replace(evt.data.GetField("currentWorkName").ToString(), "[^0-9]", "") ;
                thistile = Database.tile[idInTileDatabase];

                Debug.Log("press received reset request ID VERIFIED " + Regex.Replace(thistile.WORK_NAME, "[^0-9]", "") + "_Sultys_Editable");

                uiManager.ChangeUIText(Regex.Replace(thistile.WORK_NAME, "[^0-9]", "") + "_Sultys_Editable", evt.data.GetField("currentProduceAmount").ToString()); //setting text to represent kilos

                Debug.Log(thistile.NAME + "_done(Clone)");

                Destroy(transform.FindChild(thistile.NAME + "_done(Clone)").gameObject);


                Debug.Log("destroying done status: " + thistile.NAME + "_done(Clone)");





                



            }


        }



    }


    private void AssignTileWork(SocketIOEvent evt)
    {




        if (int.Parse(Regex.Replace(evt.data.GetField("tileID").ToString(), "[^0-9]", "")) == thistile.ID)
        {

            Debug.Log("GOT THE CALLBACK ");

         

            Database.tile[idInTileDatabase].START_OF_GROWTH = int.Parse(Regex.Replace(evt.data.GetField("unixBuffer").ToString(), "[^0-9]", ""));
            Database.tile[idInTileDatabase].BUILDING_CURRENT_WORK_AMOUNT = int.Parse(Regex.Replace(evt.data.GetField("currentWorkAmount").ToString(), "[^0-9]", ""));
            Database.tile[idInTileDatabase].WORK_NAME = evt.data.GetField("currentWorkName").ToString();

            thistile = Database.tile[idInTileDatabase];

            Debug.Log("curent work amount = " + Database.tile[idInTileDatabase].BUILDING_CURRENT_WORK_AMOUNT);

            WorkAssigned = true;
            WorkDone = false;


            //start some sort of work effects.
           

            transform.FindChild("PressCube").GetComponent<Renderer>().material.color = new Color(1f,0.1f,0.2f);







            



        }



    }


    void AssignBuildingSpecificValues()
    {

        if (thistileInfo.BUILDING_TYPE==1) // cia pastatas, reikia priskirtii pastatui specifisku parametrus
        {


            if (thistile.WORK_NAME == "nieko"|| thistile.WORK_NAME == "")
            {
                WorkAssigned = false;

            }else
            {
                Debug.Log("woop");
                WorkAssigned = true;
            }


        }

    }




    public void ReceiveWork(PressWorkPKG pkg) //parejo broadcastas, kazkam duota darbo
    {
        if (ContextOpen) //sitoj tile buvo atidarytas context, todel imam info
        {
            Debug.Log("got work");
            ContextOpen = false;
            AskForWork(pkg.workName, pkg.workAmount);

            ContextManager.CloseAndResetPressContext();



        }



    }

    private void AskForWork(string workName, int workAmount)
    {

        Dictionary<string, string> data;
        data = new Dictionary<string, string>();

        data["Uname"] = Database.UserUsername;
        data["TileID"] = thistile.ID.ToString();
        data["WorkName"] = workName;
        data["WorkAmount"] = workAmount.ToString();

     
        if (thistileInfo.BUILDING_TYPE == 1)//last verification
        {
            socket.Emit("TILE_ASSIGN_WORK", new JSONObject(data));

            Debug.Log("sending stuff out");
        }
        else
        {


            Debug.Log("--------------ELUL AUGALAS PRASO DARBO ------------");
        }



    }


}
