using UnityEngine;
using System.Collections;

public class GlobalControl : MonoBehaviour
{
    public static string Uname;
    public static string Pass;
    public static int Logincount;
    public static GlobalControl Instance;
    public bool ConnectedOnceNoDupeStatRequests = false;

    void Start()
    {

        Logincount = 1;
    }

    void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
}