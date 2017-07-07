using SocketIO;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityStandardAssets.ImageEffects;

public class LoginCheck : MonoBehaviour
{
    public string inputusername;
    public string inputpassword;
    private bool gotResponse = false;
    public SocketIOComponent socket;

    // Use this for initialization
    private void Start()
    {
        socket = GlobalControl.Instance.gameObject.GetComponent<SocketIOComponent>();
        socket.On("PASS_CHECK_CALLBACK", OnLoginCheckCallback);
    }

    private void checkLoginDetails(string username, string password)
    {
        Dictionary<string, string> data = new Dictionary<string, string>();
        data["Uname"] = username;
        data["Upass"] = password;
        socket.Emit("CHECK_LOGIN", new JSONObject(data));
    }

    private void OnLoginCheckCallback(SocketIOEvent evt)
    {
        loginorCreate(int.Parse(evt.data.GetField("passStatus").ToString()));
    }

    public void LogInCh(string un, string pass)
    {
        checkLoginDetails(un, pass);
    }

    private void loginorCreate(int stat)
    {
        gotResponse = true;
        if (stat == 0)
        {  //pass incorrect
            Reaskforlogininfo();
        }
        else if (stat == 1)
        { //pass correct
            proceedToGameScene();
        }
        else if (stat == 2)
        { //user not in DB
            noUser();
        }
        else if (stat == 3) //reimplement kad net neparodytu main screen jei pareina connection is kito PC;
        {//user already logged in from another PC.
            StartCoroutine(DisabledObjectsMain.Instance.ShowMessage(Languages.Instance.currentLanguage["already_logged_in_message"], DisabledObjectsMain.Instance.NormalTextColor, 3f));

            clearLoginInfo();
        }
        else if (stat == 4)
        {//user banned
            StartCoroutine(DisabledObjectsMain.Instance.ShowMessage(Languages.Instance.currentLanguage["banned_message"], DisabledObjectsMain.Instance.NormalTextColor, 3f));

            clearLoginInfo();
        }
    }

    private void proceedToGameScene()
    {
        StartCoroutine(LVLLoadCamEffect());
        SceneManager.LoadScene("GameScene");
    }

    private void Reaskforlogininfo()
    {
        Debug.Log("REASKING FOR LOGIN INFO");
        GlobalControl.Instance.Pass = null;
        GlobalControl.Instance.Uname = null;
        GlobalControl.Instance.Logincount = 1;
        DisabledObjectsMain.Instance.UnamePassInputField.GetComponent<InputField>().text = "";
        DisabledObjectsMain.Instance.UnamePassText.GetComponent<Text>().text = Languages.Instance.currentLanguage["enter_username"];

        StartCoroutine(notifyText(GameObject.Find("UnamePassText"), Languages.Instance.currentLanguage["wrong_password"], DisabledObjectsMain.Instance.RedTextColor));
    }

    private void clearLoginInfo()
    {
        GlobalControl.Instance.Pass = null;
        GlobalControl.Instance.Uname = null;
        GlobalControl.Instance.Logincount = 1;
        DisabledObjectsMain.Instance.UnamePassInputField.GetComponent<InputField>().text = "";
        DisabledObjectsMain.Instance.UnamePassText.GetComponent<Text>().text = Languages.Instance.currentLanguage["enter_username"];
    }

    private void noUser()
    {
        Debug.Log("asking to create a user");
        GlobalControl.Instance.Pass = null;
        GlobalControl.Instance.Uname = null;
        GlobalControl.Instance.Logincount = 1;
        DisabledObjectsMain.Instance.UnamePassInputField.GetComponent<InputField>().text = "";

        StartCoroutine(notifyText(GameObject.Find("UnamePassText"), "No user found.", DisabledObjectsMain.Instance.BlueTextColor));
    }

    private IEnumerator notifyText(GameObject txtobject, string notification, Color notificationColor)
    {
        string Oldtext = txtobject.GetComponent<Text>().text;

        txtobject.GetComponent<Text>().text = notification;
        txtobject.GetComponent<Text>().color = notificationColor;

        yield return new WaitForSeconds(1.5f);

        txtobject.GetComponent<Text>().text = "Enter your username:";
        txtobject.GetComponent<Text>().color = DisabledObjectsMain.Instance.NormalTextColor;
    }

    private IEnumerator LVLLoadCamEffect()
    {
        while (Camera.main.GetComponent<Bloom>().bloomThreshold > 0.01)
        {
            //fadeoutas
            yield return new WaitForSeconds(0.001f);
            Globals.Instance.cameraBloom.bloomThreshold -= 0.01f;
        }
    }
}