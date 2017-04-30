using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleText : MonoBehaviour {

    private float startTimer = 1.5f;
    private Color currentColor;
    private Color nextColor;
    private Text text;
    private bool nextColorGenerated = false;
    private float transitionTimer = 3f;
    private float pulseTimer = 4f; 
    private bool resetPulse = false;
    GameObject ghost;
    private bool ghost_done = false;



    private void Start()
    {
        text = gameObject.GetComponent<Text>();
        currentColor = text.color;
        ghost = GameObject.Find("TitleTextGhost");
    }

    // Update is called once per frame
    void Update () {



        if (startTimer > 0)
        {
            startTimer -= Time.deltaTime;
        }else
        {
            

            if (!nextColorGenerated)
            {

                nextColor = Random.ColorHSV(0f,1f,0.7f,1f,1f,1f,1f,1f);

                nextColorGenerated = true;
            }

            if(transitionTimer>0)
            {
                //do transit


                transitionTimer -= Time.deltaTime;

                gameObject.GetComponent<Text>().color= Color.Lerp(gameObject.GetComponent<Text>().color, nextColor,0.01f);

            }
            else
            {
                transitionTimer = 3f;
  
                nextColorGenerated = false;


            }



        }

/*
        if (!resetPulse)
        {
            if(pulseTimer > 0 )
            {
                pulseTimer -= Time.deltaTime;
                transform.localScale = Vector3.Lerp(transform.localScale,new Vector3(1.2f,1.2f,1.2f),Time.deltaTime*5);

                if (!ghost_done)
                {
                    ghostOut();
                    ghost_done = true;
                }
            }
            else
            {
                pulseTimer = 2f;
                resetPulse = true;

            }
           

            
        }else
        {
            if (pulseTimer > 0)
            {
                pulseTimer -= Time.deltaTime;
                transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(1f, 1f,1f), Time.deltaTime * 10);
            }
            else
            {
                pulseTimer = 2f;
                resetPulse = false;

            }
            

        }

    */

	}


    private void ghostOut()
    {
        ghost = GameObject.Find("TitleTextGhost");
        Color temp = text.color;
        temp.a = 0.1f;
        ghost.GetComponent<Text>().color = temp;

        StartCoroutine(ghoster());



    }

    private IEnumerator ghoster()
    {
        Vector3 maxScale=new Vector3(50f,50f,50f);

        while (ghost.transform.localScale.x < 9.9f)
        {
            yield return new WaitForSeconds(0.01f);
            ghost.GetComponent<Text>().color = Color.Lerp(ghost.GetComponent<Text>().color,new Color(1f,1f,1f,0f),0.1f);
            ghost.transform.localScale = Vector3.Lerp(ghost.transform.localScale, maxScale,Time.deltaTime);

        }

        Destroy(ghost);
        

    }
}
