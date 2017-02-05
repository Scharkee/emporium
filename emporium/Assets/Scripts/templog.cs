using UnityEngine;
using System.Collections;

public class templog : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    public void loginAsMat() {
        GlobalControl.Uname = "mat";
        GlobalControl.Pass = "rho";
        GameObject.Find("IdentifierPanel").GetComponent<LoginCheck>().LogInCh("mat", "rho");



    }




}
