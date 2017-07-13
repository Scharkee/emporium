﻿using UnityEngine;

public class InvExpander : MonoBehaviour
{
    private void Start()
    {
    }

    public void expandContract()
    {
        ClickEngine.Instance.Click();

        if (DisabledObjectsGameScene.Instance.Inventory_Fruit_panel.activeSelf)
        {
            DisabledObjectsGameScene.Instance.Inventory_Fruit_panel.SetActive(false);
        }
        else
        {
            DisabledObjectsGameScene.Instance.Inventory_Fruit_panel.SetActive(true);
        }
    }
}