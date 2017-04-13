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





    //MAIN DATBASES
    public Tile[] tile;
    public Building[] buildinginfo;
    public Inventory[] inventory; //one temp invo for JSON conversion
    public Dictionary<string, float> Inventory;  // main inventory


    public List<GameObject> ActiveTiles = new List<GameObject>(); //tiles su kuriom daromos operacijos main update cikle.


    void Awake()
    {
        Instance = this;


    }


    //BUILDING-UPGRADE-FRUIT INFORMATION
    //augalai

    /*
        public Building apelsinas_1 = new Building();
        public Building obelis_1 = new Building();
        public Building krause_1 = new Building();
        public Building slyva_1 = new Building();
        public Building vysnia_1 = new Building();
        public Building aloe_1 = new Building();
        public Building kokosas_1 = new Building();
        public Building vynuoge_1 = new Building();
        public Building granatas_1 = new Building();
        public Building citrina_1 = new Building();
        public Building melionas_1 = new Building();
        public Building papajus_1 = new Building();
        public Building ananasas_1 = new Building();
        public Building avietes_1 = new Building();
        public Building braskes_1 = new Building();
        public Building morka_1 = new Building();

    */

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






    // Use this for initialization
    void Start()
    {



    }

 

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
{


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










