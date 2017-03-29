using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisabledObjectsGameScene : MonoBehaviour {

    public static GameObject PressContextPanel;
    public static GameObject LoadingPanel;
    public static GameObject Inventory_Fruit_panel;
    public static GameObject Tiles;

    // Use this for initialization
    void Start () {
       // Inventory_Fruit_panel.SetActive(false);  TODO: should disable this but without breaking data transfer

    }
    void Awake()
    {
        PressContextPanel = GameObject.Find("PressContextPanel");
        LoadingPanel = GameObject.Find("LoadingPanel");
        Inventory_Fruit_panel = GameObject.Find("Inventory_Fruit_panel");
        Tiles= GameObject.Find("Tiles");


    }



}
