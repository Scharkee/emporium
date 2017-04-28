using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class ContextManager : MonoBehaviour
{

    public static ContextManager Instance;

    SocketManager socman;

    Color normal;
    Color invisText;

    // Use this for initialization
    void Start()
    {

        socman = GameObject.Find("_ManagerialScripts").GetComponent<SocketManager>();

    }

    void Awake()
    {
        Instance = this;


    }

    public void StartPressContext(bool working)
    {

        if (working)  // uzsiundyta darbo, bet nepabaigta. Open stats panel + expected time until finsihed
        {



        }
        else if (!working) //nera darbo. Job assigment panel.
        {

            DisabledObjectsGameScene.Instance.PressContextPanel.SetActive(true);


        }


    }


    public void StartProduceSellingContext()
    {


        DisabledObjectsGameScene.Instance.PressContextPanel.SetActive(true);


    }

    public void CloseAndResetPressContext()
    {

        DisabledObjectsGameScene.Instance.PressContextPanel.transform.FindChild("Press_AssignJob_InputField").GetComponent<InputField>().text = "";
        DisabledObjectsGameScene.Instance.PressContextPanel.transform.FindChild("Press_AssignJob_ProdTypeDropdown").GetComponent<Dropdown>().value = 0;

        DisabledObjectsGameScene.Instance.PressContextPanel.SetActive(false);





    }


    public void CancelContext()
    {

        // Globals.Instance.canvas.BroadcastMessage("CancelContext");

        if (DisabledObjectsGameScene.Instance.PressContextPanel.GetComponent<PressContextPanelScript>().aliveForHalfSec)
        {
            CloseAndResetPressContext();  //preso conteksta uzdarom

        }


        //stats konteksta uzdarom ir t.t


    }

    public void CloseStatPanel()
    {

        DisabledObjectsGameScene.Instance.StatsContextPanel.SetActive(false);

    }

    public void ShowStats(GameObject building)
    {
        BuildingScript buildingscript = building.GetComponent<BuildingScript>();

        if (!DisabledObjectsGameScene.Instance.StatsContextPanel.activeSelf) //jei nera ijungtasa stat panel tai ijungiam
        {
            DisabledObjectsGameScene.Instance.StatsContextPanel.SetActive(true);
        }


        if (buildingscript.thistileInfo.BUILDING_TYPE == 0) //augalas
        {
            string finishedString;

            int time = (buildingscript.thistile.START_OF_GROWTH + buildingscript.thistileInfo.PROG_AMOUNT) - socman.unix;
            TimeSpan ts = TimeSpan.FromSeconds(time);

            if (time <= 0)
            {
                finishedString = GlobalControl.Instance.currentLangDict["done_plant_growth"];
            }
            else
            {
                finishedString = string.Format("{0:D2}:{1:D2}:{2:D2}", ts.Hours, ts.Minutes, ts.Seconds);
            }



            DisabledObjectsGameScene.Instance.StatsContextPanel.SetActive(true);
            DisabledObjectsGameScene.Instance.StatsContextPanel.transform.FindChild("Stats_Tilename_editable").GetComponent<Text>().text = IDHelper.Instance.NameToRealName(buildingscript.thistile.NAME);
            DisabledObjectsGameScene.Instance.StatsContextPanel.transform.FindChild("Stat_Progress_editable").GetComponent<Text>().text = finishedString;
            DisabledObjectsGameScene.Instance.StatsContextPanel.transform.FindChild("Stat_Workname_editable").GetComponent<Text>().text = "";

        }
        else if (buildingscript.thistileInfo.BUILDING_TYPE == 1) //pastatas
        {

            string finishedString;

            int time = (buildingscript.thistile.START_OF_GROWTH + buildingscript.thistile.BUILDING_CURRENT_WORK_AMOUNT * (buildingscript.thistileInfo.PROG_AMOUNT / 100)) - socman.unix;

            TimeSpan ts = TimeSpan.FromSeconds(time);


            if (time <= 0)
            {
                finishedString = GlobalControl.Instance.currentLangDict["done_collect"];
            }
            else
            {
                finishedString = string.Format("{0:D2}:{1:D2}:{2:D2}", ts.Hours, ts.Minutes, ts.Seconds);
            }



            DisabledObjectsGameScene.Instance.StatsContextPanel.SetActive(true);
            DisabledObjectsGameScene.Instance.StatsContextPanel.transform.FindChild("Stats_Tilename_editable").GetComponent<Text>().text = IDHelper.Instance.NameToRealName(buildingscript.thistile.NAME);
            DisabledObjectsGameScene.Instance.StatsContextPanel.transform.FindChild("Stat_Progress_editable").GetComponent<Text>().text = finishedString;
            DisabledObjectsGameScene.Instance.StatsContextPanel.transform.FindChild("Stat_Workname_editable").GetComponent<Text>().text = buildingscript.thistile.WORK_NAME;



        }







    }


}
