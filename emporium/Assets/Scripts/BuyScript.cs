using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using SocketIO;

public class BuyScript : MonoBehaviour {

    public static string tilename;
    RaycastHit hit;
    SocketIOComponent socket;


    // Use this for initialization
    void Start () {

        GameObject go = GameObject.Find("SocketIO");
        socket = go.GetComponent<SocketIOComponent>();

    }
	
	// Update is called once per frame
	void Update () {


    }

    public void EnableSelector(string tilename) {


        GameObject menupanel = GameObject.Find("BuyMenuPanel");
        menupanel.GetComponent<CanvasGroup>().alpha = 0f;
        
       


        

        GameObject.Find("Selector").GetComponent<BuyMode>().enabled = true;

        GameObject.Find("Selector").GetComponent<BuyMode>().receiveName(tilename, menupanel);
        menupanel.SetActive(false);
    }

    public void ChoosePlot(string buildingname, float X, float Z)
    {

        Debug.Log("trying to build "+buildingname+" at "+X+" "+Z);

        

        Dictionary<string, string> data = new Dictionary<string, string>();
        data["Uname"] = GlobalControl.Uname;

        data["BuildingName"] = buildingname;
        data["X"] = X.ToString();
        data["Z"] = Z.ToString();    //gali buti kad node reikia verst atgal i INTEGER mb idk



        socket.Emit("BUY_TILE", new JSONObject(data));

    }


}
