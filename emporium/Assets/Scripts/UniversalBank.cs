using System.Collections.Generic;
using UnityEngine;

public class UniversalBank : MonoBehaviour
{
    public int one;
    public int two;

    public string produceName, PurchaseName;
    public GameObject go1;

    //random helpers for a lot of different components
    public void BuyWithTileName(string name, int buildingType)
    {
        DisabledObjectsGameScene.Instance.BuyMenuPanel.GetComponent<CanvasGroup>().alpha = 0f;
        DisabledObjectsGameScene.Instance.BuyMode.GetComponent<BuyMode>().enabled = true;
        DisabledObjectsGameScene.Instance.BuyMode.GetComponent<BuyMode>().receiveBuilding(name, buildingType);
        DisabledObjectsGameScene.Instance.BuyMenuPanel.SetActive(false);
    }

    public void HireWorker()
    {
        Dictionary<string, string> data = new Dictionary<string, string>();

        data["Uname"] = GlobalControl.Instance.Uname;
        data["Hired-AvailableWorkerID"] = one.ToString();
        DisabledObjectsGameScene.Instance.socket.Emit("HIRE_WORKER", new JSONObject(data));
        Destroy(gameObject.transform.parent.parent);
    }
}