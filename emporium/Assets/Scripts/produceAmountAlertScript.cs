using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class produceAmountAlertScript : MonoBehaviour {

    private float timeAlive = 2f;
    private Color color1;

	// Use this for initialization
	void Start () {
        color1 = Color.black;
        gameObject.GetComponent<Text>().color = color1;
	}
	
	// Update is called once per frame
	void Update () {

        //TODO make color transition
        if (timeAlive > 0)
        {
            gameObject.transform.position = new Vector3(transform.position.x,transform.position.y+1f, transform.position.z);
            timeAlive -= 0.01f;
        }else
        {


            Destroy(gameObject);
        }




		
	}
}
