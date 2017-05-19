using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PressContextPanelScript : MonoBehaviour
{
    public bool aliveForHalfSec;
    public GameObject Press_AssignJob_InputField;
    public GameObject Press_AssignJob_ProdTypeDropdown;
    public BuildingScript activePress;

    // Use this for initialization
    private void Start()
    {
        aliveForHalfSec = false;
        StartCoroutine(waitForALittleBeforeAllowingCancellationsOfPanel());
    }

    private void OnEnable()
    {
        aliveForHalfSec = false;
        StartCoroutine(waitForALittleBeforeAllowingCancellationsOfPanel());
    }

    public void AcceptPanelInput()
    {
        InputField amounttext = transform.Find("Press_AssignJob_InputField").GetComponent<InputField>();

        if (amounttext.text.Length >= 1 && amounttext.text.Length <= 100000000)
        {
            if (amounttext.text == "")
            {
                Debug.Log("nothing entered,. try again");
            }
            else
            {
                int workAmount = int.Parse(amounttext.text);
                string workName = IDHelper.Instance.PressContextPanelIDtoName(GameObject.Find("Press_AssignJob_ProdTypeDropdown").GetComponent<Dropdown>().value);

                PressWorkPKG pkg;    //sukuriaamas struct nes tik 1 parameter i broadcasta leidziama det
                pkg.workAmount = workAmount;
                pkg.workName = workName;

                DisabledObjectsGameScene.Instance.Tiles.BroadcastMessage("ReceiveWork", pkg);
            }
        }
    }

    private IEnumerator waitForALittleBeforeAllowingCancellationsOfPanel()
    {
        yield return new WaitForSeconds(0.5f);
        aliveForHalfSec = true;
    }

    public void CancelContext() //parejo broadcastas, isjungti VISUS context panels
    {
        if (aliveForHalfSec)
        {
            gameObject.SetActive(false);
        }
    }

    public void KeepAtMaxValues(string str)
    {
        //kad negaletu parduot daugiau negu turi.
        if (float.Parse(Press_AssignJob_InputField.GetComponent<InputField>().text) > activePress.thistileInfo.TILEPRODUCERANDOM2)
        {
            Press_AssignJob_InputField.GetComponent<InputField>().text = activePress.thistileInfo.TILEPRODUCERANDOM2.ToString();
            //maxed out effect
        }
        else if (float.Parse(Press_AssignJob_InputField.GetComponent<InputField>().text) < activePress.thistileInfo.TILEPRODUCERANDOM1)
        {
            Press_AssignJob_InputField.GetComponent<InputField>().text = activePress.thistileInfo.TILEPRODUCERANDOM1.ToString();
        }

        //dar paziurim ar yra tiek inventoriuose

        if ((float.Parse(Press_AssignJob_InputField.GetComponent<InputField>().text) > Database.Instance.Inventory[IDHelper.Instance.PressContextPanelIDtoName(Press_AssignJob_ProdTypeDropdown.GetComponent<Dropdown>().value)]))
        {//per daug ivesta
            Press_AssignJob_InputField.GetComponent<InputField>().text = Database.Instance.Inventory[IDHelper.Instance.PressContextPanelIDtoName(Press_AssignJob_ProdTypeDropdown.GetComponent<Dropdown>().value)] + "";
        }
    }
}

[System.Serializable]
public struct PressWorkPKG
{
    public string workName;
    public int workAmount;
}