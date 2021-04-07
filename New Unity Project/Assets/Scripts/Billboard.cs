using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    public Transform cam;
    
    
    void Start()
    {
        cam = DestroyableUtils.GetCam();
    }

    void LateUpdate()
    {

        transform.LookAt(transform.position + cam.forward);
    }
}