using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EconomyButtonScript : MonoBehaviour {

	

    public void TheClick()
    {
        
            DisabledObjectsGameScene.Instance.EconomyPanel.SetActive(!DisabledObjectsGameScene.Instance.EconomyPanel.activeSelf);
        


    }
}
