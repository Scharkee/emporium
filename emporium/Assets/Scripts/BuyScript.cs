using SocketIO;
using System.Collections.Generic;
using UnityEngine;

public class BuyScript : MonoBehaviour
{
    public string tilename;
    private RaycastHit hit;
    private SocketIOComponent socket;

    // Use this for initialization
    private void Start()
    {
        socket = DisabledObjectsGameScene.Instance.socket;
    }

    private void Update()
    {
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

            Debug.Log(Database.Instance.ActiveTiles[tileExists].GetComponent<BuildingScript>().thistile.ID);

            if (Database.Instance.ActiveTiles[tileExists].GetComponent<BuildingScript>().thistile.COUNT >= 5)
            { //TODO: pridet building types sitam shazamui + server sided too, gal statines gali ~10 stacks turet
                Debug.Log("Cant purchase any more; tile full");
                GameAlerts.Instance.AlertWithMessage(GlobalControl.Instance.currentLangDict["tile_full"]);
            }
            else if (Database.Instance.ActiveTiles[tileExists].GetComponent<BuildingScript>().thistile.COUNT != 0)
            {
                if (Database.Instance.ActiveTiles[tileExists].GetComponent<BuildingScript>().thistileInfo.BUILDING_TYPE == 0)//augalas, praleidziam
                {
                    data["TileCount"] = Database.Instance.ActiveTiles[tileExists].GetComponent<BuildingScript>().thistile.COUNT.ToString();
                    data["tileID"] = (Database.Instance.ActiveTiles[tileExists].GetComponent<BuildingScript>().thistile.ID).ToString();
                    socket.Emit("BUY_TILE", new JSONObject(data));
                }
                else if (Database.Instance.ActiveTiles[tileExists].GetComponent<BuildingScript>().thistileInfo.BUILDING_TYPE == 1) //presas, stabdom
                {
                    Debug.Log("Discrepancy. Presu negalima stackinti");
                    DisabledObjectsGameScene.Instance.SocketManager.DiscrepancyAction();
                }
                else if (Database.Instance.ActiveTiles[tileExists].GetComponent<BuildingScript>().thistileInfo.BUILDING_TYPE == 2) //transportas, currently unstackable
                {
                    Debug.Log("Discrepancy. Presu negalima stackinti");
                    DisabledObjectsGameScene.Instance.SocketManager.DiscrepancyAction();
                }
                else if (Database.Instance.ActiveTiles[tileExists].GetComponent<BuildingScript>().thistileInfo.BUILDING_TYPE == 3) //solid storage, praleidziam
                {
                    data["TileCount"] = Database.Instance.ActiveTiles[tileExists].GetComponent<BuildingScript>().thistile.COUNT.ToString();
                    data["tileID"] = (Database.Instance.ActiveTiles[tileExists].GetComponent<BuildingScript>().thistile.ID).ToString();
                    socket.Emit("BUY_TILE", new JSONObject(data));
                }
                else if (Database.Instance.ActiveTiles[tileExists].GetComponent<BuildingScript>().thistileInfo.BUILDING_TYPE == 4) //liquid storage, praleidziam
                {
                    data["TileCount"] = Database.Instance.ActiveTiles[tileExists].GetComponent<BuildingScript>().thistile.COUNT.ToString();
                    data["tileID"] = (Database.Instance.ActiveTiles[tileExists].GetComponent<BuildingScript>().thistile.ID).ToString();
                    socket.Emit("BUY_TILE", new JSONObject(data));
                }
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

        return currentDBpos;
    }
}