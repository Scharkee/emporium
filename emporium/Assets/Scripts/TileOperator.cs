using System.Collections;
using UnityEngine;

public class TileOperator : MonoBehaviour
{
    private SocketManager socman;
    private bool firstLoadCompleted = false;

    private void Awake()
    {
        socman = DisabledObjectsGameScene.Instance.managerialScripts.GetComponent<SocketManager>();
    }

    public void StartGrowthCompletrionCheckRepeat()
    {
        StartCoroutine(CheckForGrowthCompletionRepeat());
    }

    private IEnumerator CheckForGrowthCompletionRepeat()
    {
        CheckForGrowthCompletion(); //pirmas expedited checkas uzkrovimui

        while (true)
        {
            yield return new WaitForSeconds(1f);
            CheckForGrowthCompletion();
        }
    }

    private void CheckForGrowthCompletion()
    {
        Debug.Log("checking growth");
        foreach (GameObject tile in Database.Instance.ActiveTiles)
        {
            try //safety net for transitioning tiles(upgrades in progress and such)
            {
                BuildingScript tileScript = tile.GetComponent<BuildingScript>();

                if (tileScript.thistileInfo.BUILDING_TYPE == 0)
                { // augalas
                    int prog = tile.GetComponent<BuildingScript>().thistile.START_OF_GROWTH + tile.GetComponent<BuildingScript>().thistileInfo.PROG_AMOUNT;

                    if (!tileScript.colliderSet)
                    {
                        tile.GetComponent<BoxCollider>().center = new Vector3(0, transform.lossyScale.y / 2, 0);

                        tile.GetComponent<BoxCollider>().size = new Vector3(1, transform.lossyScale.y, 1);
                        tileScript.colliderSet = true;
                    }

                    if (socman.unix >= prog && !tileScript.justSpawned)
                    {
                        GameObject vaisiaiPrefab = Resources.Load("Plants/done/" + tileScript.thistile.NAME + "_vaisiai") as GameObject;
                        tileScript.TileGrown = true;

                        if (!tile.transform.Find(tileScript.thistile.NAME).Find(tileScript.thistile.NAME + "_vaisiai(Clone)"))
                        {
                            foreach (Transform child in tile.transform)
                            {
                                //yra papildomu medziu ant tile
                                if (child.name == tileScript.thistile.NAME + "(Clone)" || child.name == tileScript.thistile.NAME)
                                {
                                    GameObject vaisiai = Instantiate(vaisiaiPrefab, new Vector3(child.transform.position.x, child.transform.position.y, child.transform.position.z), child.transform.rotation, child.transform) as GameObject;
                                    //   vaisiai.transform.localScale = child.transform.localScale;
                                }
                                else if (child.name != tileScript.thistile.NAME + "(Clone)" && child.name != tileScript.thistile.NAME + "_vaisiai(Clone)")
                                {
                                }
                            }
                        }
                    }
                }
                else if (tileScript.thistileInfo.BUILDING_TYPE == 1) // presas
                {
                    int prog = tileScript.thistile.START_OF_GROWTH + tileScript.thistile.BUILDING_CURRENT_WORK_AMOUNT * (tileScript.thistileInfo.PROG_AMOUNT / 100);

                    if (socman.unix >= prog && tileScript.WorkAssigned && !tileScript.justSpawned)
                    {
                        tileScript.WorkDone = true; //TODO: maybe change to building type of thing

                        if (tile.transform.Find(tileScript.thistile.NAME + "_done(Clone)"))
                        {
                        }
                        else
                        {
                            GameObject DonePrefab = Resources.Load("Plants/done/" + tileScript.thistile.NAME + "_done") as GameObject;
                            GameObject done = Instantiate(DonePrefab, new Vector3(tile.transform.position.x, tile.transform.position.y, tile.transform.position.z), tile.transform.rotation, tile.transform) as GameObject;
                        }
                    }
                }
                else if (tileScript.thistileInfo.BUILDING_TYPE == 2) // transportas
                {
                    //kolkas nieko, transportas tik stovi vietoj
                }
            }
            catch
            {
            }
        }

        if (!firstLoadCompleted)
        {
            firstLoadCompleted = true;
            Destroy(DisabledObjectsGameScene.Instance.LoadingPanel);
            Globals.Instance.cameraBlur.blurSize = 0;
            Globals.Instance.cameraBlur.enabled = false;
        }
    }
}