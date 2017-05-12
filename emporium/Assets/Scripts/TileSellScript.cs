using SocketIO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileSellScript : MonoBehaviour
{
    public AudioClip coin1;
    public AudioClip coin2;
    public AudioClip coin3;
    public AudioClip coin4;

    public bool sellModeEnabled;

    private SocketIOComponent socket;

    private void Start()
    {
        sellModeEnabled = false;

        socket = DisabledObjectsGameScene.Instance.socket;
    }

    public void TheClick()
    {
        if (DisabledObjectsGameScene.Instance.BuyMenuPanel.activeSelf) //buymenu panel is currently open
        {
            StartCoroutine(DisabledObjectsGameScene.Instance.BuyButton.GetComponent<BuyButtonScript>().BuyMenuPanelCloser());
        }
        if (Globals.Instance.cameraUp && DisabledObjectsGameScene.Instance.BuyMode.GetComponent<BuyMode>().enabled) //buy mode is enabled. Cancel buy mode.
        {
            DisabledObjectsGameScene.Instance.BuyMode.GetComponent<BuyMode>().DisableBuyMode(false);
        }
        if (DisabledObjectsGameScene.Instance.EconomyPanel.activeSelf)
        {
            DisabledObjectsGameScene.Instance.EconomyPanel.SetActive(false);
        }

        if (DisabledObjectsGameScene.Instance.SellingPanel.activeSelf)
        {
            DisabledObjectsGameScene.Instance.SellingPanel.SetActive(false);
        }

        EnableDisableSellMode();
    }

    public void EnableDisableSellMode()
    {
        if (sellModeEnabled)
        {
            sellModeEnabled = false;
            ApplyModeTransition(false);
        }
        else
        {
            sellModeEnabled = true;
            ApplyModeTransition(true);
        }
    }

    public void ApplyModeTransition(bool sell)
    {
        if (sell)
        {
            DisabledObjectsGameScene.Instance.SellButton.GetComponent<Image>().color = Globals.Instance.buttonActiveColor1;
            DisabledObjectsGameScene.Instance.CancelPanel.SetActive(false);
            DisabledObjectsGameScene.Instance.Camcontroller.PerformCamElevetion();
        }
        else
        {
            DisabledObjectsGameScene.Instance.SellButton.GetComponent<Image>().color = Globals.Instance.buttonColor1;
            DisabledObjectsGameScene.Instance.Camcontroller.PerformCamElevetion();
            DisabledObjectsGameScene.Instance.CancelPanel.SetActive(true);
        }
    }

    private void Update()
    {
        if (sellModeEnabled)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.tag == "Building" && !DisabledObjectsGameScene.Instance.alertPanel.activeSelf)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        if (hit.transform.gameObject.GetComponent<BuildingScript>().thistileInfo.BUILDING_TYPE == 3) //ziurim ar galime parduoti PRODUCE storage
                        {
                            if (Database.Instance.Storage.TakenProduceStorage > Database.Instance.Storage.TotalProduceStorage - hit.transform.gameObject.GetComponent<BuildingScript>().thistileInfo.PROG_AMOUNT)
                            { //negalim parduot nes netalpa vaisiai
                                GameAlerts.Instance.AlertWithMessage("You have too much produce! You cannot sell produce storage at this moment.");
                            }
                            else
                            {
                                Database.Instance.RemoveFromMaxStorageAmounts((hit.transform.gameObject.GetComponent<BuildingScript>().thistileInfo.PROG_AMOUNT), 0);
                                Database.Instance.ActiveProduceStorage.Remove((hit.transform.gameObject.GetComponent<BuildingScript>()));
                                Sell(hit);
                            }
                        }
                        else if (hit.transform.gameObject.GetComponent<BuildingScript>().thistileInfo.BUILDING_TYPE == 4) //ziurim ar galime parduoti JUICE storage
                        {
                            if (Database.Instance.Storage.TakenJuiceStorage > Database.Instance.Storage.TotalJuiceStorage - hit.transform.gameObject.GetComponent<BuildingScript>().thistileInfo.PROG_AMOUNT)
                            { //negalim parduot nes netalpa sultys
                                GameAlerts.Instance.AlertWithMessage("You have too much juice! You cannot sell juice storage at this moment.");
                            }
                            else
                            {
                                Database.Instance.RemoveFromMaxStorageAmounts((hit.transform.gameObject.GetComponent<BuildingScript>().thistileInfo.PROG_AMOUNT), 1);
                                Database.Instance.ActiveJuiceStorage.Remove((hit.transform.gameObject.GetComponent<BuildingScript>()));
                                Sell(hit);
                            }
                        }
                        else //galim parduot
                        {
                            Sell(hit);
                        }
                    }
                }
            }
        }
    }

    private void Sell(RaycastHit hit)
    {
        SellTile(hit.transform.gameObject.GetComponent<BuildingScript>().thistile.ID, hit.transform.gameObject.GetComponent<BuildingScript>().thistile.NAME);
        Destroy(hit.transform.gameObject);

        GetComponent<AudioSource>().clip = coinSound();
        GetComponent<AudioSource>().Play();

        Database.Instance.ActiveTiles.Remove(hit.transform.gameObject);
    }

    public void SellTile(int ID, string name)
    {
        Debug.Log("selling " + name);

        Dictionary<string, string> data = new Dictionary<string, string>();
        data["Uname"] = GlobalControl.Instance.Uname;
        data["SellTileID"] = ID.ToString();
        data["TileName"] = name;

        socket.Emit("SELL_TILE", new JSONObject(data));
    }

    public AudioClip coinSound()
    {
        AudioClip randomized;

        int rand = Random.Range(1, 4);

        switch (rand)
        {
            case 1:
                randomized = coin1;
                break;

            case 2:
                randomized = coin2;
                break;

            case 3:
                randomized = coin3;
                break;

            case 4:
                randomized = coin4;
                break;

            default:
                randomized = coin1;
                break;
        }

        return randomized;
    }
}