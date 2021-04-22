using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IEMExplosion : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(DestroyableUtils.GetIEMExplosionRoll(), transform.position, Quaternion.identity);
        Destroy(gameObject,2);
    }

}
