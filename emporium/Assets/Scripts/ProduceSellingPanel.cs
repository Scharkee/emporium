using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProduceSellingPanel : MonoBehaviour
{
    public void CancelContext()
    {
        DisabledObjectsGameScene.Instance.managerialScripts.GetComponent<ProduceSelling>().CancelContext();
    }
}