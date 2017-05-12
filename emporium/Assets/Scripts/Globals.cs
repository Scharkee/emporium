using UnityEngine;
using UnityStandardAssets.ImageEffects;

public class Globals : MonoBehaviour
{
    public static Globals Instance;

    public bool cameraUp = false;
    public GameObject canvas;

    //skybox materials
    public Material light_skybox;

    public Material dark_skybox;

    //camera effect references
    public BlurOptimized cameraBlur;

    public Bloom cameraBloom;

    //plotselector mat refs

    public Material plotselector_standard;
    public Material plotselector_upgradeable;
    public Material plotselector_upgradeable_mouseover;
    public Material plotselector_unavailable;
    public Material plotselector_standard_mouseover;

    //colors
    public Color buttonActiveColor1;

    public Color buttonColor1;

    public Color RedTextColor;
    public Color WhiteTextColor;

    public Color RedNormalProduceAlertColorr;
    public Color NormalProduceAlertColor;

    // Use this for initialization
    private void Start()
    {
        dark_skybox = Resources.Load("Materials/Skybox_mat_darkened") as Material;
        light_skybox = RenderSettings.skybox;
    }

    private void Awake()
    {
        Instance = this;
        cameraBlur = Camera.main.GetComponent<BlurOptimized>();
        cameraBloom = Camera.main.GetComponent<Bloom>();
        canvas = GameObject.Find("Canvas");

        buttonColor1 = new Color(26f / 255, 89f / 255, 112f / 255);
        buttonActiveColor1 = new Color(17f / 255, 158f / 255, 210f / 255);
    }
}