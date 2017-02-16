using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BuyButtonScript : MonoBehaviour
{

    public float panelimagecolor;

    public Color panelimage;

  
     GameObject opgrid;




    void Start()
    {

     

       opgrid.SetActive(false);


    }

    void Awake()
    {

        opgrid = GameObject.Find("OptionGrid").gameObject;
    }

    public void TheClick()
    {

        StartCoroutine(BuyMenuPanelFader());
    }



    IEnumerator BuyMenuPanelFader()
    {
        GameObject menupanel = GameObject.Find("BuyMenuPanel");

        if (menupanel.GetComponent<CanvasGroup>().alpha<1f)
        {
            activateOpGrid(true);

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

    void activateOpGrid(bool bb)
    {

       opgrid.SetActive(bb);
    }
}
