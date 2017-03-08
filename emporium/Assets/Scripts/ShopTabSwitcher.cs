using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopTabSwitcher : MonoBehaviour {


    GameObject gridPlants;
    GameObject gridBuildings;
   

	// Use this for initialization
	void Start () {




        gridBuildings.SetActive(false);
    }

    void Awake()
    {

        gridPlants = GameObject.Find("OptionGrid");
        gridBuildings = GameObject.Find("OptionGridBuildings");
    }

    public void ReceivePlantTabButtonClick()
    {

        if (gridPlants.activeSelf)
        {


        }else
        {

            gridBuildings.SetActive(false);
            gridPlants.SetActive(true);

        }


    }

    public void ReceiveBuildingTabButtonClick()
    {


        if (gridBuildings.activeSelf)
        {


        }
        else
        {

            gridBuildings.SetActive(true);
            gridPlants.SetActive(false);

        }

    }






}
