using UnityEngine;

public class templog : MonoBehaviour
{
    // Use this for initialization
    private void Start()
    {
    }

    private void Update()
    {
    }

    public void loginAsMat()
    {
        GlobalControl.Instance.Uname = "mat";
        GlobalControl.Instance.Pass = "rho";
        GameObject.Find("IdentifierPanel").GetComponent<LoginCheck>().LogInCh("mat", "rho");
    }
}