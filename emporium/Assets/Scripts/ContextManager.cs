using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ContextManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    public static void StartPressContext(bool working)
    {

        if (working)  // uzsiundyta darbo, bet nepabaigta. Open stats panel + expected time until finsihed
        {
            


        }else if (!working) //nera darbo. Job assigment panel.
        {

            DisabledObjectsGameScene.PressContextPanel.SetActive(true);
           

        }


    }

    public static void CloseAndResetPressContext()
    {

        DisabledObjectsGameScene.PressContextPanel.transform.FindChild("Press_AssignJob_InputField").GetComponent<InputField>().text="";
        DisabledObjectsGameScene.PressContextPanel.transform.FindChild("Press_AssignJob_ProdTypeDropdown").GetComponent<Dropdown>().value = 0;

        DisabledObjectsGameScene.PressContextPanel.SetActive(false);


        


    }

    public void CancelOutContextPanels()
    {

        if (DisabledObjectsGameScene.PressContextPanel.GetComponent<PressContextPanelScript>().aliveForHalfSec)
        {
            CloseAndResetPressContext();  //preso conteksta uzdarom

        }

       
        //stats konteksta uzdarom ir t.t


    }


}
