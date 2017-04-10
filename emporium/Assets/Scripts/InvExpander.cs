using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvExpander : MonoBehaviour {
    

    void Start()
    {
        DisabledObjectsGameScene.Inventory_Fruit_panel.SetActive(false);

    }

 


    public void expandContract() //TODO: make normal disables and not alpha shit
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
