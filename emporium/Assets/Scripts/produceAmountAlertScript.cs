using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class produceAmountAlertScript : MonoBehaviour {

    private float timeAlive = 0.7f;
    private Color color1;

	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {

        //TODO make color transition based on fruit color?? idk idea
        if (timeAlive > 0)
        {
            gameObject.transform.position = new Vector3(transform.position.x,transform.position.y+2f, transform.position.z);
            timeAlive -= 0.01f;

            gameObject.GetComponent<Text>().color = Color.Lerp(Color.grey, Color.black, Mathf.PingPong(Time.time*2, 1));
        }
        else
        {


            Destroy(gameObject);
        }




		
	}
}
