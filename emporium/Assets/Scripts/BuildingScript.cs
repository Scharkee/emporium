using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SocketIO;
using System.Text.RegularExpressions;
using System;

public class BuildingScript : MonoBehaviour {

    public Tile thistile;
    public Building thistileInfo;
    SocketManager socman;
    public bool TileGrown;

    SocketIOComponent socket;
    UIManager uiManager;

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

        TileGrown = false;

       

        socket.On("RESET_TILE_GROWTH", ResetGrowth);
        

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
        if (TileGrown)
        {

            Debug.Log("harvesting");

            VerifyHarvest();
        }




    }



    void CheckForGrowthCompletion()
    {
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


        socket.Emit("VERIFY_COLLECT_TILE", new JSONObject(data));
    }

    private void ResetGrowth(SocketIOEvent evt)
    {

        Debug.Log(thistile.NAME + " " + thistile.ID + " vs " + int.Parse(Regex.Replace(evt.data.GetField("tileID").ToString(), "[^0-9]", "")));
      

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
        else
        {
            Debug.Log("fucked uuuuuuuuup");
        }




    }
}
