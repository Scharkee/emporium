using UnityEngine;

public class UniversalBank : MonoBehaviour
{
    public int one;
    public int two;

    public string produceName, PurchaseName;
    public GameObject go1;

    public void BuyWithTileName(string name, int buildingType)
    {
        DisabledObjectsGameScene.Instance.BuyMenuPanel.GetComponent<CanvasGroup>().alpha = 0f;
        DisabledObjectsGameScene.Instance.BuyMode.GetComponent<BuyMode>().enabled = true;
        DisabledObjectsGameScene.Instance.BuyMode.GetComponent<BuyMode>().receiveBuilding(name, buildingType);
        DisabledObjectsGameScene.Instance.BuyMenuPanel.SetActive(false);
    }
}