using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class alertPanelScript : MonoBehaviour
{



    public void closeAlert()
    {
        Debug.Log("aaSDASDasdasd");
        GameAlerts.Instance.closeAlert();
    }

    public void CancelContext() //parejo broadcastas, 
    {
        Destroy(gameObject);



    }
}
