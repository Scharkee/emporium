using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyMode : MonoBehaviour
{


    string buildingName;
    string TileName;
    bool darken;
    bool disableQueued;
    int camspeed = 10;



    BuyButtonScript buybuttonscript;

    // Use this for initialization
    void Start()
    {
        buybuttonscript = GameObject.Find("BuyButton").GetComponent<BuyButtonScript>();

        buybuttonscript.panelEnabled = false;
        disableQueued = false;

    }
    void OnEnable()
    {
        disableQueued = false;
    }

    void Awake()
    {


        buybuttonscript = GameObject.Find("BuyButton").GetComponent<BuyButtonScript>();
    }

    // Update is called once per frame
    void Update()
    {

        if (DisabledObjectsGameScene.Instance.PlotSelectors.activeSelf)
        {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.tag == "Selector")
                {

                    if (Input.GetMouseButtonDown(0))
                    {


                        float X = hit.transform.localPosition.x;
                        float Z = hit.transform.localPosition.z;

                        GameObject.Find("PlotSelectors").SetActive(false);

                        StartCoroutine(liftcamera());
                        buybuttonscript.panelEnabled = false;

                        //if multi-buy TODO

                        GameObject.Find("Tiles").BroadcastMessage("activateColliders", true);
                        RenderSettings.skybox = Globals.Instance.light_skybox; //MAKEME make fade     //FIXME fix material of normal skybox

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

        GameObject.Find("Tiles").BroadcastMessage("activateColliders", false);
        StartCoroutine(liftcamera());
        RenderSettings.skybox = Globals.Instance.dark_skybox;
        GameObject.Find("_GameScripts").GetComponent<PlotSelector>().plotselectors.SetActive(true);

        adaptPlotSelectors(name);

    }



    IEnumerator liftcamera()
    {


        float step = camspeed * Time.deltaTime;


        if (!Globals.Instance.cameraUp)  //raise cam
        {
            Globals.Instance.cameraUp = true;
            while (Camera.main.transform.position.y < 3.2f)
            {
                yield return new WaitForSeconds(0.001f);


                Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, new Vector3(Camera.main.transform.position.x, 3.28f, -1.94f), step);

            }


            yield break;


        }
        else if (Globals.Instance.cameraUp)  //lower cam
        {
            Globals.Instance.cameraUp = false;
            while (Camera.main.transform.position.y > 1.7f)
            {
                yield return new WaitForSeconds(0.0001f);


                Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, new Vector3(Camera.main.transform.position.x, 1.63f, -3.8f), step);
            }



            yield break;
        }

        if (disableQueued)
        {

            gameObject.GetComponent<BuyMode>().enabled = false;
        }

    }

    public void DisableBuyMode(bool MenupanelActive)
    {
        if (DisabledObjectsGameScene.Instance.PlotSelectors.activeSelf) //compatibility with sell mode & protection against crashing out
        {
            DisabledObjectsGameScene.Instance.PlotSelectors.SetActive(false);

        }

        if (Globals.Instance.cameraUp)
        {
            StartCoroutine(liftcamera());
        }

        // buybuttonscript.panelEnabled = false;

        RenderSettings.skybox = Globals.Instance.light_skybox; //MAKEME make fade    

        DisabledObjectsGameScene.Instance.BuyMenuPanel.SetActive(MenupanelActive);

        GameObject.Find("Tiles").BroadcastMessage("activateColliders", true);

        disableQueued = true;

    }

    private void adaptPlotSelectors(string name)
    {

        foreach (Transform selector in DisabledObjectsGameScene.Instance.PlotSelectors.transform)
        {
            //setting defaults for each selector before applying conditional modifications
            selector.GetComponent<BoxCollider>().enabled = true; //reeabling if it was disabled by last buymode
            selector.GetComponent<Renderer>().material = Globals.Instance.plotselector_standard;


            foreach (GameObject tile in Database.Instance.ActiveTiles)
            {

                if (tile.transform.position.x == selector.position.x && tile.transform.position.z == selector.position.z)//selector is overlaping a tile
                {
                    if (tile.GetComponent<BuildingScript>().thistile.NAME == name)//same tile, mark for upgrading
                    {
                        selector.GetComponent<Renderer>().material = Globals.Instance.plotselector_upgradeable;

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
