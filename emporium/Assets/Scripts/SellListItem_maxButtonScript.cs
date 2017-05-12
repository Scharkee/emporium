using UnityEngine;
using UnityEngine.UI;

public class SellListItem_maxButtonScript : MonoBehaviour
{
    public InputField inp;
    public string prodName;
    public Text pricetext;
    public Text SalePanelTotalstext;
    public ListItemPrice totalWeightCache;

    public ListItemPrice priceLog;
    public string Typename;

    public void TheClick()
    {
        inp.text = Database.Instance.Inventory[prodName].ToString();

        float newamount = Database.Instance.Inventory[prodName];

        //list item price(desinej)
        priceLog.PriceCache[Typename] = newamount * Database.Instance.Prices[prodName];
        priceLog.UpdatePrice(pricetext);

        //total weight in the bottom of the panel

        totalWeightCache.WeightCache[prodName] = newamount;

        totalWeightCache.UpdateTotalWeight(SalePanelTotalstext);
    }

    private void Start()
    {
        SalePanelTotalstext = DisabledObjectsGameScene.Instance.SellingPanel.transform.FindChild("Selling_totals_panel").transform.FindChild("Selling_totalsPanel_total_edit").gameObject.GetComponent<Text>();
    }

    public void resetValues()
    {
        inp.text = "";

        float newamount = 0;

        //list item price(desinej)
        priceLog.PriceCache[Typename] = newamount * Database.Instance.Prices[prodName];
        priceLog.UpdatePrice(pricetext);

        //total weight in the bottom of the panel

        totalWeightCache.WeightCache[prodName] = newamount;

        totalWeightCache.UpdateTotalWeight(SalePanelTotalstext);
    }

    public void maxOutValues()
    {
        inp.text = Database.Instance.Inventory[prodName].ToString();
        float newamount = Database.Instance.Inventory[prodName];

        //list item price(desinej)
        priceLog.PriceCache[Typename] = newamount * Database.Instance.Prices[prodName];
        priceLog.UpdatePrice(pricetext);

        //total weight in the bottom of the panel
        totalWeightCache.WeightCache[prodName] = newamount;

        totalWeightCache.UpdateTotalWeight(SalePanelTotalstext);
    }

    public void KeepAtMaxValues(string str)
    {
        //kad negaletu parduot daugiau negu turi.
        if (float.Parse(inp.text) > Database.Instance.Inventory[prodName])
        {
            inp.text = Database.Instance.Inventory[prodName].ToString();
        }

        //pritaikau price price taip pat apacioj
        float newamount = float.Parse(inp.text);

        //list item price(desinej)
        priceLog.PriceCache[Typename] = newamount * Database.Instance.Prices[prodName];
        priceLog.UpdatePrice(pricetext);

        //total weight in the bottom of the panel
        totalWeightCache.WeightCache[prodName] = newamount;

        totalWeightCache.UpdateTotalWeight(SalePanelTotalstext);
    }

    public void AdaptListingPrices()
    {
        float newamount = float.Parse(inp.text);
        priceLog.PriceCache[Typename] = newamount * Database.Instance.Prices[prodName];
        priceLog.UpdatePrice(pricetext);
    }
}