using UnityEngine;

using UnityEngine.UI;

public class UnamePassInputScript : MonoBehaviour
{
    private IdentifierScript identscr;

    // Use this for initialization
    private void Start()
    {
        identscr = GameObject.Find("IdentifierPanel").GetComponent<IdentifierScript>();
        InputField input = gameObject.GetComponent<InputField>();
        InputField.SubmitEvent se = new InputField.SubmitEvent();
        se.AddListener(pushInput);
        input.onEndEdit = se;

        input.ActivateInputField();
        input.Select();
    }

    private void Update()
    {
    }

    private void pushInput(string val)
    {
        identscr.setPlayerInfo(val);
    }
}