using UnityEngine;

public class ShopTabSwitcher : MonoBehaviour
{
    // Use this for initialization
    private void Start()
    {
    }

    public void ReceivePlantTabButtonClick()
    {
        if (DisabledObjectsGameScene.Instance.gridPlants.activeSelf)
        {
        }
        else
        {
            DisabledObjectsGameScene.Instance.gridBuildings.SetActive(false);
            DisabledObjectsGameScene.Instance.gridPlants.SetActive(true);
        }
    }

    public void ReceiveBuildingTabButtonClick()
    {
        if (DisabledObjectsGameScene.Instance.gridBuildings.activeSelf)
        {
        }
        else
        {
            DisabledObjectsGameScene.Instance.gridPlants.SetActive(false);
            DisabledObjectsGameScene.Instance.gridBuildings.SetActive(true);
        }
    }
}