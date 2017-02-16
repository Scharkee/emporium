using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;


public class Database : MonoBehaviour
{
    public static string UserUsername;
    public static float UserDollars;
    public static int UserLastOnline;
    public static int UserPlotSize;





    //MAIN DATBASES
    public static Tile[] tile;
    public static Building[] buildinginfo;
    public static Inventory[] inventory;




    //BUILDING-UPGRADE-FRUIT INFORMATION
    //augalai
    
/*
    public static Building apelsinas_1 = new Building();
    public static Building obelis_1 = new Building();
    public static Building krause_1 = new Building();
    public static Building slyva_1 = new Building();
    public static Building vysnia_1 = new Building();
    public static Building aloe_1 = new Building();
    public static Building kokosas_1 = new Building();
    public static Building vynuoge_1 = new Building();
    public static Building granatas_1 = new Building();
    public static Building citrina_1 = new Building();
    public static Building melionas_1 = new Building();
    public static Building papajus_1 = new Building();
    public static Building ananasas_1 = new Building();
    public static Building avietes_1 = new Building();
    public static Building braskes_1 = new Building();
    public static Building morka_1 = new Building();

*/

    //irenginiai

    public static Building press_1 = new Building();
    public static Building press_2 = new Building();
    public static Building press_3 = new Building();
    public static Building tank_1 = new Building();
    public static Building tank_2 = new Building();
    public static Building tank_3 = new Building();
    public static Building sterilizuotojas_1 = new Building();
    public static Building sterilizuotojas_2 = new Building();
    public static Building buteliavimas_1 = new Building();
    public static Building buteliavimas_2 = new Building();


    //pastatai

    public static Building kioskas = new Building();
    public static Building dviratis = new Building();
    public static Building mopedas = new Building();
    public static Building motociklas = new Building();
    public static Building masina_1 = new Building();
    public static Building masina_2 = new Building();
    public static Building pickup_truck = new Building();
    public static Building sukvezimis = new Building();
    public static Building sunkvezimis_2 = new Building();






    // Use this for initialization
    void Start()
    {



    }

    // Update is called once per frame
    void Update()
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

}

[System.Serializable]
public class Building
{
    public string NAME = "";
    public float PRICE;
    public int PROG_AMOUNT;
    public string TILEPRODUCENAME;


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
    public float obuoliai;
    public float vysnios;
    public float slyvos;
    public float bananai;
    public float arbuzai;
    public float kriauses;


}









