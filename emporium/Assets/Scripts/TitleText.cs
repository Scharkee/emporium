using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleText : MonoBehaviour
{

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
    private bool ghostBack = false;

    public float colorChangeAdditive = 0;


    private void Start()
    {
        text = gameObject.GetComponent<Text>();
        currentColor = text.color;
        ghost = GameObject.Find("TitleTextGhost");
    }

    // Update is called once per frame
    void Update()
    {



        if (startTimer > 0)
        {
            startTimer -= Time.deltaTime;
        }
        else
        {


            if (!nextColorGenerated)
            {

                nextColor = Random.ColorHSV(0f, 1f, 0.7f, 1f, 1f, 1f, 1f, 1f);

                nextColorGenerated = true;
            }

            if (transitionTimer > 0)
            {
                //do transit


                transitionTimer -= Time.deltaTime;

                Debug.Log(colorChangeAdditive);
                gameObject.GetComponent<Text>().color = Color.Lerp(gameObject.GetComponent<Text>().color, nextColor, colorChangeAdditive);
                ghost.GetComponent<Text>().color = Color.Lerp(new Color(gameObject.GetComponent<Text>().color.r,gameObject.GetComponent<Text>().color.g, gameObject.GetComponent<Text>().color.b,0.05f), nextColor, colorChangeAdditive);

            
                ghost.transform.localScale = Vector3.Lerp(ghost.transform.localScale, gameObject.transform.localScale * (1 + colorChangeAdditive/2),0.5f);
              
            }
            else
            {
                transitionTimer = 3f;

                nextColorGenerated = false;


            }



        }


   

    }


 
  

    private void returnGhost(GameObject ghost)
    {
        ghostBack = true;
        ghost.transform.localScale = gameObject.transform.localScale * 2;
    }
}
