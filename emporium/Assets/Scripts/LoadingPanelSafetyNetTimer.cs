using UnityEngine;

public class LoadingPanelSafetyNetTimer : MonoBehaviour
{
    private float connectionFailureTimer = 4f;

    // Update is called once per frame
    private void Update()
    {
        connectionFailureTimer -= Time.deltaTime;

        if (connectionFailureTimer <= 0)
        {
            StartCoroutine(DisabledObjectsGameScene.Instance.SocketManager.logOffWithDelay(0f));
        }
    }
}