using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;


public class Database : MonoBehaviour
{
    public string UserUsername;
    public float UserDollars;
    public int UserLastOnline;
    public int UserPlotSize;

    public static Database Instance;





    //TRANSPORT
    public Transport CurrentVehichle;

    //STOGRAGE

    public Storage Storage;

    //MAIN DATBASES
    public Tile[] tile;
    public Building[] buildinginfo;
    public Inventory[] inventory; //one temp invo for JSON conversion
    public Prices[] prices;
    public Prices[] oldPrices;
    public Dictionary<string, float> Inventory;  // main inventory
    public Dictionary<string, float> Prices;  // main inventory
    public Dictionary<string, float> Oldprices;  // main inventory

    public List<GameObject> ActiveTiles = new List<GameObject>(); //tiles su kuriom daromos operacijos main update cikle.
    public int TileSelfSignedAssignmentComplete = 0;

    public List<BuildingScript> ActiveProduceStorage = new List<BuildingScript>(); //tiles kurios skirtos laikyti vaisius/darzoves (PRODUCE)
    public List<BuildingScript> ActiveJuiceStorage = new List<BuildingScript>(); //tiles kurios skirtos laikyti sultis (JUICE)


    void Awake()
    {
        Instance = this;


    }
    //irenginiai

    public Building press_1 = new Building();
    public Building press_2 = new Building();
    public Building press_3 = new Building();
    public Building tank_1 = new Building();
    public Building tank_2 = new Building();
    public Building tank_3 = new Building();
    public Building sterilizuotojas_1 = new Building();
    public Building sterilizuotojas_2 = new Building();
    public Building buteliavimas_1 = new Building();
    public Building buteliavimas_2 = new Building();


    //pastatai

    public Building kioskas = new Building();
    public Building dviratis = new Building();
    public Building mopedas = new Building();
    public Building motociklas = new Building();
    public Building masina_1 = new Building();
    public Building masina_2 = new Building();
    public Building pickup_truck = new Building();
    public Building sukvezimis = new Building();
    public Building sunkvezimis_2 = new Building();

    public IEnumerator waitForTileAssignmentCompletion()
    {


        while (TileSelfSignedAssignmentComplete != ActiveTiles.Count)
        {
            yield return new WaitForSeconds(0.1f);

        }

        //visos tiles baige assignintis

        DisabledObjectsGameScene.Instance.managerialScripts.GetComponent<TileOperator>().StartGrowthCompletrionCheckRepeat();

        InvAmounts am = amountsInInventory();
        Storage.TakenJuiceStorage = am.CURJuiceAmount;
        Storage.TakenProduceStorage = am.CURProduceAmount;

        Debug.Log("storage yra " + Storage.TakenJuiceStorage+" sulciu ir "+ Storage.TakenProduceStorage + " produce");

        //TODO: parodyt on Header in HUD kartu su total storage amount

    }

    private InvAmounts amountsInInventory()
    {
        InvAmounts amount;
        amount.CURJuiceAmount = 0; amount.CURProduceAmount = 0;

        foreach (KeyValuePair<string, float> produktas in Inventory)
        {
            if (produktas.Key.Contains("_sultys")) // sudedam visu sulciu suma
            {
                amount.CURJuiceAmount += produktas.Value;
            }else
            {
                amount.CURProduceAmount += produktas.Value;
            }

        }


        return amount;

    }



}

public struct InvAmounts
{
    //add daugiau jei sugalvosiu kitokiu produce types
    public float CURJuiceAmount;
    public float CURProduceAmount;


}

[System.Serializable]
public class Tile
{
    public int ID;
    public string NAME;
    public int START_OF_GROWTH;
    public float X;
    public float Z;
    public float FERTILISED_UNTIL;
    public int COUNT;
    public int BUILDING_CURRENT_WORK_AMOUNT;
    public string WORK_NAME;
}

[System.Serializable]
public class Building
{
    public string NAME = "";
    public float PRICE;
    public int PROG_AMOUNT;
    public string TILEPRODUCENAME;
    public float TILEPRODUCERANGE_1; //TODO: pazet ar tinkami pavadinimai cia
    public float TILEPRODUCERANGE_2;
    public int BUILDING_TYPE;
    public int SINGLE_USE;

}

[System.Serializable]
public class BuyButtons
{
    public string NAME;
}


[System.Serializable]
public class Inventory
{//EXPNEWTREES


    public float apelsinai;
    public float apelsinai_sultys;
    public float obuoliai;
    public float obuoliai_sultys;
    public float vysnios;
    public float vysnios_sultys;
    public float slyvos;
    public float slyvos_sultys;
    public float bananai;
    public float bananai_sultys;
    public float arbuzai;
    public float arbuzai_sultys;
    public float kriauses;
    public float kriauses_sultys;
    public float nektarinai_sultys;
    public float nektarinai;
    public float kiviai;
    public float kiviai_sultys;
    public float persikai;
    public float persikai_sultys;


}


[System.Serializable]
public class Prices
{//EXPNEWTREES

    public int ID;
    public string NAME;
    public float PRICE;

}


[System.Serializable]
public class Storage
{//EXPNEWTREES

    public float TotalProduceStorage;
    public float TotalJuiceStorage;
    public float TakenProduceStorage;
    public float TakenJuiceStorage;

}

[System.Serializable]
public class Transport
{//EXPNEWTREES
    public string Name;
    public int IDinDB;
    public int IDinActiveTiles;
    public float amount;
    public float time;
}













