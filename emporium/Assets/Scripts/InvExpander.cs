using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvExpander : MonoBehaviour {
    

    void Start()
    {


    }

 


    public void expandContract() //TODO: make normal disables and not alpha shit
    {

        if (DisabledObjectsGameScene.Inventory_Fruit_panel.GetComponent<CanvasGroup>().alpha == 0)
        {

            DisabledObjectsGameScene.Inventory_Fruit_panel.GetComponent<CanvasGroup>().alpha = 1;
        }else
        {
            DisabledObjectsGameScene.Inventory_Fruit_panel.GetComponent<CanvasGroup>().alpha = 0;
        }
    }

}
