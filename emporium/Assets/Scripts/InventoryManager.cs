using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    private GameObject inventoryListItemPrefab, currentInventoryListItem;

    private void Start()
    {
        inventoryListItemPrefab = Resources.Load("UI/Inventory_ListItem") as GameObject;
    }

    public void GenerateInventory()
    {
        foreach (KeyValuePair<string, float> inventoryItem in Database.Instance.Inventory)
        {
            if (inventoryItem.Key.Contains("_sultys")) //handlinam sulciu inventory list-item'a
            {
                currentInventoryListItem = Instantiate(inventoryListItemPrefab, DisabledObjectsGameScene.Instance.Inventory_Juice_Panel.transform) as GameObject;
                currentInventoryListItem.name = inventoryItem.Key;
                currentInventoryListItem.GetComponent<Text>().text = Languages.Instance.currentLanguage[inventoryItem.Key];
                currentInventoryListItem.transform.Find("ListItem_Editable").GetComponent<Text>().text = inventoryItem.Value.ToString();
                currentInventoryListItem.transform.Find("ListItem_Editable").name = inventoryItem.Key + "_editable";
            }
            else //handlinam produce(fruit)  inventory list-item'a
            {
                currentInventoryListItem = Instantiate(inventoryListItemPrefab, DisabledObjectsGameScene.Instance.Inventory_Produce_Panel.transform) as GameObject;
                currentInventoryListItem.name = inventoryItem.Key;
                currentInventoryListItem.GetComponent<Text>().text = Languages.Instance.currentLanguage[inventoryItem.Key];
                currentInventoryListItem.transform.Find("ListItem_Editable").GetComponent<Text>().text = inventoryItem.Value.ToString();
                currentInventoryListItem.transform.Find("ListItem_Editable").name = inventoryItem.Key + "_editable";
            }
        }
    }

    public void UpdateInventoryPanelValue(string name, float newAmount, float yield, int solidOrJuice)
    {
        try
        {
            DisabledObjectsGameScene.Instance.Inventory_Juice_Panel.transform.Find(name + "_editable").GetComponent<Text>().text = newAmount.ToString();
        }
        catch
        {
            Debug.Log("inventory neatidarytas, updatinam tik DB inv value");
        }

        Database.Instance.AddToStoredAmounts(yield, solidOrJuice);
    }
}