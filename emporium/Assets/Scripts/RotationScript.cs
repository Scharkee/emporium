using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;


public class RotationScript : MonoBehaviour {
    PlayerInfoLoaderBank playerinfobank;
    Vector3 nelygusPlotCenter;
    Vector3 lygusPlotCenter;
    Vector3 currentCenter;
    private float rot;
    private float rotOld; //stores the rotation of the mouse wheel
    public float speed;      //multiplier for the mouse wheel input
    private Vector3 storePos; //stores the rotation of the attached gameObject


    private Vector3 axispoint;
    // Use this for initialization
    void Start () {
        playerinfobank = GameObject.Find("_ManagerialScripts").GetComponent<PlayerInfoLoaderBank>();
        nelygusPlotCenter = new Vector3(-0.5f,0,0.5f);
        lygusPlotCenter = Vector3.zero;

        axispoint = new Vector3(0,0,0);
        Debug.Log(currentCenter);
        StartCoroutine(SpawnInCamEffect());
        speed = 10000.0f;
        storePos = gameObject.transform.eulerAngles;  //keeps storePos up to date 


    }
	
	// Update is called once per frame
	void Update ()
    {
        transform.LookAt(axispoint);

        rotOld = rot;

        rot = Input.GetAxis("Mouse ScrollWheel") * speed * Time.deltaTime;

        rot = Mathf.LerpAngle(rotOld, rot, 0.03f);
        transform.RotateAround(currentCenter, Vector3.up, rot);

    
    }

    public void SetCurrentRotCenter(bool lygnelyg)
    {
        if (lygnelyg)
        {
            gameObject.transform.position=(new Vector3(0f, 1.63f, -3.8f));//cam pos
            GameObject.Find("Ground").transform.position = new Vector3(0f, -0.059f, 0f);//ground pos
            currentCenter = lygusPlotCenter;
        }
        else {
            gameObject.transform.position=(new Vector3(-0.5f, 1.63f, -3.8f));//cam pos
            GameObject.Find("Ground").transform.position = new Vector3(-0.5f, -0.059f, 0.5f);//ground pos
            currentCenter = nelygusPlotCenter;
        }


    }

    IEnumerator SpawnInCamEffect()
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
