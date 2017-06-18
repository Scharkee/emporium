using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;

public class SocketConnectionStabilizer : MonoBehaviour
{
    private bool flashingON = false;

    // Use this for initialization
    private void Start()
    {
        StartCoroutine(retryConnections());
        StartCoroutine(flashConnectingStatus());
    }

    private IEnumerator retryConnections()
    {
        DisabledObjectsMain.Instance.socket.Connect();
        yield return new WaitForSeconds(0.5f);
    }

    private IEnumerator flashConnectingStatus()
    {
        while (!DisabledObjectsMain.Instance.socket.IsConnected) //kol neturim connection su serveriu
        {//flashinam connecting
            yield return new WaitForSeconds(0.01f);

            if (DisabledObjectsMain.Instance.ConnectingText.GetComponent<CanvasGroup>().alpha > 0.99f) //matosi, ijungiam issijungima
            {
                flashingON = false;
            }
            else if (DisabledObjectsMain.Instance.ConnectingText.GetComponent<CanvasGroup>().alpha < 0.05f)
            {
                flashingON = true;
            }

            if (flashingON)
            {
                DisabledObjectsMain.Instance.ConnectingText.GetComponent<CanvasGroup>().alpha += 0.01f;
            }
            else
            {
                DisabledObjectsMain.Instance.ConnectingText.GetComponent<CanvasGroup>().alpha -= 0.01f;
            }
        }

        //ok, prisijungem, nebereik.
        Destroy(DisabledObjectsMain.Instance.ConnectingText);
    }
}