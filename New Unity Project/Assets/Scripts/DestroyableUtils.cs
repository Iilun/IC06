using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyableUtils : MonoBehaviour
{
    public static DestroyableUtils instance;

    public GameObject fire;

    public Transform mainCamera;

    void Awake()
    {
        instance = this;
    }

    public static GameObject GetFire()
    {
        return instance.fire;
    }

    public static Transform GetCam()
    {
        return instance.mainCamera;
    }
}
