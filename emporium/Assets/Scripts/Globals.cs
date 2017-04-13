using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.ImageEffects;

public class Globals : MonoBehaviour {

    public static Globals Instance;

    public bool cameraUp;
    public GameObject canvas;


    //skybox materials
    public Material light_skybox;
    public Material dark_skybox;

    //camera effect references
    public BlurOptimized cameraBlur;
    public Bloom cameraBloom;

    // Use this for initialization
    void Start () {

        dark_skybox = Resources.Load("Materials/Skybox_mat_darkened") as Material;
        light_skybox = RenderSettings.skybox;

    }

    void Awake()
    {
        Instance = this;
        cameraBlur = Camera.main.GetComponent<BlurOptimized>();
        cameraBloom = Camera.main.GetComponent<Bloom>();
        canvas = GameObject.Find("Canvas");

    }
	
	
}
