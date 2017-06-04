using SocketIO;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;

public class AssignTiles : MonoBehaviour
{
    private UIManager uiManager;
    private SocketManager socman;
    public AudioClip shovel1, shovel2;
    private AudioSource sound;

    private bool inventoryGenerated = false;

    // Use this for initialization
    private void Start()
    {
        sound = gameObject.GetComponent<AudioSource>();

        uiManager = DisabledObjectsGameScene.Instance.managerialScripts.GetComponent<UIManager>();

        socman = DisabledObjectsGameScene.Instance.managerialScripts.GetComponent<SocketManager>();
    }

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

        if (!inventoryGenerated)
        {
            inventoryGenerated = true;
            DisabledObjectsGameScene.Instance.managerialScripts.GetComponent<InventoryManager>().GenerateInventory();
        }
    }

    public void SpawnTiles()
    {
        //sita funkcija atsakinga uz visu tile spawninima zaidimo pradzioje.
        int i = 0;
        //TODO: randomized scales, switch (galbut), atskirts spawninima i atskira funkc.

        while (i < Database.Instance.tile.Length)
        {
            SpawnTile(i);
            Debug.Log("spawning tile " + i);
            i++;
        }

        StartCoroutine(Database.Instance.waitForTileAssignmentCompletion());
    }

    public void UpgradeTile(int ID)
    {
        GameObject tile = tileByID(ID);

        int tileposinActiveTiles = Database.Instance.ActiveTiles.IndexOf(tile);
        if (tile.GetComponent<BuildingScript>().thistileInfo.BUILDING_TYPE == 0 || tile.GetComponent<BuildingScript>().thistileInfo.BUILDING_TYPE == 3 | tile.GetComponent<BuildingScript>().thistileInfo.BUILDING_TYPE == 4)//upgradeable pastatas
        {
            Database.Instance.ActiveTiles[tileposinActiveTiles].GetComponent<BuildingScript>().thistile.COUNT++;
        }

        sound.clip = randomShovel();
        sound.Play();

        SpawnUpgradedTile(tileposinActiveTiles, tile);
    }

    private AudioClip randomShovel()
    {
        AudioClip randomShovelClip = new AudioClip();
        int randomizer = Random.Range(1, 2);

        switch (randomizer)
        {
            case 1:
                randomShovelClip = shovel1;
                break;

            case 2:
                randomShovelClip = shovel2;
                break;
        }

        return randomShovelClip;
    }

    public void SpawnUpgradedTile(int i, GameObject oldtile)
    {
        GameObject currentTile = new GameObject();
        //uzkraunami prefabs
        GameObject currentAdditionalPrefab = Resources.Load("Plants/additionalBuildings/" + Database.Instance.ActiveTiles[i].GetComponent<BuildingScript>().thistile.NAME) as GameObject;
        GameObject currentTilePrefab = Resources.Load("Plants/" + Database.Instance.ActiveTiles[i].GetComponent<BuildingScript>().thistile.NAME) as GameObject;
        float xRot = 0;

        //gal but pakeist i switcha, randomizint vosvos scale visu (kad nebutu absolutely identical.

        if (Database.Instance.ActiveTiles[i].GetComponent<BuildingScript>().thistile.COUNT == 1)
        {
            currentTile = (Instantiate(currentTilePrefab, new Vector3(Database.Instance.ActiveTiles[i].GetComponent<BuildingScript>().thistile.X, 0f, Database.Instance.ActiveTiles[i].GetComponent<BuildingScript>().thistile.Z), Quaternion.identity, GameObject.Find("Tiles").transform) as GameObject).gameObject;

            currentTile.transform.Find(Database.Instance.ActiveTiles[i].GetComponent<BuildingScript>().thistile.NAME).rotation = Quaternion.Euler(new Vector3(xRot, Random.Range(-350.0f, 350.0f), 0));
        }
        else if (Database.Instance.ActiveTiles[i].GetComponent<BuildingScript>().thistile.COUNT == 2)
        {
            currentTile = (Instantiate(currentTilePrefab, new Vector3(Database.Instance.ActiveTiles[i].GetComponent<BuildingScript>().thistile.X, 0f, Database.Instance.ActiveTiles[i].GetComponent<BuildingScript>().thistile.Z), Quaternion.identity, GameObject.Find("Tiles").transform) as GameObject).gameObject;
            GameObject n1 = currentTile.transform.Find(Database.Instance.ActiveTiles[i].GetComponent<BuildingScript>().thistile.NAME).gameObject;
            n1.transform.localPosition = new Vector3(n1.transform.localPosition.x - 0.25f, 0f, n1.transform.localPosition.z);
            n1.transform.rotation = Quaternion.Euler(new Vector3(xRot, Random.Range(-350.0f, 350.0f), 0));

            currentAdditionalPrefab = Resources.Load("Plants/additionalBuildings/" + Database.Instance.ActiveTiles[i].GetComponent<BuildingScript>().thistile.NAME) as GameObject;
            GameObject currentAdditional = (Instantiate(currentAdditionalPrefab, new Vector3(Database.Instance.ActiveTiles[i].GetComponent<BuildingScript>().thistile.X + 0.25f, 0f, Database.Instance.ActiveTiles[i].GetComponent<BuildingScript>().thistile.Z), Quaternion.Euler(new Vector3(xRot, Random.Range(-350.0f, 350.0f), 0)), currentTile.transform) as GameObject).gameObject;
        }
        else if (Database.Instance.ActiveTiles[i].GetComponent<BuildingScript>().thistile.COUNT == 3)
        {
            currentTile = (Instantiate(currentTilePrefab, new Vector3(Database.Instance.tile[i].X, 0f, Database.Instance.tile[i].Z), Quaternion.identity, GameObject.Find("Tiles").transform) as GameObject).gameObject;
            GameObject n1 = currentTile.transform.Find(Database.Instance.tile[i].NAME).gameObject;
            n1.transform.localPosition = new Vector3(n1.transform.localPosition.x, 0f, n1.transform.localPosition.z - 0.25f);
            n1.transform.rotation = Quaternion.Euler(new Vector3(xRot, Random.Range(-350.0f, 350.0f), 0));

            currentAdditionalPrefab = Resources.Load("Plants/additionalBuildings/" + Database.Instance.tile[i].NAME) as GameObject;
            GameObject currentAdditional = (Instantiate(currentAdditionalPrefab, new Vector3(Database.Instance.tile[i].X + 0.2f, 0f, Database.Instance.tile[i].Z - 0.3f), Quaternion.Euler(new Vector3(xRot, Random.Range(-350.0f, 350.0f), 0)), currentTile.transform) as GameObject).gameObject;
            GameObject currentAdditional1 = (Instantiate(currentAdditionalPrefab, new Vector3(Database.Instance.tile[i].X + 0.2f, 0f, Database.Instance.tile[i].Z + 0.3f), Quaternion.Euler(new Vector3(xRot, Random.Range(-350.0f, 350.0f), 0)), currentTile.transform) as GameObject).gameObject;
        }
        else if (Database.Instance.ActiveTiles[i].GetComponent<BuildingScript>().thistile.COUNT == 4)
        {
            currentTile = (Instantiate(currentTilePrefab, new Vector3(Database.Instance.ActiveTiles[i].GetComponent<BuildingScript>().thistile.X, 0f, Database.Instance.ActiveTiles[i].GetComponent<BuildingScript>().thistile.Z), Quaternion.identity, GameObject.Find("Tiles").transform) as GameObject).gameObject;
            GameObject n1 = currentTile.transform.Find(Database.Instance.ActiveTiles[i].GetComponent<BuildingScript>().thistile.NAME).gameObject;
            n1.transform.localPosition = new Vector3(n1.transform.localPosition.x + 0.3f, 0f, n1.transform.localPosition.z - 0.3f);
            n1.transform.rotation = Quaternion.Euler(new Vector3(xRot, Random.Range(-350.0f, 350.0f), 0));

            currentTile.GetComponent<BuildingScript>().thistile = Database.Instance.ActiveTiles[i].GetComponent<BuildingScript>().thistile;
            currentTile.GetComponent<BuildingScript>().idInTileDatabase = i;

            GameObject currentAdditional = (Instantiate(currentAdditionalPrefab, new Vector3(Database.Instance.ActiveTiles[i].GetComponent<BuildingScript>().thistile.X + 0.3f, 0f, Database.Instance.ActiveTiles[i].GetComponent<BuildingScript>().thistile.Z + 0.3f), Quaternion.Euler(new Vector3(xRot, Random.Range(-350.0f, 350.0f), 0)), currentTile.transform) as GameObject).gameObject;
            GameObject currentAdditional1 = (Instantiate(currentAdditionalPrefab, new Vector3(Database.Instance.ActiveTiles[i].GetComponent<BuildingScript>().thistile.X - 0.3f, 0f, Database.Instance.ActiveTiles[i].GetComponent<BuildingScript>().thistile.Z + 0.3f), Quaternion.Euler(new Vector3(xRot, Random.Range(-350.0f, 350.0f), 0)), currentTile.transform) as GameObject).gameObject;
            GameObject currentAdditional2 = (Instantiate(currentAdditionalPrefab, new Vector3(Database.Instance.ActiveTiles[i].GetComponent<BuildingScript>().thistile.X - 0.3f, 0f, Database.Instance.ActiveTiles[i].GetComponent<BuildingScript>().thistile.Z - 0.3f), Quaternion.Euler(new Vector3(xRot, Random.Range(-350.0f, 350.0f), 0)), currentTile.transform) as GameObject).gameObject;
        }
        else if (Database.Instance.ActiveTiles[i].GetComponent<BuildingScript>().thistile.COUNT == 5)
        {
            currentTile = (Instantiate(currentTilePrefab, new Vector3(Database.Instance.ActiveTiles[i].GetComponent<BuildingScript>().thistile.X, 0f, Database.Instance.ActiveTiles[i].GetComponent<BuildingScript>().thistile.Z), Quaternion.identity, GameObject.Find("Tiles").transform) as GameObject).gameObject;
            GameObject n1 = currentTile.transform.Find(Database.Instance.ActiveTiles[i].GetComponent<BuildingScript>().thistile.NAME).gameObject;
            n1.transform.rotation = Quaternion.Euler(new Vector3(xRot, Random.Range(-350.0f, 350.0f), 0));

            currentTile.GetComponent<BuildingScript>().thistile = Database.Instance.ActiveTiles[i].GetComponent<BuildingScript>().thistile;
            currentTile.GetComponent<BuildingScript>().idInTileDatabase = i;

            GameObject currentAdditional = (Instantiate(currentAdditionalPrefab, new Vector3(Database.Instance.ActiveTiles[i].GetComponent<BuildingScript>().thistile.X + 0.3f, 0f, Database.Instance.ActiveTiles[i].GetComponent<BuildingScript>().thistile.Z + 0.3f), Quaternion.Euler(new Vector3(xRot, Random.Range(-350.0f, 350.0f), 0)), currentTile.transform) as GameObject).gameObject;
            GameObject currentAdditional1 = (Instantiate(currentAdditionalPrefab, new Vector3(Database.Instance.ActiveTiles[i].GetComponent<BuildingScript>().thistile.X - 0.3f, 0f, Database.Instance.ActiveTiles[i].GetComponent<BuildingScript>().thistile.Z + 0.3f), Quaternion.Euler(new Vector3(xRot, Random.Range(-350.0f, 350.0f), 0)), currentTile.transform) as GameObject).gameObject;
            GameObject currentAdditional2 = (Instantiate(currentAdditionalPrefab, new Vector3(Database.Instance.ActiveTiles[i].GetComponent<BuildingScript>().thistile.X - 0.3f, 0f, Database.Instance.ActiveTiles[i].GetComponent<BuildingScript>().thistile.Z - 0.3f), Quaternion.Euler(new Vector3(xRot, Random.Range(-350.0f, 350.0f), 0)), currentTile.transform) as GameObject).gameObject;
            GameObject currentAdditional3 = (Instantiate(currentAdditionalPrefab, new Vector3(Database.Instance.ActiveTiles[i].GetComponent<BuildingScript>().thistile.X + 0.3f, 0f, Database.Instance.ActiveTiles[i].GetComponent<BuildingScript>().thistile.Z - 0.3f), Quaternion.Euler(new Vector3(xRot, Random.Range(-350.0f, 350.0f), 0)), currentTile.transform) as GameObject).gameObject;
        }

        currentTile.GetComponent<BuildingScript>().thistile = Database.Instance.ActiveTiles[i].GetComponent<BuildingScript>().thistile;

        Database.Instance.ActiveTiles.Add(currentTile);
        Database.Instance.ActiveTiles.Remove(oldtile);
        Destroy(oldtile);

        currentTile.GetComponent<BuildingScript>().idInActiveTiles = Database.Instance.ActiveTiles.IndexOf(currentTile);

        currentTile.GetComponent<BuildingScript>().RetrieveTileInfo();

        if (currentTile.GetComponent<BuildingScript>().thistileInfo.BUILDING_TYPE == 0) //plant
        {
            randomizeTileScales(currentTile);
        }
    }

    public GameObject tileByID(int id)
    {
        GameObject tile = new GameObject();

        foreach (GameObject current in Database.Instance.ActiveTiles)
        {
            if (current.GetComponent<BuildingScript>().thistile.ID == id)
            {
                tile = current;
            }
        }

        return tile;
    }

    public void BuildTile(SocketIOEvent evt)
    {
        float X = float.Parse(evt.data.GetField("TileX").ToString());
        float Z = float.Parse(evt.data.GetField("TileZ").ToString());
        string tilename = evt.data.GetField("TileName").ToString();

        int ID = int.Parse(Regex.Replace(evt.data.GetField("ID").ToString(), "[^0-9]", ""));

        SpawnATile(tilename, X, Z, ID);
    }

    public void SpawnTile(int i)
    {
        GameObject currentTile = new GameObject();
        //uzkraunami prefabs
        GameObject currentAdditionalPrefab = Resources.Load("Plants/additionalBuildings/" + Database.Instance.tile[i].NAME) as GameObject;
        GameObject currentTilePrefab = Resources.Load("Plants/" + Database.Instance.tile[i].NAME) as GameObject;
        float xRot = 0;

        //gal but pakeist i switcha, randomizint vosvos scale visu (kad nebutu absolutely identical.

        if (Database.Instance.tile[i].COUNT == 1)
        {
            currentTile = (Instantiate(currentTilePrefab, new Vector3(Database.Instance.tile[i].X, 0f, Database.Instance.tile[i].Z), Quaternion.identity, GameObject.Find("Tiles").transform) as GameObject).gameObject;

            currentTile.transform.Find(Database.Instance.tile[i].NAME).rotation = Quaternion.Euler(new Vector3(xRot, Random.Range(-350.0f, 350.0f), 0));
        }
        else if (Database.Instance.tile[i].COUNT == 2)
        {
            currentTile = (Instantiate(currentTilePrefab, new Vector3(Database.Instance.tile[i].X, 0f, Database.Instance.tile[i].Z), Quaternion.identity, GameObject.Find("Tiles").transform) as GameObject).gameObject;
            GameObject n1 = currentTile.transform.Find(Database.Instance.tile[i].NAME).gameObject;
            n1.transform.localPosition = new Vector3(n1.transform.localPosition.x - 0.25f, 0f, n1.transform.localPosition.z);
            n1.transform.rotation = Quaternion.Euler(new Vector3(xRot, Random.Range(-350.0f, 350.0f), 0));

            currentAdditionalPrefab = Resources.Load("Plants/additionalBuildings/" + Database.Instance.tile[i].NAME) as GameObject;
            GameObject currentAdditional = (Instantiate(currentAdditionalPrefab, new Vector3(Database.Instance.tile[i].X + 0.25f, 0f, Database.Instance.tile[i].Z), Quaternion.Euler(new Vector3(xRot, Random.Range(-350.0f, 350.0f), 0)), currentTile.transform) as GameObject).gameObject;
        }
        else if (Database.Instance.tile[i].COUNT == 3)
        {
            currentTile = (Instantiate(currentTilePrefab, new Vector3(Database.Instance.tile[i].X, 0f, Database.Instance.tile[i].Z), Quaternion.identity, GameObject.Find("Tiles").transform) as GameObject).gameObject;
            GameObject n1 = currentTile.transform.Find(Database.Instance.tile[i].NAME).gameObject;
            n1.transform.localPosition = new Vector3(n1.transform.localPosition.x, 0f, n1.transform.localPosition.z - 0.25f);
            n1.transform.rotation = Quaternion.Euler(new Vector3(xRot, Random.Range(-350.0f, 350.0f), 0));

            currentTile.GetComponent<BuildingScript>().thistile = Database.Instance.tile[i];
            currentTile.GetComponent<BuildingScript>().idInTileDatabase = i;

            currentAdditionalPrefab = Resources.Load("Plants/additionalBuildings/" + Database.Instance.tile[i].NAME) as GameObject;
            GameObject currentAdditional = (Instantiate(currentAdditionalPrefab, new Vector3(Database.Instance.tile[i].X - 0.25f, 0f, Database.Instance.tile[i].Z + 0.25f), Quaternion.Euler(new Vector3(xRot, Random.Range(-350.0f, 350.0f), 0)), currentTile.transform) as GameObject).gameObject;
            GameObject currentAdditional1 = (Instantiate(currentAdditionalPrefab, new Vector3(Database.Instance.tile[i].X + 0.22f, 0f, Database.Instance.tile[i].Z + 0.14f), Quaternion.Euler(new Vector3(xRot, Random.Range(-350.0f, 350.0f), 0)), currentTile.transform) as GameObject).gameObject;
        }
        else if (Database.Instance.tile[i].COUNT == 4)
        {
            currentTile = (Instantiate(currentTilePrefab, new Vector3(Database.Instance.tile[i].X, 0f, Database.Instance.tile[i].Z), Quaternion.identity, GameObject.Find("Tiles").transform) as GameObject).gameObject;
            GameObject n1 = currentTile.transform.Find(Database.Instance.tile[i].NAME).gameObject;
            n1.transform.localPosition = new Vector3(n1.transform.localPosition.x + 0.3f, 0f, n1.transform.localPosition.z - 0.3f);
            n1.transform.rotation = Quaternion.Euler(new Vector3(xRot, Random.Range(-350.0f, 350.0f), 0));

            currentTile.GetComponent<BuildingScript>().thistile = Database.Instance.tile[i];
            currentTile.GetComponent<BuildingScript>().idInTileDatabase = i;

            GameObject currentAdditional = (Instantiate(currentAdditionalPrefab, new Vector3(Database.Instance.tile[i].X + 0.3f, 0f, Database.Instance.tile[i].Z + 0.3f), Quaternion.Euler(new Vector3(xRot, Random.Range(-350.0f, 350.0f), 0)), currentTile.transform) as GameObject).gameObject;
            GameObject currentAdditional1 = (Instantiate(currentAdditionalPrefab, new Vector3(Database.Instance.tile[i].X - 0.3f, 0f, Database.Instance.tile[i].Z + 0.3f), Quaternion.Euler(new Vector3(xRot, Random.Range(-350.0f, 350.0f), 0)), currentTile.transform) as GameObject).gameObject;
            GameObject currentAdditional2 = (Instantiate(currentAdditionalPrefab, new Vector3(Database.Instance.tile[i].X - 0.3f, 0f, Database.Instance.tile[i].Z - 0.3f), Quaternion.Euler(new Vector3(xRot, Random.Range(-350.0f, 350.0f), 0)), currentTile.transform) as GameObject).gameObject;
        }
        else if (Database.Instance.tile[i].COUNT == 5)
        {
            currentTile = (Instantiate(currentTilePrefab, new Vector3(Database.Instance.tile[i].X, 0f, Database.Instance.tile[i].Z), Quaternion.identity, GameObject.Find("Tiles").transform) as GameObject).gameObject;
            GameObject n1 = currentTile.transform.Find(Database.Instance.tile[i].NAME).gameObject;
            n1.transform.rotation = Quaternion.Euler(new Vector3(xRot, Random.Range(-350.0f, 350.0f), 0));

            currentTile.GetComponent<BuildingScript>().thistile = Database.Instance.tile[i];
            currentTile.GetComponent<BuildingScript>().idInTileDatabase = i;

            GameObject currentAdditional = (Instantiate(currentAdditionalPrefab, new Vector3(Database.Instance.tile[i].X + 0.3f, 0f, Database.Instance.tile[i].Z + 0.3f), Quaternion.Euler(new Vector3(xRot, Random.Range(-350.0f, 350.0f), 0)), currentTile.transform) as GameObject).gameObject;
            GameObject currentAdditional1 = (Instantiate(currentAdditionalPrefab, new Vector3(Database.Instance.tile[i].X - 0.3f, 0f, Database.Instance.tile[i].Z + 0.3f), Quaternion.Euler(new Vector3(xRot, Random.Range(-350.0f, 350.0f), 0)), currentTile.transform) as GameObject).gameObject;
            GameObject currentAdditional2 = (Instantiate(currentAdditionalPrefab, new Vector3(Database.Instance.tile[i].X - 0.3f, 0f, Database.Instance.tile[i].Z - 0.3f), Quaternion.Euler(new Vector3(xRot, Random.Range(-350.0f, 350.0f), 0)), currentTile.transform) as GameObject).gameObject;
            GameObject currentAdditional3 = (Instantiate(currentAdditionalPrefab, new Vector3(Database.Instance.tile[i].X + 0.3f, 0f, Database.Instance.tile[i].Z - 0.3f), Quaternion.Euler(new Vector3(xRot, Random.Range(-350.0f, 350.0f), 0)), currentTile.transform) as GameObject).gameObject;
        }

        currentTile.GetComponent<BuildingScript>().idInTileDatabase = i;

        currentTile.GetComponent<BuildingScript>().thistile = Database.Instance.tile[i];

        currentTile.GetComponent<BuildingScript>().idInTileDatabase = Database.Instance.ActiveTiles.IndexOf(currentTile);

        currentTile.GetComponent<BuildingScript>().RetrieveTileInfo();

        Debug.Log(currentTile.GetComponent<BuildingScript>().thistileInfo.BUILDING_TYPE);

        if (currentTile.GetComponent<BuildingScript>().thistileInfo.BUILDING_TYPE == 0) //plant
        {
            randomizeTileScales(currentTile);
        }
        else
        {
            currentTile.transform.rotation = Quaternion.identity;
        }

        Database.Instance.ActiveTiles.Add(currentTile);
    }

    public void SpawnATile(string tilename, float X, float Z, int ID)
    {
        tilename = tilename.Replace("\"", ""); //formatting to get rid of quotation marks

        float xRot = 0;

        sound.clip = randomShovel();
        sound.Play();

        GameObject currentTilePrefab = Resources.Load("Plants/" + tilename) as GameObject;
        GameObject currentTile = Instantiate(currentTilePrefab, new Vector3(X, 0f, Z), Quaternion.Euler(new Vector3(xRot, 0, 0)), GameObject.Find("Tiles").transform) as GameObject;

        foreach (Transform child in currentTile.transform)
        {
            if (child.name == "SelectionGlow")
            {
            }
            else
            {
                child.localRotation = Quaternion.Euler(new Vector3(xRot, Random.Range(-350.0f, 350.0f), 0));
            }
        }

        currentTile.GetComponent<BuildingScript>().thistile.START_OF_GROWTH = socman.unix;
        currentTile.GetComponent<BuildingScript>().thistile.COUNT = 1;
        currentTile.GetComponent<BuildingScript>().thistile.ID = ID;
        currentTile.GetComponent<BuildingScript>().thistile.NAME = tilename;
        currentTile.GetComponent<BuildingScript>().thistile.X = X;
        currentTile.GetComponent<BuildingScript>().thistile.Z = Z;

        Database.Instance.ActiveTiles.Add(currentTile);

        currentTile.GetComponent<BuildingScript>().RetrieveTileInfo();
    }

    private void randomizeTileScales(GameObject tile)
    {
        foreach (Transform model in tile.transform)
        {
            if (model.name == "SelectionGlow") //isskirtiniai atvejai
            {
            }
            else
            {
                model.gameObject.transform.localScale = new Vector3(Random.Range(0.85f, 1.2f), Random.Range(0.85f, 1.2f), Random.Range(0.85f, 1.2f));
            }
        }
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