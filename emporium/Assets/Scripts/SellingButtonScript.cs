using UnityEngine;

public class SellingButtonScript : MonoBehaviour
{
    public void expandContract()
    {
        ClickEngine.Instance.Click();
        try
        {
            Globals.Instance.canvas.BroadcastMessage("CancelContext");
        }
        catch
        {
        }

        Globals.Instance.UIBloomActive(!DisabledObjectsGameScene.Instance.SellingPanel.activeSelf);
        DisabledObjectsGameScene.Instance.SellingPanel.SetActive(!DisabledObjectsGameScene.Instance.SellingPanel.activeSelf);

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
    }
}