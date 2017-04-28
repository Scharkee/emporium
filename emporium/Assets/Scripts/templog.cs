using UnityEngine;
using System.Collections;

public class templog : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    public void loginAsMat()
    {
        GlobalControl.Instance.Uname = "mat";
        GlobalControl.Instance.Pass = "rho";
        GameObject.Find("IdentifierPanel").GetComponent<LoginCheck>().LogInCh("mat", "rho");



    }




}
