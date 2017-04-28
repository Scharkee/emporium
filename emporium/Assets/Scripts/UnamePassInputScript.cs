using UnityEngine;
using System.Collections;

using UnityEngine.UI;



public class UnamePassInputScript : MonoBehaviour
{

    IdentifierScript identscr;

    // Use this for initialization
    void Start()
    {


        identscr = GameObject.Find("IdentifierPanel").GetComponent<IdentifierScript>();
        InputField input = gameObject.GetComponent<InputField>();
        InputField.SubmitEvent se = new InputField.SubmitEvent();
        se.AddListener(pushInput);
        input.onEndEdit = se;

        input.ActivateInputField();
        input.Select();



    }

    // Update is called once per frame
    void Update()
    {

    }

    void pushInput(string val)
    {
        identscr.setPlayerInfo(val);

    }
}
