using UnityEngine;
using UnityEngine.UI;

public class ShopTabSwitcher : MonoBehaviour
{
    // Use this for initialization
    private void Start()
    {
    }

    public void SwitchShopTab(GameObject tab)
    {
        foreach (Transform tabGrid in DisabledObjectsGameScene.Instance.TabGrids.transform)
        {
            if (tabGrid.gameObject.name != tab.GetComponent<UniversalBank>().go1.name) //kiti grid = disabled
            {
                tabGrid.gameObject.SetActive(false);
            }
            else //reikiamas grid = enabled
            {
                tabGrid.gameObject.SetActive(true);
            }
        }
        foreach (Transform tabBtn in DisabledObjectsGameScene.Instance.TabButtons.transform)
        {
            if (tabBtn.gameObject.name != tab.name) //kitas tab
            {
                tabBtn.gameObject.GetComponent<Button>().interactable = true;
            }
            else //aktyvuojamas tab
            {
                tabBtn.gameObject.GetComponent<Button>().interactable = false;
            }
        }
    }

    public void ReceivePlantTabButtonClick()
    {
        SwitchShopTab(DisabledObjectsGameScene.Instance.BuyMenuPlantTabBtn);
    }

    public void ReceiveBuildingTabButtonClick()
    {
        SwitchShopTab(DisabledObjectsGameScene.Instance.BuyMenuBuildingTabBtn);
    }
}