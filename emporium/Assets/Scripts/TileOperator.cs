using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.ImageEffects;

public class TileOperator : MonoBehaviour
{

    SocketManager socman;
    private bool firstLoadCompleted = false;

    // Use this for initialization
    void Start()
    {



    }

    void Awake()
    {


        socman = DisabledObjectsGameScene.Instance.managerialScripts.GetComponent<SocketManager>();
    }

    public void StartGrowthCompletrionCheckRepeat()
    {

        StartCoroutine(CheckForGrowthCompletionRepeat());
    }


    IEnumerator CheckForGrowthCompletionRepeat()
    {
        CheckForGrowthCompletion(); //pirmas expedited checkas uzkrovimui

        while (true)
        {
            yield return new WaitForSeconds(1f);
            CheckForGrowthCompletion();


        }
    }


    void CheckForGrowthCompletion()
    {

        foreach (GameObject tile in Database.Instance.ActiveTiles)
        {
            BuildingScript tileScript = tile.GetComponent<BuildingScript>();


            if (tileScript.thistileInfo.BUILDING_TYPE == 0)
            { // augalas
                int prog = tile.GetComponent<BuildingScript>().thistile.START_OF_GROWTH + tile.GetComponent<BuildingScript>().thistileInfo.PROG_AMOUNT;

                if (socman.unix >= prog && !tileScript.justSpawned)
                {


                    tileScript.TileGrown = true;


                    if (!tile.transform.FindChild(tileScript.thistile.NAME + "_vaisiai(Clone)"))
                    {
                        Vector3 mainTreeScale = new Vector3();

                        GameObject vaisiaiPrefab = Resources.Load("Plants/done/" + tileScript.thistile.NAME + "_vaisiai") as GameObject;

                        foreach (Transform child in tile.transform)
                        {
                            //yra papildomu medziu ant tile
                            if (child.name == tileScript.thistile.NAME + "(Clone)")
                            {

                                GameObject vaisiai = Instantiate(vaisiaiPrefab, new Vector3(child.transform.position.x, child.transform.position.y, child.transform.position.z), child.transform.rotation, child.transform) as GameObject;
                                //   vaisiai.transform.localScale = child.transform.localScale;

                            }
                            else if (child.name != tileScript.thistile.NAME + "(Clone)" && child.name != tileScript.thistile.NAME + "_vaisiai(Clone)")
                            {
                                Debug.Log("aptiktas medzio modelis");
                                mainTreeScale = child.transform.localScale;

                            }


                        }

                        //one final instantiate for main tree's fruits
                        GameObject mainVaisiai = Instantiate(vaisiaiPrefab, new Vector3(tile.transform.position.x, tile.transform.position.y, tile.transform.position.z), tile.transform.rotation, tile.transform) as GameObject;
                        //paskutinis scale pagrindinio medzio vaisiams
                        mainVaisiai.transform.localScale = mainTreeScale;

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



        if (!firstLoadCompleted)
        {

            firstLoadCompleted = true;
            GameObject loadpanel = GameObject.Find("LoadingPanel");
            Destroy(loadpanel);
            Globals.Instance.cameraBlur.blurSize = 0;
            Globals.Instance.cameraBlur.enabled = false;

        }







    }
}
