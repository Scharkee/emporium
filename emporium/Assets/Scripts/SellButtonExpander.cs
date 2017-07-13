using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellButtonExpander : MonoBehaviour
{
    private bool expanded = false;

    public void ExpandOrContractOptions()
    {
        ClickEngine.Instance.Click();
        if (expanded) //uzdarom
        {
            expanded = false;
            DisabledObjectsGameScene.Instance.Sellbutton_options.SetActive(false);
        }
        else // atidarom
        {
            expanded = true;
            DisabledObjectsGameScene.Instance.Sellbutton_options.SetActive(true);
        }
    }

    public void CancelContext() //kai ateina requestas uzdaryti visus contexts, force close
    {
        if (expanded)
        {
            expanded = false;
            DisabledObjectsGameScene.Instance.Sellbutton_options.SetActive(false);
        }
    }
}