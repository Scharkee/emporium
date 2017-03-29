using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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
	

}
