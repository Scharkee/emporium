using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    private Text dollarText;
    private Text usernameText;
    private Text apelsinaiText;

    public GameObject PressContextPanel;

    // Use this for initialization
    private void Start()
    {
        DisablePanels();
    }

    private void Update()
    {
    }

    private void Awake()
    {
        Instance = this;
        //grab all panels
        PressContextPanel = GameObject.Find("PressContextPanel");
    }

    public void ChangeUIText(string TextObjName, string newtext)
    {
        Text text = GameObject.Find(TextObjName).GetComponent<Text>();

        text.text = newtext;
    }

    private void DisablePanels()
    {// disable all panels at start
        PressContextPanel.SetActive(false);
    }
}