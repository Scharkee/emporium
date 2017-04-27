using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;
using System.Text;


public class AssignTiles : MonoBehaviour {


    UIManager uiManager;
    SocketManager socman;


    // Use this for initialization
    void Start () {


        GameObject managerial = GameObject.Find("_ManagerialScripts");
       
        uiManager = managerial.GetComponent<UIManager>();

        socman= managerial.GetComponent<SocketManager>();


    }
	
	// Update is called once per frame
	



    public void AssignReceivedTiles(SocketIOEvent evt)
    {
  

        string evtStringRows = evt.data.ToString();


        StringBuilder builder = new StringBuilder(evtStringRows);///////////////////////////////////////////////////////
        builder.Replace("rows", "Items");/////////////////////////JSON string paruosiamas konvertavimui i class object array.
        string evtStringItems = builder.ToString(); //////////////////////////////////////////////////////////////////////
      

        Database.Instance.tile = JsonHelper.FromJson<Tile>(evtStringItems);//converting & assignment



        PlotSelector plotsel = GameObject.Find("_GameScripts").GetComponent<PlotSelector>();
        plotsel.SpawnPlotSelectors();

        SpawnTiles();

    }


    public void AssignTileInformation(SocketIOEvent evt)
    {


        string evtStringRows = evt.data.ToString();


        StringBuilder builder = new StringBuilder(evtStringRows);///////////////////////////////////////////////////////
        builder.Replace("rows", "Items");/////////////////////////JSON string paruosiamas konvertavimui i class object array.
        string evtStringItems = builder.ToString(); //////////////////////////////////////////////////////////////////////


        Database.Instance.buildinginfo = JsonHelper.FromJson<Building>(evtStringItems);//converting & assignment

        BuyMenuInfoLoader.Instance.LoadBuyMenuInfo();





    }

    public void AssignInventory(SocketIOEvent evt)
    {


        string evtStringRows = evt.data.ToString();


        StringBuilder builder = new StringBuilder(evtStringRows);///////////////////////////////////////////////////////
        builder.Replace("rows", "Items");/////////////////////////JSON string paruosiamas konvertavimui i class object array.
        string evtStringItems = builder.ToString(); //////////////////////////////////////////////////////////////////////


        Database.Instance.inventory = JsonHelper.FromJson<Inventory>(evtStringItems);// assignment


        Database.Instance.Inventory = HelperScripts.Instance.ReassignInventory(Database.Instance.inventory[0]);


        
     



    }

