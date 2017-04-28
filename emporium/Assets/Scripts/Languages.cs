using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Languages : MonoBehaviour
{

    public static Dictionary<string, string> lithuanian = new Dictionary<string, string>();
    public static Dictionary<string, string> english = new Dictionary<string, string>();
    public static bool initiated = false;


    // Use this for initialization
    void Start()
    {

    }







    public static void initDicts()
    {
        if (!initiated)
        {

            initiated = true;
            /////////
            //LITHUANIAN
            /////////

            //MAIN
            lithuanian.Add("login", "Prisijungti");
            lithuanian.Add("templog", "laikinas");
            lithuanian.Add("loading", "Jungiamasi...");
            lithuanian.Add("done_plant_growth", "Derlius paruoštas.");
            lithuanian.Add("done_collect", "Paruošta surinkimui.");
            //GAMESCENE

            //preso context table
            lithuanian.Add("Press_AssignJobText", "Darbo priskyrimas");
            lithuanian.Add("Press_ProduceTypeText", "Produktas");
            lithuanian.Add("Press_AssignJob_AmountText", "Kiekis");


            //TODO: dropdown

            //NAMES

            lithuanian.Add("kriause_1", "Kriaušė");
            lithuanian.Add("kivis_1", "Kivių medis");
            lithuanian.Add("apelsinas_1", "Apelsinmedis");
            lithuanian.Add("nektarinas_1", "Nektarinų medis");
            lithuanian.Add("obuolys_1", "Obelis");
            lithuanian.Add("persikas_1", "Persikų medis");
            lithuanian.Add("slyva_1", "Slyva");

            lithuanian.Add("presas_1", "Maža sulčiaspaudė");




            /////////
            //ENGLISH
            /////////


            //MAIN
            english.Add("login", "Login");
            english.Add("templog", "temporary");
            english.Add("loading", "Loading...");
            english.Add("done_plant_growth", "Ready to Harvest.");
            english.Add("done_collect", "Ready to Collect.");


            //GAMESCENE

            //preso context table
            english.Add("Press_AssignJobText", "Assign Job");
            english.Add("Press_ProduceTypeText", "Produce");
            english.Add("Press_AssignJob_AmountText", "Amount");


            //NAMES

            english.Add("kriause_1", "Pear tree");
            english.Add("kivis_1", "Kiwi tree");
            english.Add("apelsinas_1", "Orange tree");
            english.Add("nektarinas_1", "Nectarine tree");
            english.Add("obuolys_1", "Apple tree");
            english.Add("persikas_1", "Peach tree");
            english.Add("slyva_1", "Plum tree");
            english.Add("vysnia_1", "Cherry tree");
            english.Add("presas_1", "Small juicer");


        }


    }


}

