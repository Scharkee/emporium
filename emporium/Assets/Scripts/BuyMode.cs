﻿using UnityEngine;
using UnityEngine.UI;

public class BuyMode : MonoBehaviour
{
    private string buildingName;
    private string TileName;
    private bool darken;
    private bool disableQueued;
    private int camspeed = 10;
    private RaycastHit hit;

    private BuyButtonScript buybuttonscript;

    // Use this for initialization
    private void Start()
    {
        buybuttonscript = DisabledObjectsGameScene.Instance.BuyButton.GetComponent<BuyButtonScript>();

        buybuttonscript.panelEnabled = false;
        disableQueued = false;
    }

    private void OnEnable()
    {
        disableQueued = false;
    }

    private void Awake()
    {
    }

    private void Update()
    {
        if (DisabledObjectsGameScene.Instance.PlotSelectors.activeSelf)
        {
            RaycastHit oldHit = hit;

            try
            {
                if (hit.transform.gameObject.GetComponent<PlotSelectorScript>().currentMat == 2)
                {
                    oldHit.transform.gameObject.GetComponent<Renderer>().material = Globals.Instance.plotselector_standard;
                    hit.transform.gameObject.GetComponent<PlotSelectorScript>().currentMat = 1;
                }
                else if (hit.transform.gameObject.GetComponent<PlotSelectorScript>().currentMat == 4)
                {
                    oldHit.transform.gameObject.GetComponent<Renderer>().material = Globals.Instance.plotselector_upgradeable;
                    hit.transform.gameObject.GetComponent<PlotSelectorScript>().currentMat = 3;
                }
            }
            catch
            {
            }

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.tag == "Selector")
                {
                    if (hit.transform.gameObject.GetComponent<PlotSelectorScript>().currentMat == 1)
                    {
                        hit.transform.gameObject.GetComponent<Renderer>().material = Globals.Instance.plotselector_standard_mouseover;
                        hit.transform.gameObject.GetComponent<PlotSelectorScript>().currentMat = 2;
                    }
                    else if (hit.transform.gameObject.GetComponent<PlotSelectorScript>().currentMat == 3)
                    {
                        hit.transform.gameObject.GetComponent<Renderer>().material = Globals.Instance.plotselector_upgradeable_mouseover;
                        hit.transform.gameObject.GetComponent<PlotSelectorScript>().currentMat = 4;
                    }

                    if (Input.GetMouseButtonDown(0))
                    {
                        EnableGroundCol(true);
                        float X = hit.transform.localPosition.x;
                        float Z = hit.transform.localPosition.z;

                        DisabledObjectsGameScene.Instance.PlotSelectors.SetActive(false);

                        DisabledObjectsGameScene.Instance.Camcontroller.PerformCamElevetion();
                        buybuttonscript.panelEnabled = false;

                        //if multi-buy TODO

                        DisabledObjectsGameScene.Instance.Tiles.BroadcastMessage("activateColliders", true);

                        DisabledObjectsGameScene.Instance.BuyMenuPanel.SetActive(false);

                        GameObject.Find("BuyScript").GetComponent<BuyScript>().ChoosePlot(buildingName, X, Z);
                        DisabledObjectsGameScene.Instance.BuyButton.GetComponent<Image>().color = Globals.Instance.buttonColor1;
                        Globals.Instance.cameraBlur.blurSize = 0;
                    }
                }
            }
        }
    }

    public void receiveName(string name)
    {
        buildingName = name;

        buybuttonscript.panelEnabled = false;

        Globals.Instance.cameraBlur.enabled = false;
        //effects and pltoselectors

        DisabledObjectsGameScene.Instance.Tiles.BroadcastMessage("activateColliders", false);
        DisabledObjectsGameScene.Instance.Camcontroller.PerformCamElevetion();

        DisabledObjectsGameScene.Instance.PlotSelectors.SetActive(true);

        EnableGroundCol(false);

        adaptPlotSelectors(name);
    }

    public void EnableGroundCol(bool enabled)
    {
        DisabledObjectsGameScene.Instance.RealGround.GetComponent<BoxCollider>().enabled = enabled;
    }

    public void DisableBuyMode(bool MenupanelActive)
    {
        if (DisabledObjectsGameScene.Instance.PlotSelectors.activeSelf) //compatibility with sell mode & protection against crashing out
        {
            DisabledObjectsGameScene.Instance.PlotSelectors.SetActive(false);
        }

        if (Globals.Instance.cameraUp)
        {
            DisabledObjectsGameScene.Instance.Camcontroller.PerformCamElevetion();
        }

        // buybuttonscript.panelEnabled = false;

        DisabledObjectsGameScene.Instance.BuyMenuPanel.SetActive(MenupanelActive);

        try
        {
            DisabledObjectsGameScene.Instance.Tiles.BroadcastMessage("activateColliders", true);
        }
        catch
        {
            Debug.Log("no tiles to be re-enabled");
        }
        EnableGroundCol(true);

        disableQueued = true;
    }

    public void CancelContext()
    {
        DisableBuyMode(false);
    }

    private void adaptPlotSelectors(string name)
    { //highlighter for upgradeable tiles + grey out for unavailable ones
        foreach (Transform selector in DisabledObjectsGameScene.Instance.PlotSelectors.transform)
        {
            //setting defaults for each selector before applying conditional modifications
            selector.GetComponent<BoxCollider>().enabled = true; //reeabling if it was disabled by last buymode
            selector.GetComponent<Renderer>().material = Globals.Instance.plotselector_standard;

            foreach (GameObject tile in Database.Instance.ActiveTiles)
            {
                if (tile.transform.position.x == selector.position.x && tile.transform.position.z == selector.position.z)//selector is overlaping a tile
                {
                    if (tile.GetComponent<BuildingScript>().thistileInfo.BUILDING_TYPE == 1 || tile.GetComponent<BuildingScript>().thistileInfo.BUILDING_TYPE == 2)
                    {//visi neupgradinami pastatai
                        selector.GetComponent<Renderer>().material = Globals.Instance.plotselector_unavailable;
                        selector.GetComponent<BoxCollider>().enabled = false;
                    }
                    else
                    {//visi upgradiname pastatai
                        if (tile.GetComponent<BuildingScript>().thistile.NAME == name && tile.GetComponent<BuildingScript>().thistile.COUNT >= 5)//max upgraded SAME tile. Mark unavailable.
                        {
                            selector.GetComponent<Renderer>().material = Globals.Instance.plotselector_unavailable;
                            selector.GetComponent<BoxCollider>().enabled = false;
                        }
                        else if (tile.GetComponent<BuildingScript>().thistile.NAME == name)//same tile, mark for upgrading
                        {
                            selector.GetComponent<Renderer>().material = Globals.Instance.plotselector_upgradeable;

                            selector.GetComponent<PlotSelectorScript>().currentMat = 3; //upgrade mat code
                        }
                        else //not the same type of tile, grey out and disable collider
                        {
                            selector.GetComponent<Renderer>().material = Globals.Instance.plotselector_unavailable;
                            selector.GetComponent<BoxCollider>().enabled = false;
                        }
                    }
                }
            }
        }
    }
}