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

    private void Awake()
    {
        initDicts();
    }

    public static void initDicts()
    {
        if (!initiated) //TODO: make sure kad initiated Main scenoje, kad veiktu locals
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
            english.Add("lempa_1", "Lamp");

            english.Add("mopedas_1", "Moped");
            english.Add("masina_1", "Car");
            english.Add("pikapas_1", "Pickup Truck");
            english.Add("sunkvezimis_1", "Truck");
            english.Add("", "None");

            //UI
            english.Add("price", "Price");
            english.Add("growth_time", "Growth time");
            english.Add("yield", "Yield");
            english.Add("speed", "Speed");
            english.Add("efic", "Efficiency");
            english.Add("job_time", "Job time");
            english.Add("capacity", "Capacity");
            english.Add("password_passable", "Passable");
            english.Add("password_weak", "Strong");
            english.Add("password_strong", "Too Weak");
            english.Add("banned_message", "You are banned. Please try again later.");
            english.Add("already_logged_in_message", "You are already logged in from an another location.");
            english.Add("enter_username", "Enter your username:");
            english.Add("enter_password", "Enter your password:");
            english.Add("wrong_password", "Wrong Password.");
            english.Add("bug_report_success", "Thank You! Your bug report has been submitted.");
            english.Add("not_enough_solid_storage_space", "Not enough storage space!");
            english.Add("status_busy", "Status: busy");
            english.Add("status_idle", "Status: idle");
            english.Add("tile_full", "Tile cannot host any more trees.");
            english.Add("tile_progress", "Progress: ");
            english.Add("tile_assigned", "Assigned: ");
            english.Add("tile_capacity", "Capacity: ");
            english.Add("tile_status", "Status: ");
            english.Add("tile_expected_yield", "Expected yield:");
            english.Add("vehicle_current", "Current vehicle: ");
            english.Add("password_reset_email_sent", "Password reset email sent!");
            english.Add("email_too_short", "Too Short!");
            english.Add("transport_cannot_support_weight", "Your current transport cannot support this amount of weight!");
            english.Add("plotsize_upgraded", "You have upgraded your plot size! Please log in again.");

            english.Add("desync_detected", "Desynchronization detected. Logging off...");
            english.Add("not_enough_money", "Not enough money!");
            english.Add("sssssss", "ssssssss");

            english.Add("sssssss", "ssssssss");

            //PRODUCE
            english.Add("kriauses", "Pears");
            english.Add("obuoliai", "Apples");
            english.Add("apelsinai", "Oranges");
            english.Add("persikai", "Peaches");
            english.Add("nektarinai", "Nectarines");
            english.Add("kiviai", "Kiwis");
            english.Add("slyvos", "Plums");
            english.Add("bananai", "Bananas");
            english.Add("arbuzai", "Watermelons");
            english.Add("vysnios", "Cherries");

            //JUICE
            english.Add("kriauses_sultys", "Pear Juice");
            english.Add("obuoliai_sultys", "Apple Juice");
            english.Add("apelsinai_sultys", "Orange Juice");
            english.Add("persikai_sultys", "Peach Juice");
            english.Add("nektarinai_sultys", "Nectarine Juice");
            english.Add("kiviai_sultys", "Kiwi Juice");
            english.Add("slyvos_sultys", "Plum Juice");
            english.Add("bananai_sultys", "Banana Juice");
            english.Add("arbuzai_sultys", "Watermelon Juice");
            english.Add("vysnios_sultys", "Cherry Juice");
        }
    }
}