using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;
using System.Text;


public class AssignTiles : MonoBehaviour {

 


    // Use this for initialization
    void Start () {



       



    }
	
	// Update is called once per frame
	



    public void AssignReceivedTiles(SocketIOEvent evt)
    {
  

        string evtStringRows = evt.data.ToString();
        Debug.Log(evtStringRows);

        StringBuilder builder = new StringBuilder(evtStringRows);///////////////////////////////////////////////////////
        builder.Replace("rows", "Items");/////////////////////////JSON string paruosiamas konvertavimui i class object array.
        string evtStringItems = builder.ToString(); //////////////////////////////////////////////////////////////////////
      

        Database.tile = JsonHelper.FromJson<Tile>(evtStringItems);//converting & assignment



        PlotSelector plotsel = GameObject.Find("_GameScripts").GetComponent<PlotSelector>();
        plotsel.SpawnPlotSelectors();

        SpawnTiles();

    }


    public void AssignTileInformation(SocketIOEvent evt)
    {


        string evtStringRows = evt.data.ToString();
        Debug.Log(evtStringRows);

        StringBuilder builder = new StringBuilder(evtStringRows);///////////////////////////////////////////////////////
        builder.Replace("rows", "Items");/////////////////////////JSON string paruosiamas konvertavimui i class object array.
        string evtStringItems = builder.ToString(); //////////////////////////////////////////////////////////////////////


        Database.buildinginfo = JsonHelper.FromJson<Building>(evtStringItems);//converting & assignment

        Debug.Log(Database.buildinginfo[0].PROG_AMOUNT);


    }

    public void SpawnTiles()
    {
        int i = 0;

        while (i < Database.tile.Length)
        {
            GameObject currentTilePrefab = Resources.Load("Plants/"+Database.tile[i].NAME) as GameObject;
            float xRot=0;
         //   if (Database.tile[i].NAME == "obuolys_1"|| Database.tile[i].NAME == "kriause_1") { xRot = -90f; }// for models that need rotations. Spearate with ||.

            GameObject currentTile = Instantiate(currentTilePrefab, new Vector3(Database.tile[i].X, 0f, Database.tile[i].Z),Quaternion.Euler(new Vector3(xRot, Random.Range(-350.0f, 350.0f), 0)) ,GameObject.Find("Tiles").transform) as GameObject;
            currentTile.GetComponent<BuildingScript>().thistile = Database.tile[i];

            i++;
        }

        GameObject loadpanel = GameObject.Find("LoadingPanel");
        Destroy(loadpanel);

       

    }


    public void BuildTile(SocketIOEvent evt)
    {
        Debug.Log("GOT BUILD_TILE FROM SERVER");
        float X = float.Parse(evt.data.GetField("TileX").ToString());
        float Z = float.Parse(evt.data.GetField("TileZ").ToString());
        string tilename = evt.data.GetField("TileName").ToString();

        int i = 0;
        int count = 1;

    /*    while (i < Database.tile.Length)
        {
            if (Database.tile[i].X==X&& Database.tile[i].Z == Z)
            {
                count++;

            }
            i++;
        }
       */                    //FIXME /\
        
        if (count == 1) //single plant
        {
            SpawnATile(tilename, X, Z);

        }else if(count == 2) //two plants one tile
        {
            //move tile to the side
            SpawnATile(tilename, X+0.25f, Z);
        }
        else if(count == 3) //three plants 1 tile
        {


        }



       




    }


    public void SpawnATile(string tilename, float X, float Z)
    {
        tilename = tilename.Replace("\"", ""); //formatting to get rid of quotation marks
        float xRot = 0;
      //  if (tilename == "obuolys_1" || tilename == "kriause_1") { xRot = -90f; }// for models that need rotations. Spearate with ||.

   
       

        GameObject currentTilePrefab = Resources.Load("Plants/" + tilename) as GameObject;
        GameObject currentTile = Instantiate(currentTilePrefab, new Vector3(X, 0f, Z), Quaternion.Euler(new Vector3(xRot, Random.Range(-350.0f, 350.0f), 0)), GameObject.Find("Tiles").transform) as GameObject;
       // currentTile.transform.localScale = new Vector3(Random.Range(0.05f, 0.09f), Random.Range(0.05f, 0.09f), Random.Range(0.05f, 0.09f));  FIXME: ask unity teacher y not workin
        

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
