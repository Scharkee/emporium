using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RegisterScript : MonoBehaviour
{
    public bool RegisterPanelOpen = false;
    public InputField usernameInput;
    public InputField passwordInput;
    public InputField emailInput;

    public Text RequiredAlertTextEM;
    public Text RequiredAlertTextPSW;
    public Text RequiredAlertTextUN;

    public void TheClick()
    {
        ClickEngine.Instance.Click();
        if (!RegisterPanelOpen)
        {
            RegisterPanelOpen = true;
            DisabledObjectsMain.Instance.ResetLoginButton();
            StartCoroutine(ManageRegisterPanel(true));
        }
        else
        {
            RegisterPanelOpen = false;
            StartCoroutine(ManageRegisterPanel(false));
        }
    }

    public IEnumerator ManageRegisterPanel(bool open)
    {
        if (open)
        {
            DisabledObjectsMain.Instance.RegisterPanel.SetActive(open);
            DisabledObjectsMain.Instance.Menumusic.HaltBeats = true;
        }

        if (open)
        {
            while (DisabledObjectsMain.Instance.RegisterPanel.GetComponent<CanvasGroup>().alpha <= 0.99f)
            {
                yield return new WaitForSeconds(0.005f);
                DisabledObjectsMain.Instance.RegisterPanel.GetComponent<CanvasGroup>().alpha += 0.1f;
            }
        }
        else
        {
            while (DisabledObjectsMain.Instance.RegisterPanel.GetComponent<CanvasGroup>().alpha > 0f)
            {
                yield return new WaitForSeconds(0.005f);
                DisabledObjectsMain.Instance.RegisterPanel.GetComponent<CanvasGroup>().alpha -= 0.1f;
            }
        }

        if (!open)
        {
            DisabledObjectsMain.Instance.RegisterPanel.SetActive(open);
            DisabledObjectsMain.Instance.Menumusic.HaltBeats = false;
        }
    }

    public void SendRegisterForm(string Username, string Password, string Email)
    {
        Dictionary<string, string> data = new Dictionary<string, string>();

        data["Uname"] = Username;
        data["Upass"] = Password;
        data["Email"] = Email;

        DisabledObjectsMain.Instance.socket.Emit("REGISTER_USER", new JSONObject(data));
    }

    public void CancelRegistering()
    {
        //reset registry fields

        usernameInput.text = "";
        passwordInput.text = "";
        emailInput.text = "";
        TheClick();
    }

    public void SubmitRegisterForm()
    {
        ClickEngine.Instance.Click();
        if (usernameInput.text.Length == 0) // no username
        {
            RequiredAlertTextUN.gameObject.SetActive(true);
        }
        else if (passwordInput.text.Length == 0) // no password
        {
            RequiredAlertTextPSW.gameObject.SetActive(true);
        }
        else if (emailInput.text.Length == 0) // no email
        {
            RequiredAlertTextEM.gameObject.SetActive(true);
        }
        else if (usernameInput.text.Length < 3) // username too short
        {
            RequiredAlertTextUN.gameObject.SetActive(true);
            RequiredAlertTextUN.text = "Too Short!";
        }
        else if (passwordInput.text.Length < 8) // pass too short
        {
            RequiredAlertTextPSW.gameObject.SetActive(true);
            RequiredAlertTextPSW.text = "Too Short!";
        }
        else if (emailInput.text.Length < 6) // email too short
        {
            RequiredAlertTextEM.gameObject.SetActive(true);
            RequiredAlertTextEM.text = "Too Short!";
        }
        else
        {
            string un = usernameInput.text;
            string psw = passwordInput.text;
            string email = emailInput.text;

            SendRegisterForm(un, psw, email);

            StartCoroutine(DisabledObjectsMain.Instance.ShowMessage("Success! Check your email to confirm your account.", DisabledObjectsMain.Instance.NormalTextColor, 3f));
            CancelRegistering();
        }
    }
}