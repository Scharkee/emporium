using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class ContextManager : MonoBehaviour {

    static SocketManager socman;

    Color normal;
    Color invisText;

	// Use this for initialization
	void Start () {

        socman = GameObject.Find("_ManagerialScripts").GetComponent<SocketManager>();
		
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

    public static void CloseStatPanel()
    {

        DisabledObjectsGameScene.StatsContextPanel.SetActive(false);

    }

    public static void ShowStats(GameObject building)
    {
        BuildingScript buildingscript = building.GetComponent<BuildingScript>();

        if (!DisabledObjectsGameScene.StatsContextPanel.activeSelf) //jei nera ijungtasa stat panel tai ijungiam
        {
            DisabledObjectsGameScene.StatsContextPanel.SetActive(true);
        }
            

            if (buildingscript.thistileInfo.BUILDING_TYPE == 0) //augalas
            {
            string finishedString;

            int time = (buildingscript.thistile.START_OF_GROWTH + buildingscript.thistileInfo.PROG_AMOUNT) - socman.unix;
            TimeSpan ts = TimeSpan.FromSeconds(time);

            if (time <= 0)
            {
                finishedString = GlobalControl.currentLangDict["done_plant_growth"];
            }else
            {
                finishedString = string.Format("{0:D2}:{1:D2}:{2:D2}", ts.Hours, ts.Minutes, ts.Seconds);
            }
               


                DisabledObjectsGameScene.StatsContextPanel.SetActive(true);
                DisabledObjectsGameScene.StatsContextPanel.transform.FindChild("Stats_Tilename_editable").GetComponent<Text>().text = IDHelper.NameToRealName(buildingscript.thistile.NAME);
                DisabledObjectsGameScene.StatsContextPanel.transform.FindChild("Stat_Progress_editable").GetComponent<Text>().text = finishedString;
                DisabledObjectsGameScene.StatsContextPanel.transform.FindChild("Stat_Workname_editable").GetComponent<Text>().text = "";

        }
        else if(buildingscript.thistileInfo.BUILDING_TYPE == 1) //pastatas
        {

            string finishedString;

            int time = (buildingscript.thistile.START_OF_GROWTH + buildingscript.thistile.BUILDING_CURRENT_WORK_AMOUNT * (buildingscript.thistileInfo.PROG_AMOUNT / 100)) - socman.unix;
            
            TimeSpan ts = TimeSpan.FromSeconds(time);


            if (time <= 0)
            {
                finishedString = GlobalControl.currentLangDict["done_collect"];
            }
            else
            {
                finishedString = string.Format("{0:D2}:{1:D2}:{2:D2}", ts.Hours, ts.Minutes, ts.Seconds);
            }



            DisabledObjectsGameScene.StatsContextPanel.SetActive(true);
            DisabledObjectsGameScene.StatsContextPanel.transform.FindChild("Stats_Tilename_editable").GetComponent<Text>().text = IDHelper.NameToRealName( buildingscript.thistile.NAME);
            DisabledObjectsGameScene.StatsContextPanel.transform.FindChild("Stat_Progress_editable").GetComponent<Text>().text = string.Format("{0:D2}:{1:D2}:{2:D2}", ts.Hours, ts.Minutes, ts.Seconds);
            DisabledObjectsGameScene.StatsContextPanel.transform.FindChild("Stat_Workname_editable").GetComponent<Text>().text = buildingscript.thistile.WORK_NAME;



        }







    }


}
