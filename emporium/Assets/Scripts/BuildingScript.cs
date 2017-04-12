﻿using System.Collections;
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

    AudioSource audiosource;

    public AudioClip yeh;
    public AudioClip noh;



    //both
    SocketIOComponent socket;
    UIManager uiManager;

    public Tile thistile;
    public Building thistileInfo;
    SocketManager socman;
    public bool justSpawned;

    public int idInTileDatabase; //norint pasiekti savo tile bendrame tile array
    private int idInTileInfoDatabase;  // norint pasiekti savo tile informacija bendrame BuildingInfo array



    // Use this for initialization
    void Start () {
        justSpawned = true;

        GameObject managerial = GameObject.Find("_ManagerialScripts");
        socman = managerial.GetComponent<SocketManager>();
        uiManager = managerial.GetComponent<UIManager>();

        GameObject go = GameObject.Find("SocketIO");
        socket = go.GetComponent<SocketIOComponent>();

        audiosource = GetComponent<AudioSource>();




        RetrieveTileInfo();
  
        AssignBuildingSpecificValues();
        TileGrown = false;
        WorkDone = false;
        
        ContextOpen = false;
       

        socket.On("RESET_TILE_GROWTH", ResetGrowth);
        socket.On("ASSIGN_TILE_WORK", AssignTileWork);


        

    }
	





    void OnMouseDown()
    {

        if (thistileInfo.BUILDING_TYPE == 0) { 
        if (TileGrown)
        {
                audiosource.clip = yeh;
                audiosource.Play();

            Debug.Log("harvesting plant");

            VerifyHarvest();
            }else
            {
                audiosource.clip = noh;
                audiosource.Play();

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
        }else if (thistileInfo.BUILDING_TYPE == 2) //pardavimu dalykelis darbo net nera galima sakyt(nebent upgrades)
        {
            ContextManager.StartProduceSellingContext();


        }




    }



   




    void RetrieveTileInfo()
    {
        int i = -1;
        while (thistileInfo.NAME != thistile.NAME)
        {
            i++;
            thistileInfo.NAME = Database.buildinginfo[i].NAME;



        }

        

        thistileInfo = Database.buildinginfo[i];
        idInTileInfoDatabase = i;

        justSpawned = false;

     
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

        if (thistileInfo.SINGLE_USE == 1) //vienkartinius destroyinam, taip pat istrinti ir database.
        {

            Database.ActiveTiles.Remove(gameObject);
            Destroy(gameObject);
           
        }



    }

    private void ResetGrowth(SocketIOEvent evt)
    {

        if(thistileInfo.BUILDING_TYPE == 0)//augalas
        {


        if (int.Parse(Regex.Replace(evt.data.GetField("tileID").ToString(), "[^0-9]", "")) == thistile.ID)
        {
               
           Database.Inventory[thistileInfo.TILEPRODUCENAME]=float.Parse(evt.data.GetField("currentProduceAmount").ToString()); //increasing ammount in inventory

          
                Destroy(transform.FindChild(thistile.NAME + "_vaisiai(Clone)").gameObject);



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
                Debug.Log(thistile.WORK_NAME);
                Debug.Log("this just in");


    
                Database.Inventory[thistileInfo.TILEPRODUCENAME+"_sultys"]=float.Parse(evt.data.GetField("currentProduceAmount").ToString()); //increasing ammount in inventory 
             
                

                Destroy(transform.FindChild(thistile.NAME + "_done(Clone)").gameObject);

                //TODO: pridet i ingame inventoriu IR sutvarkyt inventory expander (kad disablintu o ne nuimtu alpha iki 0)


                Debug.Log("destroying done status: " + thistile.NAME + "_done(Clone)");


                Database.tile[idInTileDatabase].START_OF_GROWTH = int.Parse(Regex.Replace(evt.data.GetField("unixBuffer").ToString(), "[^0-9]", ""));
                

                Database.tile[idInTileDatabase].BUILDING_CURRENT_WORK_AMOUNT = 0;

                Database.tile[idInTileDatabase].WORK_NAME = "";
              
                thistile = Database.tile[idInTileDatabase];

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

         

            Database.tile[idInTileDatabase].START_OF_GROWTH = int.Parse(Regex.Replace(evt.data.GetField("unixBuffer").ToString(), "[^0-9]", ""));
            Database.tile[idInTileDatabase].BUILDING_CURRENT_WORK_AMOUNT = int.Parse(Regex.Replace(evt.data.GetField("currentWorkAmount").ToString(), "[^0-9]", ""));
            Database.tile[idInTileDatabase].WORK_NAME = Regex.Replace(evt.data.GetField("currentWorkName").ToString(), "[^a-z]", "");

            thistile = Database.tile[idInTileDatabase];

            Debug.Log("curent work amount = " + Database.tile[idInTileDatabase].BUILDING_CURRENT_WORK_AMOUNT);
            Debug.Log("workname is " + Database.tile[idInTileDatabase].WORK_NAME+" and "+thistile.WORK_NAME);
            WorkAssigned = true;
            WorkDone = false;

            thistile = Database.tile[idInTileDatabase];

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
