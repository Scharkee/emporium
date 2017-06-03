using UnityEngine;
using UnityEngine.UI;

public class EconomyPanelScript : MonoBehaviour
{
    public GameObject EconomyPanel_refreshTimer_edit;
    public GameObject EconomyPanel_currentManager_edit;
    public Sprite rising;
    public Sprite falling;
    public Sprite stable;

    private void Update()
    {
        EconomyPanel_refreshTimer_edit.GetComponent<Text>().text = (DisabledObjectsGameScene.Instance.pricemanager.nextUpdate - DisabledObjectsGameScene.Instance.SocketManager.unix).ToString("F1");
    }

    public void Adapt()
    {
        foreach (Transform listitem in DisabledObjectsGameScene.Instance.EconomyPanel_panel.transform)
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
                listitem.GetComponent<EconomyPanelListItem>().SellListItem_produce_symbol_image.sprite = falling;
            }
            else if (Database.Instance.Prices[listitem.GetComponent<EconomyPanelListItem>().bankProduce.produceName] == Database.Instance.Oldprices[listitem.GetComponent<EconomyPanelListItem>().bankProduce.produceName])
            {//stable
                listitem.GetComponent<EconomyPanelListItem>().SellListItem_produce_symbol_image.sprite = stable;
            }
            else //pabrango
            {
                listitem.GetComponent<EconomyPanelListItem>().SellListItem_produce_symbol_image.sprite = rising;
            }
        }
    }

    public void CancelContext() //parejo broadcastas, isjungti VISUS context panels
    {
        gameObject.SetActive(false);
    }
}