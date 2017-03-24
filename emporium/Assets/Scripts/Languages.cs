using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Languages : MonoBehaviour {

    public static Dictionary<string, string> lithuanian = new Dictionary<string, string>();
    public static Dictionary<string, string> english = new Dictionary<string, string>();

 

    // Use this for initialization
    void Start () {
		
	}
	




    public static void initDicts()
    {

        /////////
        //LITHUANIAN
        /////////

        //MAIN
        lithuanian.Add("login","Prisijungti");
        lithuanian.Add("templog", "laikinas");
        lithuanian.Add("loading", "Jungiamasi...");
       

        //GAMESCENE

        //preso context table
        lithuanian.Add("Press_AssignJobText", "Darbo priskyrimas");
        lithuanian.Add("Press_ProduceTypeText", "Produktas");
        lithuanian.Add("Press_AssignJob_AmountText", "Kiekis");

        //TODO: dropdown



        /////////
        //ENGLISH
        /////////


        //MAIN
        english.Add("login", "Login");
        english.Add("templog", "temporary");
        english.Add("loading", "Loading...");


        //GAMESCENE

        //preso context table
        english.Add("Press_AssignJobText", "Assign Job");
        english.Add("Press_ProduceTypeText", "Produce");
        english.Add("Press_AssignJob_AmountText", "Amount");



    }

    
}

