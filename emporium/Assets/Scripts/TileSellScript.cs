using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;

public class TileSellScript : MonoBehaviour {

    public bool sellModeEnabled;
   
    SocketIOComponent socket;

    void Start()
    {

        sellModeEnabled = false;

      
     

        socket = DisabledObjectsGameScene.socket;

    }


    public void TheClick()
    {


        if (DisabledObjectsGameScene.BuyMenuPanel.activeSelf) //buymenu panel is currently open
        {
            StartCoroutine(GameObject.Find("BuyButton").GetComponent<BuyButtonScript>().BuyMenuPanelFader());

        }
        if (Globals.cameraUp && DisabledObjectsGameScene.Selector.GetComponent<BuyMode>().enabled) //buy mode is enabled. Cancel buy mode.
        {

            DisabledObjectsGameScene.Selector.GetComponent<BuyMode>().DisableBuyMode();
           
        }

        EnableDisableSellMode();



    }


    public void EnableDisableSellMode()
    {

        if (sellModeEnabled)
        {
            sellModeEnabled = false;
            ApplyModeTransition(false);
        }else
        {
            sellModeEnabled = true;
            ApplyModeTransition(true);

        }

    }

    private void ApplyModeTransition(bool sell)
    {

        if (sell)
        {
            RenderSettings.skybox = DisabledObjectsGameScene.dark_skybox;
            StartCoroutine(moveCamera());

        }else
        {
            StartCoroutine(moveCamera());
            RenderSettings.skybox = DisabledObjectsGameScene.light_skybox;

        }


    }

    void Update()
    {

        if (sellModeEnabled) { 

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.tag == "Building")
            {

                if (Input.GetMouseButtonDown(0))
                {
                        SellTile(hit.transform.gameObject.GetComponent<BuildingScript>().thistile.ID, hit.transform.gameObject.GetComponent<BuildingScript>().thistile.NAME);
                        Destroy(hit.transform.gameObject);
                        Database.ActiveTiles.Remove(hit.transform.gameObject);
                    }

            }
            

        }

        }
    }

    public void SellTile(int ID, string name)
    {

        Debug.Log("trying to sell tile ID " + ID);

       
        Dictionary<string, string> data = new Dictionary<string, string>();
        data["Uname"] = GlobalControl.Uname;
        data["SellTileID"] = ID.ToString();
        data["TileName"] = name;

        socket.Emit("SELL_TILE", new JSONObject(data));

    }

    IEnumerator moveCamera()
    {
        float step = 10 * Time.deltaTime;

        if (!Globals.cameraUp)  //raise cam
        {
            Globals.cameraUp = true;
            while (Camera.main.transform.position.y < 3.2f)
            {
                yield return new WaitForSeconds(0.001f);
                Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, new Vector3(Camera.main.transform.position.x, 3.28f, -1.94f), step);
            }
        

            yield break;
        }
        else if (Globals.cameraUp)  //lower cam
        {
            Globals.cameraUp = false;
            while (Camera.main.transform.position.y > 1.7f)
            {
                yield return new WaitForSeconds(0.0001f);
                Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, new Vector3(Camera.main.transform.position.x, 1.63f, -3.8f), step);
            }
        

            yield break;
        }

    }


}
