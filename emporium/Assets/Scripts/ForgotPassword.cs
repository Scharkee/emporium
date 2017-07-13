using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ForgotPassword : MonoBehaviour
{
    public bool ForgotPasswordPanelOpen = false;
    public InputField emailInput;
    public Text RequiredAlertTextEM;

    public void TheClick()
    {
        ClickEngine.Instance.Click();
        if (!ForgotPasswordPanelOpen)
        {
            ForgotPasswordPanelOpen = true;
            DisabledObjectsMain.Instance.ResetLoginButton();
            StartCoroutine(ManageForgotPasswordPanel(true));
        }
        else
        {
            ForgotPasswordPanelOpen = false;
            StartCoroutine(ManageForgotPasswordPanel(false));
        }
    }

    public IEnumerator ManageForgotPasswordPanel(bool open)
    {
        if (open)
        {
            DisabledObjectsMain.Instance.ForgotPassPanel.SetActive(open);
            DisabledObjectsMain.Instance.Menumusic.HaltBeats = true;
        }

        if (open)
        {
            while (DisabledObjectsMain.Instance.ForgotPassPanel.GetComponent<CanvasGroup>().alpha <= 0.99f)
            {
                yield return new WaitForSeconds(0.005f);
                DisabledObjectsMain.Instance.ForgotPassPanel.GetComponent<CanvasGroup>().alpha += 0.1f;
            }
        }
        else
        {
            while (DisabledObjectsMain.Instance.ForgotPassPanel.GetComponent<CanvasGroup>().alpha > 0f)
            {
                yield return new WaitForSeconds(0.005f);
                DisabledObjectsMain.Instance.ForgotPassPanel.GetComponent<CanvasGroup>().alpha -= 0.1f;
            }
        }

        if (!open)
        {
            DisabledObjectsMain.Instance.ForgotPassPanel.SetActive(open);
            DisabledObjectsMain.Instance.Menumusic.HaltBeats = false;
        }
    }

    public void SubmitPasswordReset()
    {
        ClickEngine.Instance.Click();
        if (emailInput.text.Length == 0) // no email
        {
            RequiredAlertTextEM.gameObject.SetActive(true);
        }
        else if (emailInput.text.Length < 6) // email too short
        {
            RequiredAlertTextEM.gameObject.SetActive(true);
            RequiredAlertTextEM.text = Languages.Instance.currentLanguage["email_too_short"];
        }
        else
        {
            AskForPasswordReset(emailInput.text);

            Debug.Log("doing the message");
            StartCoroutine(DisabledObjectsMain.Instance.ShowMessage(Languages.Instance.currentLanguage["password_reset_email_sent"], DisabledObjectsMain.Instance.NormalTextColor, 2f));

            CancelForgotPassword();
        }
    }

    public void AskForPasswordReset(string email)
    {
        Dictionary<string, string> data = new Dictionary<string, string>();

        data["Email"] = email;

        DisabledObjectsMain.Instance.socket.Emit("FORGOT_PASS", new JSONObject(data));
    }

    public void CancelForgotPassword()
    {
        emailInput.text = "";

        TheClick();
    }
}