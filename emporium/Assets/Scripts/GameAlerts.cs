using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameAlerts : MonoBehaviour
{
    private bool alertUp;
    private List<string> alertQueue;
    private List<string> tempQueue;
    private Vector2 alertDefaultPos;
    private Vector2 alertCenterPos;

    public static GameAlerts Instance;

    private int popUpSpeed;

    public AudioClip alert;
    public AudioClip error;

    private void Start()
    {
        alertQueue = new List<string>();
        alertUp = false;  //nerodomas joks alertas.
        alertDefaultPos = DisabledObjectsGameScene.Instance.alertPanel.transform.position;
        alertCenterPos = new Vector2(Screen.width / 2, Screen.height / 2);
        popUpSpeed = 1;
        DisabledObjectsGameScene.Instance.alertPanel.GetComponent<AudioSource>().clip = alert; //implement more later mb.
    }

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (!alertQueue.Count.Equals(0))
        {
            lock (alertQueue)
            {
                foreach (string alertContent in alertQueue)
                {
                    StartCoroutine(startAlert(alertContent));
                }

                alertQueue.Clear();
            }
        }
    }

    public void AlertWithMessage(string content)
    {
        lock (alertQueue)
        {
            alertQueue.Add(content);
        }

        //TODO
    }

    private IEnumerator startAlert(string str)
    {
        while (alertUp)
        {
            yield return new WaitForSeconds(0.8f);
            Debug.Log("waiting for alert to be closed");
        }

        DisabledObjectsGameScene.Instance.alertPanel.SetActive(true);
        DisabledObjectsGameScene.Instance.alertPanel.transform.FindChild("Alert_Text").GetComponent<Text>().text = str;

        while (DisabledObjectsGameScene.Instance.alertPanel.GetComponent<CanvasGroup>().alpha < 1)
        {
            DisabledObjectsGameScene.Instance.alertPanel.transform.position = Vector2.Lerp(DisabledObjectsGameScene.Instance.alertPanel.transform.position, alertCenterPos, Time.deltaTime * 10);
            DisabledObjectsGameScene.Instance.alertPanel.GetComponent<CanvasGroup>().alpha += 0.1f;
            yield return new WaitForSeconds(0.01f);
        }

        DisabledObjectsGameScene.Instance.alertPanel.GetComponent<AudioSource>().Play();

        alertUp = true;
    }

    public void closeAlert()
    {
        DisabledObjectsGameScene.Instance.alertPanel.GetComponent<CanvasGroup>().alpha = 0f;
        DisabledObjectsGameScene.Instance.alertPanel.transform.position = alertDefaultPos;
        DisabledObjectsGameScene.Instance.alertPanel.SetActive(false);
        alertUp = false;
    }
}