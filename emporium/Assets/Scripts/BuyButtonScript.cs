using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BuyButtonScript : MonoBehaviour
{

    public float panelimagecolor;

    public Color panelimage;

    public bool panelEnabled;

  
     GameObject opgrid;

    GameObject menupanel;


    void Start()
    {

     menupanel = GameObject.Find("BuyMenuPanel");

       opgrid.SetActive(false);
        panelEnabled = false;

    }

    void Awake()
    {

        opgrid = GameObject.Find("OptionGrid").gameObject;
    }

    public void TheClick()
    {

        if(Camera.main.transform.position.y > 3) //buy mode is enabled. Cancel buy mode.
        {

            DisabledObjectsGameScene.Selector.GetComponent<BuyMode>().DisableBuyMode();
            DisabledObjectsGameScene.Selector.GetComponent<BuyMode>().enabled = false;
        }

        StartCoroutine(BuyMenuPanelFader());
    }



    IEnumerator BuyMenuPanelFader()
    {
       menupanel = GameObject.Find("BuyMenuPanel");

        if (menupanel.GetComponent<CanvasGroup>().alpha<1f)
        {
            activateOpGrid(true);
            panelEnabled = true; // used to stop rotation when viewing panel

            while (menupanel.GetComponent<CanvasGroup>().alpha < 1f)
            {
                //fadeoutas
               
                yield return new WaitForSeconds(0.001f);
                //didinam alpha kas cikla
                menupanel.GetComponent<CanvasGroup>().alpha = menupanel.GetComponent<CanvasGroup>().alpha + 0.1f;
               
               
            }

            menupanel.GetComponent<CanvasGroup>().alpha = 1f;



        }
        else if (menupanel.GetComponent<CanvasGroup>().alpha > 0f)
        {
            panelEnabled = false;

            while (menupanel.GetComponent<CanvasGroup>().alpha > 0f)
            {
                //fadeoutas

                yield return new WaitForSeconds(0.001f);
                //mazinam alpha kas cikla
                menupanel.GetComponent<CanvasGroup>().alpha = menupanel.GetComponent<CanvasGroup>().alpha - 0.1f;
                
              


            }
            menupanel.GetComponent<CanvasGroup>().alpha = 0f;


            activateOpGrid(false);
          
        }

    }

    public void activateOpGrid(bool bb)
    {

       opgrid.SetActive(bb);
    }
}
