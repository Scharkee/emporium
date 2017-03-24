using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvExpander : MonoBehaviour {
    

    void Start()
    {


    }

 


    public void expandContract()
    {

        if (DisabledObjectsGameScene.Inventory_Fruit_panel.activeSelf)
        {
            DisabledObjectsGameScene.Inventory_Fruit_panel.SetActive(false);
        }else
        {
            DisabledObjectsGameScene.Inventory_Fruit_panel.SetActive(true);
        }
    }

}
