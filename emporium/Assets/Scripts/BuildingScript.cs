using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingScript : MonoBehaviour {

    public Tile thistile;
    public Building thistileInfo;
    SocketManager socman;
    public bool TileGrown;





    // Use this for initialization
    void Start () {
        socman = GameObject.Find("_ManagerialScripts").GetComponent<SocketManager>();

        AssignTileValues();
        StartCoroutine(CheckForGrowthCompletionRepeat());
        RetrieveTileInfo();

        TileGrown = false;
        

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

            socman.VerifyHarvest(thistile.ID);
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
        else
        {

            Debug.Log("medis neuzauges, " +socman.unix +" yra daugiau uz " + prog);

        }


    }

    void AssignTileValues()
    {

    }



    void RetrieveTileInfo()
    {
        int i = -1;
        while (thistileInfo.NAME != thistile.NAME)
        {
            i++;
            thistileInfo.NAME = Database.buildinginfo[i].NAME;


            Debug.Log(Database.buildinginfo[i].NAME);
            Debug.Log(thistileInfo.NAME);
            
        }

        thistileInfo = Database.buildinginfo[i];

        Debug.Log("final tile progress from thistileinfo is: " + thistileInfo.PROG_AMOUNT);



    }
}
