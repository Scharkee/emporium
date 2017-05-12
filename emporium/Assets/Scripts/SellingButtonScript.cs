using UnityEngine;

public class SellingButtonScript : MonoBehaviour
{
    public void expandContract()
    {
        if (DisabledObjectsGameScene.Instance.SellingPanel.activeSelf)
        {
            DisabledObjectsGameScene.Instance.SellingPanel.SetActive(false);
        }
        else
        {
            DisabledObjectsGameScene.Instance.SellingPanel.SetActive(true);
        }

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
    }
}