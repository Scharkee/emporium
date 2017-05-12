using System.Collections;
using UnityEngine;
using UnityStandardAssets.ImageEffects;

public class CameraController : MonoBehaviour
{
    private PlayerInfoLoaderBank playerinfobank;
    private Vector3 nelygusPlotCenter;
    private Vector3 lygusPlotCenter;
    public Vector3 currentCenter;
    private float rot;
    private float rotOld; //stores the rotation of the mouse wheel
    public float speed;      //multiplier for the mouse wheel input
    private Vector3 storePos; //stores the rotation of the attached gameObject

    private bool inHeightTransition = false;
    private bool cameraUp = false;

    private BuyButtonScript buybuttonscript;

    private Vector3 axispoint;

    // Use this for initialization
    private void Start()
    {
        playerinfobank = GameObject.Find("_ManagerialScripts").GetComponent<PlayerInfoLoaderBank>();
        nelygusPlotCenter = new Vector3(-0.5f, 0, 0.5f);
        lygusPlotCenter = Vector3.zero;

        axispoint = new Vector3(0, 0, 0);

        StartCoroutine(SpawnInCamEffect());
        speed = 10000.0f;
        storePos = gameObject.transform.eulerAngles;  //keeps storePos up to date

        buybuttonscript = GameObject.Find("BuyButton").GetComponent<BuyButtonScript>();
    }

    // Update is called once per frame
    private void Update()
    {
        transform.LookAt(currentCenter);

        rotOld = rot;

        if (!buybuttonscript.panelEnabled)
        {
            rot = Input.GetAxis("Mouse ScrollWheel") * speed * Time.deltaTime;
        }
        else { rot = 0; }

        rot = Mathf.LerpAngle(rotOld, rot, 0.03f);

        transform.RotateAround(currentCenter, Vector3.up, rot);
    }

    private IEnumerator raiseCamera()
    {
        if (inHeightTransition)
        {
            yield return new WaitForSeconds(0.1f);
        }
        else
        {
            inHeightTransition = true; //uzsilockinam, kad nieks neciupinetu cameros

            if (!Globals.Instance.cameraUp) //keliam
            {
                RenderSettings.skybox = Globals.Instance.dark_skybox;
                while (Camera.main.transform.position.y < Database.Instance.UserPlotSize * 1.4f)
                {
                    yield return new WaitForSeconds(0.001f);

                    // 3 plotsize tinka 3.28f elevation, todel 6 plotsize tinka 6.56, 1 = 1.0933.

                    Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, new Vector3(Camera.main.transform.position.x, Database.Instance.UserPlotSize * 1.5f, Camera.main.transform.position.z), Time.deltaTime * 10);
                }

                Globals.Instance.cameraUp = true;
            }
            else
            {
                RenderSettings.skybox = Globals.Instance.light_skybox;
                while (Camera.main.transform.position.y > Database.Instance.UserPlotSize * 0.8f)
                {
                    yield return new WaitForSeconds(0.0001f);

                    //3=1.63, 1=0.5433f
                    Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, new Vector3(Camera.main.transform.position.x, Database.Instance.UserPlotSize * 0.7f, Camera.main.transform.position.z), Time.deltaTime * 10);
                }

                Globals.Instance.cameraUp = false;
            }

            inHeightTransition = false;
        }
    }

    public void PerformCamElevetion()
    {
        StartCoroutine(raiseCamera());
    }

    public void SetCurrentRotCenter(bool lygnelyg)
    {
        if (lygnelyg)
        {
            gameObject.transform.position = (new Vector3(0f, 1.63f, -3.8f));//cam pos
            GameObject.Find("Ground").transform.position = new Vector3(0f, -0.059f, 0f);//ground pos
            currentCenter = lygusPlotCenter;
        }
        else
        {
            gameObject.transform.position = (new Vector3(-0.5f, 1.63f, -3.8f));//cam pos
            GameObject.Find("Ground").transform.position = new Vector3(-0.5f, -0.059f, 0.5f);//ground pos
            currentCenter = nelygusPlotCenter;
        }
    }

    private IEnumerator SpawnInCamEffect()
    {
        Vortex vortexscript = GetComponent<Vortex>();
        Bloom bloom = GetComponent<Bloom>();
        while (bloom.bloomThreshold < 0.5)
        {
            //fadeoutas
            yield return new WaitForSeconds(0.001f);
            bloom.bloomThreshold = bloom.bloomThreshold + 0.01f;
        }
    }
}