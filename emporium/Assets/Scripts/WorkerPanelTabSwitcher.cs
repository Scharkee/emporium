using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorkerPanelTabSwitcher : MonoBehaviour
{
    public void SwitchToPanel(int panel)
    {
        ColorBlock newColor = new ColorBlock();
        ClickEngine.Instance.Click();

        switch (panel)
        {
            case 0: //hired panel

                DisabledObjectsGameScene.Instance.Worker_panel_hired_panel.SetActive(true);
                DisabledObjectsGameScene.Instance.Worker_panel_available_panel.SetActive(false);

                //highlitinam buttona
                newColor = DisabledObjectsGameScene.Instance.Worker_panel_hired_panel_button.GetComponent<Button>().colors;
                newColor.normalColor = Globals.Instance.WorkerPanelTabOn;
                DisabledObjectsGameScene.Instance.Worker_panel_hired_panel_button.GetComponent<Button>().colors = newColor;

                //un-highlightinam kita button
                newColor.normalColor = Globals.Instance.WorkerPanelTabOff;
                DisabledObjectsGameScene.Instance.Worker_panel_available_panel_button.GetComponent<Button>().colors = newColor;
                break;

            case 1: //available panel

                DisabledObjectsGameScene.Instance.Worker_panel_hired_panel.SetActive(false);
                DisabledObjectsGameScene.Instance.Worker_panel_available_panel.SetActive(true);

                //highlitinam buttona
                newColor = DisabledObjectsGameScene.Instance.Worker_panel_available_panel_button.GetComponent<Button>().colors;
                newColor.normalColor = Globals.Instance.WorkerPanelTabOn;
                DisabledObjectsGameScene.Instance.Worker_panel_available_panel_button.GetComponent<Button>().colors = newColor;

                //un-highlightinam kita button
                newColor.normalColor = Globals.Instance.WorkerPanelTabOff;
                DisabledObjectsGameScene.Instance.Worker_panel_hired_panel_button.GetComponent<Button>().colors = newColor;

                break;

            default:
                Debug.Log("wrong panel int");
                break;
        }
    }
}