using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyMode : MonoBehaviour {

    
    string buildingName;
    string TileName;
    bool darken;
    int camspeed = 10;
    GameObject menupanell;

    Material light_skybox;


    // Use this for initialization
    void Start () {
		
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
                 
                    RenderSettings.skybox = light_skybox; //MAKEME make fade     //FIXME fix material of normal skybox

                    menupanell.SetActive(true);


                    GameObject.Find("BuyScript").GetComponent<BuyScript>().ChoosePlot(buildingName, X, Z);

                }

            }
            else
            {
                if (Input.GetMouseButtonDown(0))
                {
                   //MAKEME reset view (build cancelled)

                }
            }

       

          

        }

    }

    public void receiveName(string name, GameObject menupanel)
    {
        
        buildingName = name;
        menupanell = menupanel;



        //effects and pltoselectors
        StartCoroutine(liftcamera());
        GameObject.Find("_GameScripts").GetComponent<PlotSelector>().plotselectors.SetActive(true);

        light_skybox = RenderSettings.skybox;
        RenderSettings.skybox = Resources.Load("Materials/Skybox_mat_darkened") as Material; // MAKEME fade here

    }

   

    IEnumerator liftcamera()
    {


        float step = camspeed * Time.deltaTime;


        if (Camera.main.transform.position.y<3)  //raise cam
        {
            while (Camera.main.transform.position.y < 3.2f)
            {
                yield return new WaitForSeconds(0.001f);

              
                Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, new Vector3(Camera.main.transform.position.x, 3.28f, -1.94f), step);

            }

            yield break;


        }
        else if (Camera.main.transform.position.y > 3)  //lower cam
        {
            while (Camera.main.transform.position.y > 1.7f)
            {
                yield return new WaitForSeconds(0.0001f);

              
                Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, new Vector3(Camera.main.transform.position.x, 1.63f, -3.8f), step);
            }

            yield break;
        }
        
}

    
}