    public void SpawnTiles()
    {
        //sita funkcija atsakinga uz visu tile spawninima zaidimo pradzioje.
        int i = 0;
        //TODO: randomized scales, switch (galbut), atskirts spawninima i atskira funkc.


        while (i < Database.Instance.tile.Length)
        {
            GameObject currentTile = new GameObject();
            //uzkraunami prefabs
            GameObject currentAdditionalPrefab = Resources.Load("Plants/additionalBuildings/" + Database.Instance.tile[i].NAME) as GameObject;
            GameObject currentTilePrefab = Resources.Load("Plants/" + Database.Instance.tile[i].NAME) as GameObject;
            float xRot = 0;

            //gal but pakeist i switcha, randomizint vosvos scale visu (kad nebutu absolutely identical.                                                                                                                    )

            if (Database.Instance.tile[i].COUNT == 1)
            {
                currentTile = (Instantiate(currentTilePrefab, new Vector3(Database.Instance.tile[i].X, 0f, Database.Instance.tile[i].Z), Quaternion.Euler(new Vector3(xRot, Random.Range(-350.0f, 350.0f), 0)), GameObject.Find("Tiles").transform) as GameObject).gameObject;
                currentTile.GetComponent<BuildingScript>().thistile = Database.Instance.tile[i];
                currentTile.GetComponent<BuildingScript>().idInTileDatabase = i;

            }else if(Database.Instance.tile[i].COUNT == 2)
            {

                currentTile = (Instantiate(currentTilePrefab, new Vector3(Database.Instance.tile[i].X-0.3f, 0f, Database.Instance.tile[i].Z), Quaternion.Euler(new Vector3(xRot, Random.Range(-350.0f, 350.0f), 0)), GameObject.Find("Tiles").transform) as GameObject).gameObject;
                currentTile.GetComponent<BuildingScript>().thistile = Database.Instance.tile[i];
                currentTile.GetComponent<BuildingScript>().idInTileDatabase = i;

                currentAdditionalPrefab = Resources.Load("Plants/additionalBuildings/" + Database.Instance.tile[i].NAME) as GameObject;
                GameObject currentAdditional = (Instantiate(currentAdditionalPrefab, new Vector3(Database.Instance.tile[i].X + 3f, 0f, Database.Instance.tile[i].Z), Quaternion.Euler(new Vector3(xRot, Random.Range(-350.0f, 350.0f), 0)), currentTile.transform) as GameObject).gameObject;



            }
            else if (Database.Instance.tile[i].COUNT == 3)
            {

                currentTile = (Instantiate(currentTilePrefab, new Vector3(Database.Instance.tile[i].X + 0.3f, 0f, Database.Instance.tile[i].Z + 0.3f), Quaternion.Euler(new Vector3(xRot, Random.Range(-350.0f, 350.0f), 0)), GameObject.Find("Tiles").transform) as GameObject).gameObject;
                currentTile.GetComponent<BuildingScript>().thistile = Database.Instance.tile[i];
                currentTile.GetComponent<BuildingScript>().idInTileDatabase = i;

                currentAdditionalPrefab = Resources.Load("Plants/additionalBuildings/" + Database.Instance.tile[i].NAME) as GameObject;
                GameObject currentAdditional = (Instantiate(currentAdditionalPrefab, new Vector3(Database.Instance.tile[i].X -0.25f, 0f, Database.Instance.tile[i].Z), Quaternion.Euler(new Vector3(xRot, Random.Range(-350.0f, 350.0f), 0)), currentTile.transform) as GameObject).gameObject;
                GameObject currentAdditional1 = (Instantiate(currentAdditionalPrefab, new Vector3(Database.Instance.tile[i].X + 0.3f, 0f, Database.Instance.tile[i].Z + 0.3f), Quaternion.Euler(new Vector3(xRot, Random.Range(-350.0f, 350.0f), 0)), currentTile.transform) as GameObject).gameObject;




            }
            else if (Database.Instance.tile[i].COUNT == 4)
            {

                currentTile = (Instantiate(currentTilePrefab, new Vector3(Database.Instance.tile[i].X+0.3f, 0f, Database.Instance.tile[i].Z-0.3f), Quaternion.Euler(new Vector3(xRot, Random.Range(-350.0f, 350.0f), 0)), GameObject.Find("Tiles").transform) as GameObject).gameObject;
                currentTile.GetComponent<BuildingScript>().thistile = Database.Instance.tile[i];
                currentTile.GetComponent<BuildingScript>().idInTileDatabase = i;

               
                GameObject currentAdditional = (Instantiate(currentAdditionalPrefab, new Vector3(Database.Instance.tile[i].X+0.3f, 0f, Database.Instance.tile[i].Z + 0.3f), Quaternion.Euler(new Vector3(xRot, Random.Range(-350.0f, 350.0f), 0)), currentTile.transform) as GameObject).gameObject;
                GameObject currentAdditional1 = (Instantiate(currentAdditionalPrefab, new Vector3(Database.Instance.tile[i].X - 0.3f, 0f, Database.Instance.tile[i].Z + 0.3f), Quaternion.Euler(new Vector3(xRot, Random.Range(-350.0f, 350.0f), 0)), currentTile.transform) as GameObject).gameObject;
                GameObject currentAdditional2 = (Instantiate(currentAdditionalPrefab, new Vector3(Database.Instance.tile[i].X - 0.3f, 0f, Database.Instance.tile[i].Z - 0.3f), Quaternion.Euler(new Vector3(xRot, Random.Range(-350.0f, 350.0f), 0)), currentTile.transform) as GameObject).gameObject;



            }
            else if (Database.Instance.tile[i].COUNT == 5) //TODO: pabaigt su situ
            {

                currentTile = (Instantiate(currentTilePrefab, new Vector3(Database.Instance.tile[i].X, 0f, Database.Instance.tile[i].Z), Quaternion.Euler(new Vector3(xRot, Random.Range(-350.0f, 350.0f), 0)), GameObject.Find("Tiles").transform) as GameObject).gameObject;
                currentTile.GetComponent<BuildingScript>().thistile = Database.Instance.tile[i];
                currentTile.GetComponent<BuildingScript>().idInTileDatabase = i;

                
                GameObject currentAdditional = (Instantiate(currentAdditionalPrefab, new Vector3(Database.Instance.tile[i].X + 0.3f, 0f, Database.Instance.tile[i].Z + 0.3f), Quaternion.Euler(new Vector3(xRot, Random.Range(-350.0f, 350.0f), 0)), currentTile.transform) as GameObject).gameObject;
                GameObject currentAdditional1 = (Instantiate(currentAdditionalPrefab, new Vector3(Database.Instance.tile[i].X - 0.3f, 0f, Database.Instance.tile[i].Z + 0.3f), Quaternion.Euler(new Vector3(xRot, Random.Range(-350.0f, 350.0f), 0)), currentTile.transform) as GameObject).gameObject;
                GameObject currentAdditional2 = (Instantiate(currentAdditionalPrefab, new Vector3(Database.Instance.tile[i].X - 0.3f, 0f, Database.Instance.tile[i].Z - 0.3f), Quaternion.Euler(new Vector3(xRot, Random.Range(-350.0f, 350.0f), 0)), currentTile.transform) as GameObject).gameObject;




            }

            Debug.Log(currentTile.GetComponent<BuildingScript>().thistile.NAME);
           

            Database.Instance.ActiveTiles.Add(currentTile);



            i++;
        }


        GameObject.Find("_ManagerialScripts").GetComponent<TileOperator>().StartGrowthCompletrionCheckRepeat();

        

    }


