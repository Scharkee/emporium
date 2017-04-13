using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvExpander : MonoBehaviour {
    

    void Start()
    {


    DisabledObjectsGameScene.Instance.Inventory_Fruit_panel.SetActive(false);

    }

 


    public void expandContract() //TODO: make normal disables and not alpha shit
    {

        if (DisabledObjectsGameScene.Instance.Inventory_Fruit_panel.activeSelf)
        {

            DisabledObjectsGameScene.Instance.Inventory_Fruit_panel.SetActive(false);
        }else
        {
            DisabledObjectsGameScene.Instance.Inventory_Fruit_panel.SetActive(true);
        }
    }

}
