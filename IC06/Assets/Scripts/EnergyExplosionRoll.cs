using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyExplosionRoll : MonoBehaviour
{

    [SerializeField]
    private ParticleSystem lightning;

    private ParticleSystem roll;
    // Start is called before the first frame update
    void Start()
    {
        ParticleSystem.ShapeModule shapePs = lightning.shape;
        ParticleSystem.ShapeModule baseShapePs = GetComponent<ParticleSystem>().shape;

        Destroy(gameObject, 4);
        shapePs.radius = baseShapePs.radius;
        //0 -> 0.3
        //2.9x -> 7.8
    }

    // Update is called once per frame
    void Update()
    {
        ParticleSystem.ShapeModule shapePs = lightning.shape;
        shapePs.radius = shapePs.radius + (2.68965517f * Time.deltaTime); 
    }
}
