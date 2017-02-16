using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {


    Text dollarText;
    Text usernameText;
    Text apelsinaiText;

	// Use this for initialization
	void Start () {





		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ChangeUIText(string TextObjName, string newtext)
    {
        Text text = GameObject.Find(TextObjName).GetComponent<Text>();

        text.text = newtext;


    }


}
