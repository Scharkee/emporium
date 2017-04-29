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

        Debug.Log("trying to build " + buildingname + " at " + X + " " + Z);



        Dictionary<string, string> data = new Dictionary<string, string>();
        data["Uname"] = GlobalControl.Instance.Uname;

        data["BuildingName"] = buildingname;
        data["X"] = X.ToString();
        data["Z"] = Z.ToString();    //gali buti kad node reikia verst atgal i INTEGER mb idk

        int tileExists = tileExistsAt(X, Z);

        if (tileExists != -9898)//placeholder
        {
            //tile exists

            if (Database.Instance.tile[tileExists].COUNT >= 5)
            {
                Debug.Log("Cant purchase any more.");
                GameAlerts.Instance.AlertWithMessage("Tile cannot host any more trees.");
            }
            else if (Database.Instance.tile[tileExists].COUNT != 0)
            {
                data["TileCount"] = Database.Instance.tile[tileExists].COUNT.ToString();
                data["tileID"] = (Database.Instance.tile[tileExists].ID).ToString();
                socket.Emit("BUY_TILE", new JSONObject(data));
                Debug.Log(Database.Instance.tile[tileExists].ID);
            }
        }
        else //tile does not exist.
        {
            data["TileCount"] = 1.ToString();
            socket.Emit("BUY_TILE", new JSONObject(data));
        }



        

    }

    private int tileExistsAt(float X, float Z)
    {
        int currentDBpos = -1;
        float x;

        Debug.Log("checking for tiles at " + X + " " + Z);
        do
        {
            currentDBpos++;
            try
            {
                x = Database.Instance.tile[currentDBpos].X;

            }
            catch
            {
                Debug.Log("caught a thing");
                currentDBpos = -9898;

                break;
            }

        } while (Database.Instance.tile[currentDBpos].X != X && Database.Instance.tile[currentDBpos].Z != Z);

        Debug.Log("return code " + currentDBpos);

        return currentDBpos;

    }
}
