using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Database : MonoBehaviour
{
    public string UserUsername;
    public float UserDollars;
    public int UserLastOnline;
    public int UserPlotSize;

    public static Database Instance;

    //TRANSPORT
    public Transport CurrentVehichle = null;

    //STOGRAGE

    public Storage Storage;

    //MAIN DATBASES
    public Tile[] tile;

    public TransportJob[] TransportJobs;
    public List<TransportJob> TransportJobList = new List<TransportJob>();

    public Worker[] ReceivedWorkers;
    public Worker[] ReceivedAvailableWorkers;
    public List<Worker> HiredWorkerList = new List<Worker>();
    public List<Worker> AvailableWorkerList = new List<Worker>();

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

    private void Awake()
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
        Debug.Log("tiles done assigning");

        //visos tiles baige assignintis

        if (CurrentVehichle == null) //nera transporto tile
        {
            CurrentVehichle.amount = 30;
            CurrentVehichle.time = 600;
            CurrentVehichle.Name = "none";
            CurrentVehichle.IDinDB = 0;
            CurrentVehichle.ID = 0;
            CurrentVehichle.IDinActiveTiles = 0;
            CurrentVehichle.status = "";
        }
        else
        {
            Debug.Log("Looking for " + CurrentVehichle.Name);
            DisabledObjectsGameScene.Instance.TransportName.text = "Current vehicle: " + GlobalControl.Instance.currentLangDict[CurrentVehichle.Name];
        }

        DisabledObjectsGameScene.Instance.managerialScripts.GetComponent<TileOperator>().StartGrowthCompletrionCheckRepeat();

        InvAmounts am = amountsInInventory();
        Storage.TakenJuiceStorage = am.CURJuiceAmount;
        Storage.TakenProduceStorage = am.CURProduceAmount;

        DisabledObjectsGameScene.Instance.JuicetorageEdit.text = Storage.TakenJuiceStorage + "/" + Storage.TotalJuiceStorage + " L";
        DisabledObjectsGameScene.Instance.ProduceStorageEdit.text = Storage.TakenProduceStorage + "/" + Storage.TotalProduceStorage + " KG";

        if (Storage.TakenJuiceStorage >= Storage.TotalJuiceStorage)
        {
            DisabledObjectsGameScene.Instance.JuicetorageEdit.color = Globals.Instance.RedTextColor;
        }
        if (Storage.TakenProduceStorage >= Storage.TotalProduceStorage)
        {
            DisabledObjectsGameScene.Instance.ProduceStorageEdit.color = Globals.Instance.RedTextColor;
        }
    }

    public void AddToStoredAmounts(float amount, int solidOrJuice)
    {
        switch (solidOrJuice)
        {
            case 0: //solid
                Interlocked.Exchange(ref Storage.TakenProduceStorage, Storage.TakenProduceStorage + amount);
                DisabledObjectsGameScene.Instance.ProduceStorageEdit.text = Storage.TakenProduceStorage + "/" + Storage.TotalProduceStorage;

                if (Storage.TakenProduceStorage >= Storage.TotalProduceStorage)
                {
                    DisabledObjectsGameScene.Instance.ProduceStorageEdit.color = Globals.Instance.RedTextColor;
                }
                else
                {
                    DisabledObjectsGameScene.Instance.ProduceStorageEdit.color = Globals.Instance.WhiteTextColor;
                }
                break;

            case 1: //juice

                Interlocked.Exchange(ref Storage.TakenJuiceStorage, Storage.TakenJuiceStorage + amount);
                DisabledObjectsGameScene.Instance.JuicetorageEdit.text = Storage.TakenJuiceStorage + "/" + Storage.TotalJuiceStorage;

                if (Storage.TakenJuiceStorage >= Storage.TotalJuiceStorage)
                {
                    DisabledObjectsGameScene.Instance.JuicetorageEdit.color = Globals.Instance.RedTextColor;
                }
                else
                {
                    DisabledObjectsGameScene.Instance.JuicetorageEdit.color = Globals.Instance.WhiteTextColor;
                }

                break;
        }
    }

    public void RemoveFromStoredAmounts(float amount, int solidOrJuice)
    {
        switch (solidOrJuice)
        {
            case 0: //solid
                Interlocked.Exchange(ref Storage.TakenProduceStorage, Storage.TakenProduceStorage - amount);
                DisabledObjectsGameScene.Instance.ProduceStorageEdit.text = Storage.TakenProduceStorage + "/" + Storage.TotalProduceStorage;

                if (Storage.TakenProduceStorage >= Storage.TotalProduceStorage)
                {
                    DisabledObjectsGameScene.Instance.ProduceStorageEdit.color = Globals.Instance.RedTextColor;
                }
                else
                {
                    DisabledObjectsGameScene.Instance.ProduceStorageEdit.color = Globals.Instance.WhiteTextColor;
                }
                break;

            case 1: //juice

                Interlocked.Exchange(ref Storage.TakenJuiceStorage, Storage.TakenJuiceStorage - amount);
                DisabledObjectsGameScene.Instance.JuicetorageEdit.text = Storage.TakenJuiceStorage + "/" + Storage.TotalJuiceStorage;

                if (Storage.TakenJuiceStorage >= Storage.TotalJuiceStorage)
                {
                    DisabledObjectsGameScene.Instance.JuicetorageEdit.color = Globals.Instance.RedTextColor;
                }
                else
                {
                    DisabledObjectsGameScene.Instance.JuicetorageEdit.color = Globals.Instance.WhiteTextColor;
                }

                break;
        }
    }

    public void AddToMaxStorageAmounts(float amount, int solidOrJuice)
    {
        switch (solidOrJuice)
        {
            case 0: //solid
                Interlocked.Exchange(ref Storage.TotalProduceStorage, Storage.TotalProduceStorage + amount);
                DisabledObjectsGameScene.Instance.ProduceStorageEdit.text = Storage.TakenProduceStorage + "/" + Storage.TotalProduceStorage;

                if (Storage.TakenProduceStorage >= Storage.TotalProduceStorage)
                {
                    DisabledObjectsGameScene.Instance.ProduceStorageEdit.color = Globals.Instance.RedTextColor;
                }
                else
                {
                    DisabledObjectsGameScene.Instance.ProduceStorageEdit.color = Globals.Instance.WhiteTextColor;
                }
                break;

            case 1: //juice

                Interlocked.Exchange(ref Storage.TotalJuiceStorage, Storage.TotalJuiceStorage + amount);
                DisabledObjectsGameScene.Instance.JuicetorageEdit.text = Storage.TakenJuiceStorage + "/" + Storage.TotalJuiceStorage;

                if (Storage.TakenJuiceStorage >= Storage.TotalJuiceStorage)
                {
                    DisabledObjectsGameScene.Instance.JuicetorageEdit.color = Globals.Instance.RedTextColor;
                }
                else
                {
                    DisabledObjectsGameScene.Instance.JuicetorageEdit.color = Globals.Instance.WhiteTextColor;
                }

                break;
        }
    }

    public void RemoveFromMaxStorageAmounts(float amount, int solidOrJuice)
    {
        switch (solidOrJuice)
        {
            case 0: //solid
                Interlocked.Exchange(ref Storage.TotalProduceStorage, Storage.TotalProduceStorage - amount);
                DisabledObjectsGameScene.Instance.ProduceStorageEdit.text = Storage.TakenProduceStorage + "/" + Storage.TotalProduceStorage;

                if (Storage.TakenProduceStorage >= Storage.TotalProduceStorage)
                {
                    DisabledObjectsGameScene.Instance.ProduceStorageEdit.color = Globals.Instance.RedTextColor;
                }
                else
                {
                    DisabledObjectsGameScene.Instance.ProduceStorageEdit.color = Globals.Instance.WhiteTextColor;
                }
                break;

            case 1: //juice

                Interlocked.Exchange(ref Storage.TotalJuiceStorage, Storage.TotalJuiceStorage - amount);
                DisabledObjectsGameScene.Instance.JuicetorageEdit.text = Storage.TakenJuiceStorage + "/" + Storage.TotalJuiceStorage;

                if (Storage.TakenJuiceStorage >= Storage.TotalJuiceStorage)
                {
                    DisabledObjectsGameScene.Instance.JuicetorageEdit.color = Globals.Instance.RedTextColor;
                }
                else
                {
                    DisabledObjectsGameScene.Instance.JuicetorageEdit.color = Globals.Instance.WhiteTextColor;
                }

                break;
        }
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
            }
            else
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
    public float EFIC;
    public int PROG_AMOUNT;
    public string TILEPRODUCENAME;
    public float TILEPRODUCERANDOM1;
    public float TILEPRODUCERANDOM2;
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
{
    public int ID;
    public string NAME;
    public float PRICE;
}

[System.Serializable]
public class Storage
{
    public float TotalProduceStorage = 0;
    public float TotalJuiceStorage = 0;
    public float TakenProduceStorage = 0;
    public float TakenJuiceStorage = 0;
}

[System.Serializable]
public class Transport
{
    public string Name;
    public int IDinDB;
    public int ID;
    public int count;
    public int IDinActiveTiles;
    public float amount;
    public float time;
    public string status;
}

[System.Serializable]
public class TransportJob
{
    public int ID;
    public string DEST;
    public int START_OF_TRANSPORTATION;
    public int LENGTH_OF_TRANSPORTATION;
    public bool AskedForVerif = false;
}

[System.Serializable]
public class Worker
{
    public int ID;
    public int ASSIGNEDTILEID;
    public int SPEED;
}