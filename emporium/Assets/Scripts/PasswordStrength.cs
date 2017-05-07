using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PasswordStrength : MonoBehaviour
{

    public InputField passInp;
    public Text RegisterPassText3;
    public Image PasswordStrenthMeterFG;
    public Color GoodEnough, NotGoodEnough,Strong;



    public void AdaptPasswordStrengthMeter()
    {

        if (passInp.text == "")
        {
            RegisterPassText3.text = "";

        }
        else if (passInp.text.Length >= 12)
        {
            PasswordStrenthMeterFG.color = Strong;
            RegisterPassText3.text = "Strong";
            PasswordStrenthMeterFG.fillAmount = 1;

        }
        else if (passInp.text.Length >= 8)
        {
            PasswordStrenthMeterFG.fillAmount = 1;
            PasswordStrenthMeterFG.color = GoodEnough;
            RegisterPassText3.text = "Passable";

        }
        else if (passInp.text.Length < 8)
        {
            PasswordStrenthMeterFG.color = NotGoodEnough;

            float fillAmmount = 0.125f * passInp.text.Length;
            PasswordStrenthMeterFG.fillAmount = fillAmmount;
       
            RegisterPassText3.text = "Too Weak";

        }




    }
}
