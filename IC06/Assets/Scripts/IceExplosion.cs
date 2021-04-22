using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceExplosion : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(DestroyableUtils.GetIceExplosionRoll(), transform.position, Quaternion.identity);
        Destroy(gameObject,2);
    }

}
