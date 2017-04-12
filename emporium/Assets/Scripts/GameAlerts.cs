﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameAlerts : MonoBehaviour {

    private static bool alertUp;
    private static List<string> alertQueue;
    private static List<string> tempQueue;
    static Vector2 alertDefaultPos;
    static Vector2 alertCenterPos;

    int popUpSpeed;

    public AudioClip alert;
    public AudioClip error;

    void Start()
    {
        alertQueue = new List<string>();
        alertUp = false;  //nerodomas joks alertas. 
        alertDefaultPos = DisabledObjectsGameScene.alertPanel.transform.position;
        alertCenterPos = new Vector2(Screen.width/2,Screen.height/2);
        popUpSpeed = 1;
        DisabledObjectsGameScene.alertPanel.GetComponent<AudioSource>().clip = alert; //implement more later mb.
    }

    void Update()
    {
        

        if (!alertQueue.Count.Equals(0)&& !alertUp)
        {
            
            //TODO: veikia tik 2 alertai max. tyngiu taisyt mb later.


            

        
            for (int i = 0; i < alertQueue.Count; i++) // Loop through List with for
            {
                Debug.Log("aaa");

                StartCoroutine(startAlert(alertQueue[i]));

            }

            alertQueue.Clear();

        }
    }


    public static void AlertWithMessage(string content)
    {
        Debug.Log("alerting string "+ content);
        alertQueue.Add(content);

        //TODO
        
    }


    private IEnumerator startAlert(string str)
    {

    
        while (alertUp)
        {
            yield return new WaitForSeconds(0.5f);
            Debug.Log("waiting for alert to be closed");
        }

       

        DisabledObjectsGameScene.alertPanel.SetActive(true);
        DisabledObjectsGameScene.alertPanel.transform.FindChild("Alert_Text").GetComponent<Text>().text=str;

        
        
        while (DisabledObjectsGameScene.alertPanel.GetComponent<CanvasGroup>().alpha < 1)
        {
            
            DisabledObjectsGameScene.alertPanel.transform.position = Vector2.Lerp(DisabledObjectsGameScene.alertPanel.transform.position, alertCenterPos,Time.deltaTime*10);
            DisabledObjectsGameScene.alertPanel.GetComponent<CanvasGroup>().alpha += 0.1f;
            yield return new WaitForSeconds(0.01f);
            

        }

        DisabledObjectsGameScene.alertPanel.GetComponent<AudioSource>().Play();


       alertUp = true;
        Debug.Log("alertups is" + alertUp);

    }

    public static void closeAlert()
    {
        
        DisabledObjectsGameScene.alertPanel.GetComponent<CanvasGroup>().alpha = 0f;
        DisabledObjectsGameScene.alertPanel.transform.position = alertDefaultPos;
        DisabledObjectsGameScene.alertPanel.SetActive(false);
        alertUp = false;
    }
}
