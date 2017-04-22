using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPanel : MonoBehaviour {

    UIManager uimanager;
    private bool firstStart=true;

    void Start()
    {

        uimanager = GameObject.Find("_ManagerialScripts").GetComponent<UIManager>();
        
    }

	void OnEnable()
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
        Debug.Log("adjusting values");

        //ADD NEW FRUITS AND JUICES

        UIManager.Instance.ChangeUIText("kriauses_Editable", Database.Instance.Inventory["kriauses"].ToString());
        UIManager.Instance.ChangeUIText("apelsinai_Editable", Database.Instance.Inventory["apelsinai"].ToString());
        UIManager.Instance.ChangeUIText("persikai_Editable", Database.Instance.Inventory["persikai"].ToString());
        UIManager.Instance.ChangeUIText("nektarinai_Editable", Database.Instance.Inventory["nektarinai"].ToString());
        UIManager.Instance.ChangeUIText("kiviai_Editable", Database.Instance.Inventory["kiviai"].ToString());
        UIManager.Instance.ChangeUIText("slyvos_Editable", Database.Instance.Inventory["slyvos"].ToString());



        UIManager.Instance.ChangeUIText("kriauses_Sultys_Editable", Database.Instance.Inventory["kriauses_sultys"].ToString());
        UIManager.Instance.ChangeUIText("apelsinai_Sultys_Editable", Database.Instance.Inventory["apelsinai_sultys"].ToString());
        UIManager.Instance.ChangeUIText("persikai_Sultys_Editable", Database.Instance.Inventory["persikai_sultys"].ToString());
        UIManager.Instance.ChangeUIText("slyvos_Sultys_Editable", Database.Instance.Inventory["slyvos_sultys"].ToString());

    }
}
