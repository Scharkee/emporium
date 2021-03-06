﻿using SocketIO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BugReporter : MonoBehaviour
{
    public bool BugReporterOpen = false;

    private void Start()
    {
        DisabledObjectsMain.Instance.socket.On("RECEIVED_BUGREPORT", reportConfirmation);
    }

    public void TheClick()
    {
        ClickEngine.Instance.Click();
        if (!BugReporterOpen)
        {
            BugReporterOpen = true;
            DisabledObjectsMain.Instance.ResetLoginButton();
            StartCoroutine(ManageBugReporter(true));
        }
        else
        {
            BugReporterOpen = false;
            StartCoroutine(ManageBugReporter(false));
        }
    }

    public IEnumerator ManageBugReporter(bool open)
    {
        if (open)
        {
            DisabledObjectsMain.Instance.BugReportPanel.SetActive(open);
            DisabledObjectsMain.Instance.Menumusic.HaltBeats = true;
        }

        if (open)
        {
            while (DisabledObjectsMain.Instance.BugReportPanel.GetComponent<CanvasGroup>().alpha <= 0.99f)
            {
                yield return new WaitForSeconds(0.005f);
                DisabledObjectsMain.Instance.BugReportPanel.GetComponent<CanvasGroup>().alpha += 0.1f;
            }
        }
        else
        {
            while (DisabledObjectsMain.Instance.BugReportPanel.GetComponent<CanvasGroup>().alpha > 0f)
            {
                yield return new WaitForSeconds(0.005f);
                DisabledObjectsMain.Instance.BugReportPanel.GetComponent<CanvasGroup>().alpha -= 0.1f;
            }
        }

        if (!open)
        {
            DisabledObjectsMain.Instance.BugReportPanel.SetActive(open);
            DisabledObjectsMain.Instance.Menumusic.HaltBeats = false;
        }
    }

    private void reportConfirmation(SocketIOEvent evt)
    {
        Debug.Log("report successfull");
    }

    public void SendReport(string report)
    {
        Dictionary<string, string> data = new Dictionary<string, string>();

        data["report"] = report;

        DisabledObjectsMain.Instance.socket.Emit("SUBMIT_BUGREPORT", new JSONObject(data));
    }

    public void SubmitReport()
    {
        ClickEngine.Instance.Click();
        string str = DisabledObjectsMain.Instance.ReportInputField.GetComponent<InputField>().text;
        SendReport(str);
        Debug.Log(str);
        DisabledObjectsMain.Instance.ReportInputField.GetComponent<InputField>().text = "";

        StartCoroutine(DisabledObjectsMain.Instance.ShowMessage(Languages.Instance.currentLanguage["bug_report_success"], DisabledObjectsMain.Instance.NormalTextColor, 2f));
        CancelReporting();
    }

    public void CancelReporting()
    {
        DisabledObjectsMain.Instance.ReportInputField.GetComponent<InputField>().text = "";
        TheClick();
    }
}