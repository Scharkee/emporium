using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BuyButtonScript : MonoBehaviour
{
    public float panelimagecolor;

    public Color panelimage;

    public bool panelEnabled;

    private GameObject opgrid;

    private void Start()
    {
        panelEnabled = false;
    }

    public void TheClick()
    {
        ClickEngine.Instance.Click();
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
        if (DisabledObjectsGameScene.Instance.tileSellScript.GetComponent<TileSellScript>().sellModeEnabled)
        {
            DisabledObjectsGameScene.Instance.tileSellScript.GetComponent<TileSellScript>().sellModeEnabled = false;
            DisabledObjectsGameScene.Instance.tileSellScript.GetComponent<TileSellScript>().ApplyModeTransition(false);
        }
        if (DisabledObjectsGameScene.Instance.SellingPanel.activeSelf)
        {
            DisabledObjectsGameScene.Instance.SellingPanel.SetActive(false);
        }

        StartCoroutine(BuyMenuPanelFader());
    }

    public IEnumerator BuyMenuPanelFader()
    {
        DisabledObjectsGameScene.Instance.BuyMenuPanel.SetActive(true);

        if (DisabledObjectsGameScene.Instance.BuyMenuPanel.GetComponent<CanvasGroup>().alpha < 1f)
        {
            DisabledObjectsGameScene.Instance.BuyButton.GetComponent<Image>().color = Globals.Instance.buttonActiveColor1;

            panelEnabled = true; // used to stop rotation when viewing panel

            while (DisabledObjectsGameScene.Instance.BuyMenuPanel.GetComponent<CanvasGroup>().alpha < 1f)
            {
                //fadeoutas
                //TODO: gal pakeist i Globals.instance.UIbluractive

                yield return new WaitForSeconds(0.001f);
                //didinam alpha kas cikla
                DisabledObjectsGameScene.Instance.BuyMenuPanel.GetComponent<CanvasGroup>().alpha += 0.1f;

                Globals.Instance.cameraBlur.blurSize += 0.22f;
            }
            Globals.Instance.cameraBlur.enabled = true;
            DisabledObjectsGameScene.Instance.BuyMenuPanel.GetComponent<CanvasGroup>().alpha = 1f;
        }
        else if (DisabledObjectsGameScene.Instance.BuyMenuPanel.GetComponent<CanvasGroup>().alpha > 0f)
        {
            panelEnabled = false;
            DisabledObjectsGameScene.Instance.BuyButton.GetComponent<Image>().color = Globals.Instance.buttonColor1;

            while (DisabledObjectsGameScene.Instance.BuyMenuPanel.GetComponent<CanvasGroup>().alpha > 0f)
            {
                //fadeoutas

                yield return new WaitForSeconds(0.001f);
                //mazinam alpha kas cikla
                DisabledObjectsGameScene.Instance.BuyMenuPanel.GetComponent<CanvasGroup>().alpha -= 0.1f;

                Globals.Instance.cameraBlur.blurSize -= 0.22f;
            }
            Globals.Instance.cameraBlur.enabled = false;
            DisabledObjectsGameScene.Instance.BuyMenuPanel.GetComponent<CanvasGroup>().alpha = 0f;

            DisabledObjectsGameScene.Instance.BuyMenuPanel.SetActive(false);
        }
    }

    public IEnumerator BuyMenuPanelCloser()
    {
        if (DisabledObjectsGameScene.Instance.BuyMenuPanel.activeSelf)
        {
            if (DisabledObjectsGameScene.Instance.BuyMenuPanel.GetComponent<CanvasGroup>().alpha > 0f)
            {
                panelEnabled = false;
                DisabledObjectsGameScene.Instance.BuyButton.GetComponent<Image>().color = Globals.Instance.buttonColor1;

                while (DisabledObjectsGameScene.Instance.BuyMenuPanel.GetComponent<CanvasGroup>().alpha > 0f)
                {
                    //fadeoutas

                    yield return new WaitForSeconds(0.001f);
                    //mazinam alpha kas cikla
                    DisabledObjectsGameScene.Instance.BuyMenuPanel.GetComponent<CanvasGroup>().alpha -= 0.1f;

                    Globals.Instance.cameraBlur.blurSize -= 0.22f;
                }
                Globals.Instance.cameraBlur.enabled = false;
                DisabledObjectsGameScene.Instance.BuyMenuPanel.GetComponent<CanvasGroup>().alpha = 0f;

                DisabledObjectsGameScene.Instance.BuyMenuPanel.SetActive(false);
            }
        }
    }

    public void CancelContext() //parejo broadcastas, isjungti VISUS context panels
    {
        Globals.Instance.UIBloomActive(false);
        StartCoroutine(BuyMenuPanelCloser());
    }
}