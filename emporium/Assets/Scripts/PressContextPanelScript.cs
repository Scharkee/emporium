using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PressContextPanelScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	


    public void AcceptPanelInput()
    {
        InputField amounttext = transform.FindChild("Press_AssignJob_InputField").GetComponent<InputField>();

        if (amounttext.text.Length >=1 && amounttext.text.Length <= 100000000 )
        {
            if(amounttext.text == "")
            {
                Debug.Log("nothing entered,. try again");
            }else
            {
        
                int workAmount = int.Parse(amounttext.text);       
                string workName = IDHelper.PressContextPanelIDtoName(GameObject.Find("Press_AssignJob_ProdTypeDropdown").GetComponent<Dropdown>().value);

                PressWorkPKG pkg;    //sukuriaamas struct nes tik 1 parameter i broadcasta leidziama det
                pkg.workAmount = workAmount;
                pkg.workName = workName;

                DisabledObjectsGameScene.Tiles.BroadcastMessage("ReceiveWork", pkg);
              
            }


        }






    }


}


[System.Serializable]
public struct PressWorkPKG{
    public string workName;
    public int workAmount;



}
