using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPanel : MonoBehaviour {

    UIManager uimanager;

    void Start()
    {

        uimanager = GameObject.Find("_ManagerialScripts").GetComponent<UIManager>();
    }

	void OnEnable()
    {
        
        Debug.Log("adjusting values");

        //ADD NEW FRUITS AND JUICES

        UIManager.ChangeUIText("kriauses_Editable", Database.Inventory["kriauses"].ToString());
        UIManager.ChangeUIText("apelsinai_Editable", Database.Inventory["apelsinai"].ToString());
        UIManager.ChangeUIText("persikai_Editable", Database.Inventory["persikai"].ToString());
        UIManager.ChangeUIText("nektarinai_Editable", Database.Inventory["nektarinai"].ToString());
        UIManager.ChangeUIText("kiviai_Editable", Database.Inventory["kiviai"].ToString());
        UIManager.ChangeUIText("slyvos_Editable", Database.Inventory["slyvos"].ToString());


      
        UIManager.ChangeUIText("kriauses_Sultys_Editable", Database.Inventory["kriauses_sultys"].ToString());
        UIManager.ChangeUIText("apelsinai_Sultys_Editable", Database.Inventory["apelsinai_sultys"].ToString());
        UIManager.ChangeUIText("persikai_Sultys_Editable", Database.Inventory["persikai_sultys"].ToString());
        UIManager.ChangeUIText("slyvos_Sultys_Editable", Database.Inventory["slyvos_sultys"].ToString());




    }
}