    public void BuildTile(SocketIOEvent evt)
    {
        Debug.Log("GOT BUILD_TILE FROM SERVER");
        float X = float.Parse(evt.data.GetField("TileX").ToString());
        float Z = float.Parse(evt.data.GetField("TileZ").ToString());
        string tilename = evt.data.GetField("TileName").ToString();
     

        int count = int.Parse(evt.data.GetField("TileCount").ToString());



        if (count == 1) //single plant
        {
            SpawnATile(tilename, X, Z);

        }else if(count == 2) //two plants one tile
        {
            //move tile to the side
            SpawnATile(tilename, X + 0.25f, Z + 0.25f);
            SpawnATile(tilename, X - 0.25f, Z - 0.25f);
        }
        else if(count == 3) //three plants 1 tile
        {


        }



    }


    public void SpawnATile(string tilename, float X, float Z)
    {
        tilename = tilename.Replace("\"", ""); //formatting to get rid of quotation marks
    
        float xRot = 0;

   
       

        GameObject currentTilePrefab = Resources.Load("Plants/" + tilename) as GameObject;
        GameObject currentTile = Instantiate(currentTilePrefab, new Vector3(X, 0f, Z), Quaternion.Euler(new Vector3(xRot, Random.Range(-350.0f, 350.0f), 0)), GameObject.Find("Tiles").transform) as GameObject;
        // currentTile.transform.localScale = new Vector3(Random.Range(0.05f, 0.09f), Random.Range(0.05f, 0.09f), Random.Range(0.05f, 0.09f));  FIXME: ask unity teacher y not workin


        Database.Instance.ActiveTiles.Add(currentTile);




        currentTile.GetComponent<BuildingScript>().thistile.START_OF_GROWTH = socman.unix;

        currentTile.GetComponent<BuildingScript>().thistile.NAME = tilename;
        



    }


}





public static class JsonHelper
{
    public static T[] FromJson<T>(string json)
    {
        Wrapper<T> wrapper = UnityEngine.JsonUtility.FromJson<Wrapper<T>>(json);
        return wrapper.Items;
    }

    public static string ToJson<T>(T[] array)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return UnityEngine.JsonUtility.ToJson(wrapper);
    }

    [System.Serializable]
    private class Wrapper<T>
    {
        public T[] Items;
    }
}
