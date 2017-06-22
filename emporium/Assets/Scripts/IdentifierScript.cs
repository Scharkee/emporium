using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class IdentifierScript : MonoBehaviour
{
    //apsibreziami pag. objektai

    private GameObject Identpanel;
    private LoginCheck logincheck;
    private GameObject Networkman;
    private GameObject GlobalObj;
    private GameObject globalobje;
    private LoginCheck logcheck;

    //for fading text
    public TextMesh textmesh;

    public Color def;

    private Text connectingText;

    public bool create = false;

    //new

    private InputField inputfplaceholder;
    private Text Unamepasstext;

    // Use this for initialization
    private void Start()
    {
        //TODO:  inputf = GameObject.Find("logintextfield").GetComponent<InputField>();
        globalobje = GameObject.Find("GlobalObject");

        logcheck = gameObject.GetComponent<LoginCheck>();
        connectingText = GameObject.Find("ConnectingText").GetComponent<Text>();
    }

    public void setPlayerInfo(string val)
    {
        InputField inpf = DisabledObjectsMain.Instance.UnamePassInputField.GetComponent<InputField>();

        if (GlobalControl.Instance.Logincount == 1)
        {
            if (val != "")
            {
                GlobalControl.Instance.Uname = val;
                GlobalControl.Instance.Logincount++;
                DisabledObjectsMain.Instance.UnamePassText.GetComponent<Text>().text = GlobalControl.Instance.currentLangDict["enter_password"];
            }
            inpf.ActivateInputField();
            inpf.Select();
        }
        else if (GlobalControl.Instance.Logincount == 2)
        {
            if (val != "")
            {
                GlobalControl.Instance.Pass = val;
                GlobalControl.Instance.Logincount++;
                Debug.Log(GlobalControl.Instance.Logincount);

                inpf.text = "";

                logcheck.LogInCh(GlobalControl.Instance.Uname, GlobalControl.Instance.Pass);
            }

            inpf.ActivateInputField();
            inpf.Select();
        }

        inpf.text = "";
    }

    private IEnumerator BlinkConnecting()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            connectingText.color = Color.Lerp(Color.grey, Color.black, Mathf.PingPong(Time.time * 2, 1));
        }
    }
}