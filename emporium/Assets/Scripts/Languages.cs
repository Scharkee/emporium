using System.Collections.Generic;
using UnityEngine;

public class Languages : MonoBehaviour
{
    public static Dictionary<string, string> lithuanian = new Dictionary<string, string>();
    public static Dictionary<string, string> english = new Dictionary<string, string>();
    public static bool initiated = false;

    // Use this for initialization
    private void Start()
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

            /////////
            //ENGLISH
            /////////

            //MAIN
            english.Add("login", "Login");
            english.Add("templog", "temporary");
            english.Add("loading", "Loading...");
            english.Add("done_plant_growth", "Ready to Harvest.");
            english.Add("job_unassigned", "Idle.");
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
            english.Add("deze_1", "Storage Box");
            english.Add("statine_1", "Barrel");

            english.Add("mopedas_1", "Moped");
            english.Add("masina_1", "Car");
            english.Add("pikapas_1", "Pickup Truck");
            english.Add("sunkvezimis_1", "Truck");

            //UI
            english.Add("price", "Price");
            english.Add("growth_time", "Growth time");
            english.Add("yield", "Yield");
            english.Add("speed", "Speed");
            english.Add("efic", "Efficiency");
            english.Add("job_time", "Job time");
            english.Add("capacity", "Capacity");
        }
    }
}