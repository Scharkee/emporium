using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EconomyPanelScript : MonoBehaviour
{

    public GameObject EconomyPanel_refreshTimer_edit;
    public GameObject EconomyPanel_currentManager_edit;
    public Sprite rising;
    public Sprite falling;
    public Sprite stable;
    public float priceUpdateTimer = 999f;
    public float priceUpdateInterval = 30f;

    // Update is called once per frame
    void Update()
    {


        if (priceUpdateTimer > 0)
        {
            priceUpdateTimer -= Time.deltaTime;
        }
        else
        {
            priceUpdateTimer = priceUpdateInterval;
            DisabledObjectsGameScene.Instance.pricemanager.retrievePrices();
        }


        EconomyPanel_refreshTimer_edit.GetComponent<Text>().text = priceUpdateTimer.ToString("F1");



    }

    public void Adapt()
    {


        foreach (Transform listitem in gameObject.transform)
        {
            //check if even works

            listitem.GetComponent<EconomyPanelListItem>().SellListItem_juice_price.text = Database.Instance.Prices[listitem.GetComponent<EconomyPanelListItem>().bankJuice.produceName].ToString();
            listitem.GetComponent<EconomyPanelListItem>().SellListItem_produce_price.text = Database.Instance.Prices[listitem.GetComponent<EconomyPanelListItem>().bankProduce.produceName].ToString();

            listitem.GetComponent<EconomyPanelListItem>().SellListItem_juice_price_prev.text = Database.Instance.Oldprices[listitem.GetComponent<EconomyPanelListItem>().bankJuice.produceName].ToString();
            listitem.GetComponent<EconomyPanelListItem>().SellListItem_produce_price_prev.text = Database.Instance.Oldprices[listitem.GetComponent<EconomyPanelListItem>().bankProduce.produceName].ToString();

            if (Database.Instance.Prices[listitem.GetComponent<EconomyPanelListItem>().bankJuice.produceName] < Database.Instance.Oldprices[listitem.GetComponent<EconomyPanelListItem>().bankJuice.produceName])
            {//atpigo
                listitem.GetComponent<EconomyPanelListItem>().SellListItem_juice_symbol_image.sprite = falling;

            }
            else if (Database.Instance.Prices[listitem.GetComponent<EconomyPanelListItem>().bankJuice.produceName] == Database.Instance.Oldprices[listitem.GetComponent<EconomyPanelListItem>().bankJuice.produceName])
            {//stable
                listitem.GetComponent<EconomyPanelListItem>().SellListItem_juice_symbol_image.sprite = stable;

            }
            else //pabrango
            {
                listitem.GetComponent<EconomyPanelListItem>().SellListItem_juice_symbol_image.sprite = rising;


            }

            if (Database.Instance.Prices[listitem.GetComponent<EconomyPanelListItem>().bankProduce.produceName] < Database.Instance.Oldprices[listitem.GetComponent<EconomyPanelListItem>().bankProduce.produceName])
            {//atpigo
                listitem.GetComponent<EconomyPanelListItem>().SellListItem_juice_symbol_image.sprite = falling;

            }
            else if (Database.Instance.Prices[listitem.GetComponent<EconomyPanelListItem>().bankProduce.produceName] == Database.Instance.Oldprices[listitem.GetComponent<EconomyPanelListItem>().bankProduce.produceName])
            {//stable
                listitem.GetComponent<EconomyPanelListItem>().SellListItem_juice_symbol_image.sprite = stable;

            }
            else //pabrango
            {
                listitem.GetComponent<EconomyPanelListItem>().SellListItem_juice_symbol_image.sprite = rising;


            }


        }


    }


}
