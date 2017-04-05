using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisabledObjectsGameScene : MonoBehaviour {

    public static GameObject PressContextPanel;
    public static GameObject LoadingPanel;
    public static GameObject Inventory_Fruit_panel;
    public static GameObject Tiles;
    public static GameObject BuyMenuPanel;
    public static GameObject Selector;

    // Use this for initialization
    void Start () {

        StartCoroutine(delayedDisable());
        

    }
    void Awake()
    {
        PressContextPanel = GameObject.Find("PressContextPanel");
        LoadingPanel = GameObject.Find("LoadingPanel");
        Inventory_Fruit_panel = GameObject.Find("Inventory_Fruit_panel");
        Tiles= GameObject.Find("Tiles");
        BuyMenuPanel = GameObject.Find("BuyMenuPanel");
        Selector = GameObject.Find("Selector");


    }

    IEnumerator delayedDisable() //should test if works against bad 
    {

        while (DisabledObjectsGameScene.LoadingPanel)
        {
            yield return new WaitForSeconds(0.1f);
           

        }

        GameObject.Find("InventoryDropDown").GetComponent<CanvasGroup>().alpha = 1f;
        Inventory_Fruit_panel.SetActive(false);
    }



}
