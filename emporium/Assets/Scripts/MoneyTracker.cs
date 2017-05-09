using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyTracker : MonoBehaviour
{

    float lastDollars;
    bool moneyChanging=false;

    // Update is called once per frame
    void Update()
    {
        if (Database.Instance.UserDollars != lastDollars)
        {
            lastDollars = Database.Instance.UserDollars;

            if (moneyChanging)
            {
                StopAllCoroutines();
                StartCoroutine(changeMoney_Effect(lastDollars));

            }
            else
            {
                moneyChanging = true;
                StartCoroutine(changeMoney_Effect(lastDollars));
            }


        }



    }


    IEnumerator changeMoney_Effect(float newMoney)
    {



        if (newMoney == float.Parse(DisabledObjectsGameScene.Instance.moneyEdit.GetComponent<Text>().text)) { Debug.Log("error, shouldnt happen, only on first load technically"); }
        else if (newMoney > float.Parse(DisabledObjectsGameScene.Instance.moneyEdit.GetComponent<Text>().text))
        {
            while (float.Parse(DisabledObjectsGameScene.Instance.moneyEdit.GetComponent<Text>().text) < newMoney)
            {
                yield return new WaitForSeconds(0.01f);
                DisabledObjectsGameScene.Instance.moneyEdit.GetComponent<Text>().text = Mathf.Lerp(float.Parse(DisabledObjectsGameScene.Instance.moneyEdit.GetComponent<Text>().text), newMoney, 0.1f).ToString();

            }


        }
        else if (newMoney < float.Parse(DisabledObjectsGameScene.Instance.moneyEdit.GetComponent<Text>().text))
        {

            while (float.Parse(DisabledObjectsGameScene.Instance.moneyEdit.GetComponent<Text>().text) > newMoney)
            {
                yield return new WaitForSeconds(0.01f);
                DisabledObjectsGameScene.Instance.moneyEdit.GetComponent<Text>().text = Mathf.Lerp(float.Parse(DisabledObjectsGameScene.Instance.moneyEdit.GetComponent<Text>().text), newMoney, 0.1f).ToString();

            }


        }


        moneyChanging = false;


    }


}
