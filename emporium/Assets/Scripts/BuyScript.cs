using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using SocketIO;

public class BuyScript : MonoBehaviour
{

    public string tilename;
    RaycastHit hit;
    SocketIOComponent socket;


    // Use this for initialization
    void Start()
    {

        GameObject go = GameObject.Find("SocketIO");
        socket = go.GetComponent<SocketIOComponent>();

    }

    // Update is called once per frame
    void Update()
    {


    }

    public void EnableSelector(string tilename)
    {



        DisabledObjectsGameScene.Instance.BuyMenuPanel.GetComponent<CanvasGroup>().alpha = 0f;




        DisabledObjectsGameScene.Instance.BuyMode.GetComponent<BuyMode>().enabled = true;

        DisabledObjectsGameScene.Instance.BuyMode.GetComponent<BuyMode>().receiveName(tilename);

        DisabledObjectsGameScene.Instance.BuyMenuPanel.SetActive(false);
    }

    public void ChoosePlot(string buildingname, float X, float Z)
    {

        Debug.Log("requesting " + buildingname + " at " + X + " " + Z);



        Dictionary<string, string> data = new Dictionary<string, string>();
        data["Uname"] = GlobalControl.Instance.Uname;

        data["BuildingName"] = buildingname;
        data["X"] = X.ToString();
        data["Z"] = Z.ToString();    //gali buti kad node reikia verst atgal i INTEGER mb idk

        int tileExists = tileExistsAt(X, Z);

        if (tileExists != -9898)//placeholder
        {
            //tile exists
            Debug.Log("551");
            Debug.Log(Database.Instance.ActiveTiles[tileExists].GetComponent<BuildingScript>().thistile.ID);

            if (Database.Instance.ActiveTiles[tileExists].GetComponent<BuildingScript>().thistile.COUNT >= 5)
            {
                Debug.Log("Cant purchase any more.");
                GameAlerts.Instance.AlertWithMessage("Tile cannot host any more trees.");
               
            }
            else if (Database.Instance.ActiveTiles[tileExists].GetComponent<BuildingScript>().thistile.COUNT != 0)
            {
                Debug.Log("551");
                data["TileCount"] = Database.Instance.ActiveTiles[tileExists].GetComponent<BuildingScript>().thistile.COUNT.ToString();
                data["tileID"] = (Database.Instance.ActiveTiles[tileExists].GetComponent<BuildingScript>().thistile.ID).ToString();
                socket.Emit("BUY_TILE", new JSONObject(data));
                Debug.Log("2");
                Debug.Log("551");

            }
        }
        else //tile does not exist.
        {
            Debug.Log("3");
            data["TileCount"] = 1.ToString();
            socket.Emit("BUY_TILE", new JSONObject(data));
        }





    }

    private int tileExistsAt(float X, float Z)
    {
        int currentDBpos = -9898;





        foreach (GameObject tile in Database.Instance.ActiveTiles)
        {

            try
            {
                
                if (tile.transform.position.x == X && tile.transform.position.z == Z)
                {

                    currentDBpos = Database.Instance.ActiveTiles.IndexOf(tile);

                }

            }
            catch
            {

            }


        }

        Debug.Log("tile at Activetiles index " + currentDBpos);

        return currentDBpos;

    }
}
