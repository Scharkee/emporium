using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellingButtonScript : MonoBehaviour
{



    public void expandContract()
    {

        if (DisabledObjectsGameScene.Instance.SellingPanel.activeSelf)
        {

            DisabledObjectsGameScene.Instance.SellingPanel.SetActive(false);
        }
        else
        {

            DisabledObjectsGameScene.Instance.SellingPanel.SetActive(true);

        }
    }


}
