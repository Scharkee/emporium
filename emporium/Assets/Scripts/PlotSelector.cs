using UnityEngine;

public class PlotSelector : MonoBehaviour
{
    public GameObject PlotSelectorPrefab;

    // Use this for initialization
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }

    public void SpawnPlotSelectors()
    {
        int plotsz = Database.Instance.UserPlotSize;
        float currentX = -0.5f;
        float currentZ = 0.5f;

        while (Physics.CheckSphere(new Vector3(currentX, -0.07f, currentZ), 0.3f))
        {
            currentZ--;
        }

        currentZ++;

        while (Physics.CheckSphere(new Vector3(currentX, -0.07f, currentZ), 0.1f))
        {
            currentX--;
        }

        currentX++;

        int Xcount = 1;
        int Zcount = 1;
        float startingX = currentX;

        while (Zcount <= plotsz)
        {
            while (Xcount <= plotsz)
            {
                GameObject currentplot = Instantiate(PlotSelectorPrefab, new Vector3(currentX, 0.1f, currentZ), Quaternion.identity, DisabledObjectsGameScene.Instance.PlotSelectors.transform) as GameObject;
                currentplot.transform.localScale = new Vector3(0.65f, 0.0234f, 0.65f);

                currentX++;

                Xcount++;
            }

            currentX = startingX;
            Xcount = 1;
            currentZ++;
            Zcount++;
        }

        DisabledObjectsGameScene.Instance.PlotSelectors.SetActive(false);
        DisabledObjectsGameScene.Instance.RealGround.SetActive(true);
        DisabledObjectsGameScene.Instance.PlaceholderGround.SetActive(false);
    }
}