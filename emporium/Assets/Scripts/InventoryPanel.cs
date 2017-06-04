using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryPanel : MonoBehaviour
{
    private UIManager uimanager;
    private bool firstStart = true;

    private void Start()
    {
        uimanager = GameObject.Find("_ManagerialScripts").GetComponent<UIManager>();
    }

    private void OnEnable()
    {
        if (!firstStart)
        {
            adjustValues();
        }
        else
        {
            firstStart = false;
        }
    }

    public void adjustValues()
    {
        //ADD NEW FRUITS AND JUICES

        foreach (KeyValuePair<string, float> inventoryItem in Database.Instance.Inventory)
        {
            if (inventoryItem.Key.Contains("_sultys")) //handlinam sulciu inventory list-item'a
            {
                DisabledObjectsGameScene.Instance.Inventory_Juice_Panel.transform.Find(inventoryItem.Key + "/" + inventoryItem.Key + "_editable").GetComponent<Text>().text = inventoryItem.Value.ToString();
            }
            else //handlinam produce inventory list-item'a
            {
                DisabledObjectsGameScene.Instance.Inventory_Produce_Panel.transform.Find(inventoryItem.Key + "/" + inventoryItem.Key + "_editable").GetComponent<Text>().text = inventoryItem.Value.ToString();
            }
        }
    }
}