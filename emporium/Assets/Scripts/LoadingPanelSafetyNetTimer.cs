using UnityEngine;

public class LoadingPanelSafetyNetTimer : MonoBehaviour
{
    private float connectionFailureTimer = 4f;

    private void Update()
    {
        connectionFailureTimer -= Time.deltaTime;

        if (connectionFailureTimer <= 0)
        {
            StartCoroutine(DisabledObjectsGameScene.Instance.SocketManager.logOffWithDelay(0f));
        }
    }
}