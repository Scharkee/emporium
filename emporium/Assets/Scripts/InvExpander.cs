using UnityEngine;

public class InvExpander : MonoBehaviour
{
    private void Start()
    {
    }

    public void expandContract()
    {
        if (DisabledObjectsGameScene.Instance.Inventory_Fruit_panel.activeSelf)
        {
            Debug.Log("disabling");
            DisabledObjectsGameScene.Instance.Inventory_Fruit_panel.SetActive(false);
        }
        else
        {
            Debug.Log("enabling");
            DisabledObjectsGameScene.Instance.Inventory_Fruit_panel.SetActive(true);
            Debug.Log("enabling");
        }
    }
}