using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatContextManager : MonoBehaviour {

    Ray ray;
    RaycastHit hit;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {

            if (hit.collider.tag == "Ground")
            {

                if (DisabledObjectsGameScene.StatsContextPanel.activeSelf)
                {
                    ContextManager.CloseStatPanel();

                }

            }

                if (hit.transform.tag=="Building")
            {
                ContextManager.ShowStats(hit.transform.gameObject);

            }



        }else
        {

           
                if (DisabledObjectsGameScene.StatsContextPanel.activeSelf)
                {
                    ContextManager.CloseStatPanel();

                }

            


        }

    }

    void LateUpdate()
    {

        if (DisabledObjectsGameScene.StatsContextPanel.activeSelf)
        {
           
            DisabledObjectsGameScene.StatsContextPanel.transform.position = new Vector3(Input.mousePosition.x+100, Input.mousePosition.y-40, Input.mousePosition.z);


        }
    }

    
}
