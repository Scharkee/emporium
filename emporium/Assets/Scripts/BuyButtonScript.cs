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

     

     
        panelEnabled = false;

    }

    void Awake()
    {

      
    }

    public void TheClick()
    {

        if(Globals.Instance.cameraUp && DisabledObjectsGameScene.Instance.Selector.GetComponent<BuyMode>().enabled) //buy mode is enabled. Cancel buy mode.
        {

            DisabledObjectsGameScene.Instance.Selector.GetComponent<BuyMode>().DisableBuyMode();
            
        }else if (Globals.Instance.cameraUp && DisabledObjectsGameScene.Instance.tileSellScript.GetComponent<TileSellScript>().sellModeEnabled) //enabled sell mode, disabling.
        {
            DisabledObjectsGameScene.Instance.tileSellScript.GetComponent<TileSellScript>().EnableDisableSellMode();

        }

        StartCoroutine(BuyMenuPanelFader());
    }



    public IEnumerator BuyMenuPanelFader()
    {
        DisabledObjectsGameScene.Instance.BuyMenuPanel.SetActive(true);

        if (DisabledObjectsGameScene.Instance.BuyMenuPanel.GetComponent<CanvasGroup>().alpha<1f)
        {
          
            panelEnabled = true; // used to stop rotation when viewing panel

            while (DisabledObjectsGameScene.Instance.BuyMenuPanel.GetComponent<CanvasGroup>().alpha < 1f)
            {
                //fadeoutas
               
                yield return new WaitForSeconds(0.001f);
                //didinam alpha kas cikla
                DisabledObjectsGameScene.Instance.BuyMenuPanel.GetComponent<CanvasGroup>().alpha += 0.1f;
                
                Globals.Instance.cameraBlur.blurSize += 0.22f;
               
               
            }
            Globals.Instance.cameraBlur.enabled = true;
            DisabledObjectsGameScene.Instance.BuyMenuPanel.GetComponent<CanvasGroup>().alpha = 1f;



        }
        else if (DisabledObjectsGameScene.Instance.BuyMenuPanel.GetComponent<CanvasGroup>().alpha > 0f)
        {
            panelEnabled = false;

            while (DisabledObjectsGameScene.Instance.BuyMenuPanel.GetComponent<CanvasGroup>().alpha > 0f)
            {
                //fadeoutas

                yield return new WaitForSeconds(0.001f);
                //mazinam alpha kas cikla
                DisabledObjectsGameScene.Instance.BuyMenuPanel.GetComponent<CanvasGroup>().alpha -= 0.1f;

                Globals.Instance.cameraBlur.blurSize -= 0.22f;



            }
            Globals.Instance.cameraBlur.enabled = false;
            DisabledObjectsGameScene.Instance.BuyMenuPanel.GetComponent<CanvasGroup>().alpha = 0f;


            DisabledObjectsGameScene.Instance.BuyMenuPanel.SetActive(false);


        }

    }


    public void CancelContext() //parejo broadcastas, isjungti VISUS context panels
    {
        StartCoroutine(BuyMenuPanelFader());



    }

}
