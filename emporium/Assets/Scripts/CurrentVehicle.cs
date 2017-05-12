using UnityEngine;
using UnityEngine.UI;

public class CurrentVehicle : MonoBehaviour
{
    public Text currentVehichle;

    private void Start()
    {
        currentVehichle = GetComponent<Text>();
    }

    private void OnEnable()
    {
        try
        {
            currentVehichle.text = Database.Instance.CurrentVehichle.Name;
        }
        catch
        {
        }
    }
}