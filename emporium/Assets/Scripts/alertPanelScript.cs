using UnityEngine;

public class alertPanelScript : MonoBehaviour
{
    public void closeAlert()
    {
        GameAlerts.Instance.closeAlert();
    }

    public void openAlert()
    {
    }

    public void CancelContext() //parejo broadcastas,
    {
        Globals.Instance.UIBloomActive(false);
        GameAlerts.Instance.closeAlert();
    }
}