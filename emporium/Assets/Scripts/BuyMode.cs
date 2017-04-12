﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyMode : MonoBehaviour {

    
    string buildingName;
    string TileName;
    bool darken;
    bool disableQueued;
    int camspeed = 10;

   
    BuyButtonScript buybuttonscript;

    // Use this for initialization
    void Start () {
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
	void Update () {



        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.tag == "Selector")
            {

                if (Input.GetMouseButtonDown(0))
                {
                    Debug.Log("hit selector at pos: " + hit.transform);

                    float X = hit.transform.localPosition.x;
                    float Z = hit.transform.localPosition.z;

                    GameObject.Find("PlotSelectors").SetActive(false);
                 
                    StartCoroutine(liftcamera());
                    buybuttonscript.panelEnabled = false;

                    RenderSettings.skybox = DisabledObjectsGameScene.light_skybox; //MAKEME make fade     //FIXME fix material of normal skybox

                    DisabledObjectsGameScene.BuyMenuPanel.SetActive(false);

                  

                    GameObject.Find("BuyScript").GetComponent<BuyScript>().ChoosePlot(buildingName, X, Z);

                }

            }
            else
            {
                
            }

       
        }

    }

    public void receiveName(string name)
    {
        
        buildingName = name;
     
        buybuttonscript.panelEnabled = false;

        DisabledObjectsGameScene.cameraBlur.enabled = false;
        //effects and pltoselectors
        StartCoroutine(liftcamera());
        RenderSettings.skybox = DisabledObjectsGameScene.dark_skybox;
        GameObject.Find("_GameScripts").GetComponent<PlotSelector>().plotselectors.SetActive(true);


    }

   

    IEnumerator liftcamera()
    {


        float step = camspeed * Time.deltaTime;


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

        if (disableQueued)
        {

            gameObject.SetActive(false);
        }
        
}

    public void DisableBuyMode()
    {
        if (DisabledObjectsGameScene.PlotSelectors.activeSelf) //compatibility with sell mode & protection against crashing out
        {
            DisabledObjectsGameScene.PlotSelectors.SetActive(false);

        }
  

        StartCoroutine(liftcamera());
        // buybuttonscript.panelEnabled = false;
        Debug.Log("changing back skybox");
        RenderSettings.skybox = DisabledObjectsGameScene.light_skybox; //MAKEME make fade    
        Debug.Log("changing back skybox");
        DisabledObjectsGameScene.BuyMenuPanel.SetActive(true);


        disableQueued = true;
       


    }

    
}
