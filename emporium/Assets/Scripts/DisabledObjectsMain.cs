using SocketIO;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DisabledObjectsMain : MonoBehaviour
{
    public static DisabledObjectsMain Instance;

    public GameObject SubmitButton;
    public GameObject UnamePassInputField;
    public GameObject UnamePassText;
    public GameObject MainCanvas;
    public GameObject titleText;
    public GameObject LoginButton;
    public GameObject BugReportPanel;
    public GameObject BugReportButton;
    public GameObject RegisterPanel;
    public GameObject RegisterButton;
    public GameObject ReportInputField;
    public GameObject ForgotPassPanel;
    public GameObject ForgotPassButton;
    public GameObject MessageText;
    public GameObject MuteButton;
    public GameObject ContactButton;
    public GameObject LangDropDown;
    public GameObject TempLogButton;
    public GameObject FullScreenMessagePanel;
    public MenuMusic Menumusic;
    public GameObject ConnectingText;
    public Color NormalTextColor, RedTextColor, BlueTextColor;

    public SocketIOComponent socket;

    public Color blueish;
    public Color defaultcolor;

    // Use this for initialization
    private void Start()
    {
        SubmitButton.SetActive(false);
        UnamePassInputField.SetActive(false);
        UnamePassText.SetActive(false);
    }

    public void ResetLoginButton()
    {
        LoginButton.SetActive(true);
        UnamePassInputField.SetActive(false);
        SubmitButton.SetActive(false);
        UnamePassText.SetActive(false);
    }

    public IEnumerator ShowMessage(string message, Color color, float waitAmount)
    {
        while (DisabledObjectsMain.Instance.FullScreenMessagePanel.activeSelf) // mesage is already being shown. Wait.
        {
            yield return new WaitForSeconds(0.4f);
        }

        DisabledObjectsMain.Instance.FullScreenMessagePanel.SetActive(true);
        DisabledObjectsMain.Instance.MessageText.GetComponent<Text>().text = message;
        DisabledObjectsMain.Instance.MessageText.GetComponent<Text>().color = color;

        while (DisabledObjectsMain.Instance.FullScreenMessagePanel.GetComponent<CanvasGroup>().alpha <= 0.99f)
        {
            yield return new WaitForSeconds(0.01f);
            DisabledObjectsMain.Instance.FullScreenMessagePanel.GetComponent<CanvasGroup>().alpha += 0.07f;
        }

        yield return new WaitForSeconds(waitAmount);

        while (DisabledObjectsMain.Instance.FullScreenMessagePanel.GetComponent<CanvasGroup>().alpha > 0f)
        {
            yield return new WaitForSeconds(0.01f);
            DisabledObjectsMain.Instance.FullScreenMessagePanel.GetComponent<CanvasGroup>().alpha -= 0.07f;
        }

        DisabledObjectsMain.Instance.FullScreenMessagePanel.SetActive(false);
    }

    private void Awake()
    {
        Instance = this;
    }
}