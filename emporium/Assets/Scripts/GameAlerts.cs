using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameAlerts : MonoBehaviour {

    private static bool alertUp;
    private static List<string> alertQueue;
    private static List<string> tempQueue;
    static Vector2 alertDefaultPos;

    int popUpSpeed;

    void Start()
    {
        alertQueue = new List<string>();
        alertUp = false;  //nerodomas joks alertas. 
        alertDefaultPos = DisabledObjectsGameScene.alertPanel.transform.position;

        popUpSpeed = 1;
    }

    void Update()
    {
        

        if (!alertQueue.Count.Equals(0)&& !alertUp)
        {
            Debug.Log("alerting");
            tempQueue = alertQueue;
            


            Debug.Log(tempQueue.Count);
            Debug.Log(alertQueue.Count);
            alertQueue.Clear();

            foreach (string str in tempQueue) //doesnt get trough this for some reason.
            {
                Debug.Log("aaa");

                StartCoroutine(startAlert(str));
            }

           

        }
    }


    public static void AlertWithMessage(string str)
    {
        Debug.Log("alerting string "+str);
        alertQueue.Add(str);

        //TODO


    }

    private IEnumerator startAlert(string str)
    {

        Debug.Log("through to IENUM");


        while (alertUp)
        {
            yield return new WaitForSeconds(0.5f);
        }

        alertUp = true;

        DisabledObjectsGameScene.alertPanel.SetActive(true);
        DisabledObjectsGameScene.alertPanel.transform.FindChild("Alert_Text").GetComponent<Text>().text=str;

        while (DisabledObjectsGameScene.alertPanel.GetComponent<CanvasGroup>().alpha <= 1)
        {
            DisabledObjectsGameScene.alertPanel.GetComponent<CanvasGroup>().alpha += 0.01f;
            yield return new WaitForSeconds(0.01f);

        }

        



    }

    public static void closeAlert()
    {
        alertUp = false;
        DisabledObjectsGameScene.alertPanel.GetComponent<CanvasGroup>().alpha = 0f;
        DisabledObjectsGameScene.alertPanel.transform.position = alertDefaultPos;
        DisabledObjectsGameScene.alertPanel.SetActive(false);
    }
}
