using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindExplosion : MonoBehaviour
{
    void Start()
    {
        Instantiate(DestroyableUtils.GetWindExplosionRoll(), transform.position, Quaternion.identity);
        Destroy(gameObject,2);
    }
}
