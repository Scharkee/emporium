using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileOperator : MonoBehaviour {

    SocketManager socman;

	// Use this for initialization
	void Start () {

        StartCoroutine(CheckForGrowthCompletionRepeat());
		
	}

    void Awake()
    {


        socman = GameObject.Find("_ManagerialScripts").GetComponent<SocketManager>();
    }
	
	


    IEnumerator CheckForGrowthCompletionRepeat()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            CheckForGrowthCompletion();


        }
    }


    void CheckForGrowthCompletion()
    {

        foreach (GameObject tile in Database.ActiveTiles)
        {
            BuildingScript tileScript = (tile.GetComponent<BuildingScript>());

            if (tileScript.thistileInfo.BUILDING_TYPE == 0)
            { // augalas
                int prog = tileScript.thistile.START_OF_GROWTH + tileScript.thistileInfo.PROG_AMOUNT;

                if (socman.unix >= prog && !tileScript.justSpawned)
                {

                    tileScript.TileGrown = true;


                    if (tile.transform.FindChild(tileScript.thistile.NAME + "_vaisiai(Clone)"))
                    {

                    }
                    else
                    {


                        GameObject vaisiaiPrefab = Resources.Load("Plants/done/" + tileScript.thistile.NAME + "_vaisiai") as GameObject;

                        GameObject vaisiai = Instantiate(vaisiaiPrefab, new Vector3(tile.transform.position.x, tile.transform.position.y, tile.transform.position.z), tile.transform.rotation, tile.transform) as GameObject;

                    }       
                }
            }
            else if (tileScript.thistileInfo.BUILDING_TYPE == 1) // presas
            {

                int prog = tileScript.thistile.START_OF_GROWTH + tileScript.thistile.BUILDING_CURRENT_WORK_AMOUNT * (tileScript.thistileInfo.PROG_AMOUNT / 100);


                if (socman.unix >= prog && tileScript.WorkAssigned && !tileScript.justSpawned)
                {

                    tileScript.WorkDone = true; //TODO: maybe change to building type of thing


                    if (tile.transform.FindChild(tileScript.thistile.NAME + "_done(Clone)"))
                    {

                    }
                    else
                    {

                        GameObject DonePrefab = Resources.Load("Plants/done/" + tileScript.thistile.NAME + "_done") as GameObject;                 
                        GameObject done = Instantiate(DonePrefab, new Vector3(tile.transform.position.x, tile.transform.position.y, tile.transform.position.z), tile.transform.rotation, tile.transform) as GameObject;
                    }
                }
            }
        }

    }
}
