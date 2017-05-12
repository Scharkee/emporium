using UnityEngine;

public class alertPanelScript : MonoBehaviour
{
    public void closeAlert()
    {
        GameAlerts.Instance.closeAlert();
    }

    public void CancelContext() //parejo broadcastas,
    {
        GameAlerts.Instance.closeAlert();
    }
}