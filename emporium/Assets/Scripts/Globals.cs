using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.ImageEffects;

public class Globals : MonoBehaviour {


    public static bool cameraUp;
    public static GameObject canvas;


    //skybox materials
    public static Material light_skybox;
    public static Material dark_skybox;

    //camera effect references
    public static BlurOptimized cameraBlur;
    public static Bloom cameraBloom;

    // Use this for initialization
    void Start () {

        dark_skybox = Resources.Load("Materials/Skybox_mat_darkened") as Material;
        light_skybox = RenderSettings.skybox;

    }

    void Awake()
    {
        cameraBlur = Camera.main.GetComponent<BlurOptimized>();
        cameraBloom = Camera.main.GetComponent<Bloom>();
        canvas = GameObject.Find("Canvas");

    }
	
	
}
