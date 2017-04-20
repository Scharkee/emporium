using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;

public class ProduceSelling : MonoBehaviour {


    SocketIOComponent socket;
	// Use this for initialization
	void Start () {

        socket = DisabledObjectsGameScene.Instance.socket;
        socket.On("SALE_VERIFICATION", ReceiveSaleVerification);

    }

    public void AskForSaleVerification(Dictionary<string, string> sale)
    {
        socket.Emit("VERIFY_SOLD_PRODUCE", new JSONObject(sale));

    }
    public void ReceiveSaleVerification(SocketIOEvent evt)
    {

        Database.Instance.UserDollars = int.Parse(evt.data.GetField("dollars").ToString());


    }
	
	
}
