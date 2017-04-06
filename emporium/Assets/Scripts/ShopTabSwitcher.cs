using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopTabSwitcher : MonoBehaviour {


  
	// Use this for initialization
	void Start () {




        DisabledObjectsGameScene.gridBuildings.SetActive(false);
    }


    public void ReceivePlantTabButtonClick()
    {

        if (DisabledObjectsGameScene.gridPlants.activeSelf)
        {


        }else
        {

            DisabledObjectsGameScene.gridBuildings.SetActive(false);
            DisabledObjectsGameScene.gridPlants.SetActive(true);

        }


    }

    public void ReceiveBuildingTabButtonClick()
    {


        if (DisabledObjectsGameScene.gridBuildings.activeSelf)
        {


        }
        else
        {
            DisabledObjectsGameScene.gridPlants.SetActive(false);
            DisabledObjectsGameScene.gridBuildings.SetActive(true);
           

        }

    }






}
