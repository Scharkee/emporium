using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;

public class HelperScripts : MonoBehaviour {

    SocketIOComponent socket;

    void Start()
    {
        GameObject socketgo = GameObject.Find("SocketIO");
        socket = socketgo.GetComponent<SocketIOComponent>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            data["Uname"] = GlobalControl.Uname;
            Debug.Log("Received lastlogged ping from server");
            socket.Emit("AUTOSAVE_PUSH_LASTLOGGED", new JSONObject(data));

    
        }



    }


    public bool LyginisPlotsize()
    {
        bool lygnelyg = true;

        if (Database.UserPlotSize % 2 == 0) //lyginis plot dimensions
        {
            lygnelyg = true;
        }
        else if (Database.UserPlotSize % 2 == 1)//NElyginis plot dimensions
        {
            lygnelyg = false;
        }
        return lygnelyg;
    }
}
