using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IDHelper : MonoBehaviour
{
    public static IDHelper Instance;




    void Awake()
    {
        Instance = this;


    }

    public string PressContextPanelIDtoName(int ID)
    {
        string name = "";


        switch (ID)
        {
            case 0:
                name = "obuoliai";
                break;
            case 1:
                name = "apelsinai";
                break;
            case 2:
                name = "slyvos";
                break;
            case 3:
                name = "kriauses";
                break;
            case 4:
                name = "vysnios";
                break;
            case 5:
                name = "bananai";
                break;
            case 6:
                name = "arbuzai";
                break;





        }



        return name;
    }

    public string NameToRealName(string name)
    {

        return GlobalControl.Instance.currentLangDict[name];
    }
}
