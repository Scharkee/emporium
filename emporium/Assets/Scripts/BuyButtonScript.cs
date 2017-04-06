using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BuyButtonScript : MonoBehaviour
{

    public float panelimagecolor;

    public Color panelimage;

    public bool panelEnabled;

  
     GameObject opgrid;

   


    void Start()
    {

     

       DisabledObjectsGameScene.BuyMenuPanel.SetActive(false);
        panelEnabled = false;

    }

    void Awake()
    {

      
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
        DisabledObjectsGameScene.BuyMenuPanel.SetActive(true);

        if (DisabledObjectsGameScene.BuyMenuPanel.GetComponent<CanvasGroup>().alpha<1f)
        {
          
            panelEnabled = true; // used to stop rotation when viewing panel

            while (DisabledObjectsGameScene.BuyMenuPanel.GetComponent<CanvasGroup>().alpha < 1f)
            {
                //fadeoutas
               
                yield return new WaitForSeconds(0.001f);
                //didinam alpha kas cikla
                DisabledObjectsGameScene.BuyMenuPanel.GetComponent<CanvasGroup>().alpha += 0.1f;
               
               
            }

            DisabledObjectsGameScene.BuyMenuPanel.GetComponent<CanvasGroup>().alpha = 1f;



        }
        else if (DisabledObjectsGameScene.BuyMenuPanel.GetComponent<CanvasGroup>().alpha > 0f)
        {
            panelEnabled = false;

            while (DisabledObjectsGameScene.BuyMenuPanel.GetComponent<CanvasGroup>().alpha > 0f)
            {
                //fadeoutas

                yield return new WaitForSeconds(0.001f);
                //mazinam alpha kas cikla
                DisabledObjectsGameScene.BuyMenuPanel.GetComponent<CanvasGroup>().alpha -= 0.1f;
                
              


            }
            DisabledObjectsGameScene.BuyMenuPanel.GetComponent<CanvasGroup>().alpha = 0f;


            DisabledObjectsGameScene.BuyMenuPanel.SetActive(false);


        }

    }

    public void activateOpGrid(bool bb)
    {

       opgrid.SetActive(bb);
    }
}
