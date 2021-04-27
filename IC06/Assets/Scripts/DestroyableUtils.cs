using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyableUtils : MonoBehaviour
{
    public static DestroyableUtils instance;

    public GameObject fire;

    public GameObject ice;

    public GameObject iceCube;

    public GameObject iceExplosionRoll;

    public GameObject iceExplosion;

    public GameObject fieExplosion;

    public GameObject windExplosionRoll;

    public GameObject windExplosion;

    public GameObject iemExplosion;

    public GameObject iemExplosionRoll;

    public GameObject bigExplosion;

    public GameObject normalExplosion;

    public GameObject bomb;

    public GameObject tileDestroyAlone;

    public Material indestructible;

    public Mesh indestructibleMesh;

    public Mesh halfBrokenTileMesh;
    public Mesh heavyBrokenTileMesh;
    public Transform mainCamera;

    void Awake()
    {
        instance = this;
    }

    public static Mesh GetHalfBrokenTileMesh(){
        return instance.halfBrokenTileMesh;
    }

    public static Mesh GetHeavyBrokenTileMesh(){
        return instance.heavyBrokenTileMesh;
    }
    public static Mesh GetIndestructibleTileMesh(){
        return instance.indestructibleMesh;
    }
    public static Material GetIndestructibleTileMaterial(){
        return instance.indestructible;
    }

    public static GameObject GetFire()
    {
        return instance.fire;
    }

    public static Transform GetCam()
    {
        return instance.mainCamera;
    }

    public static GameObject GetIce()
    {
        return instance.ice;
    }

    public static GameObject GetIceCube()
    {
        return instance.iceCube;
    }

    public static GameObject GetIceExplosionRoll()
    {
        return instance.iceExplosionRoll;
    }

    public static GameObject GetIceExplosion()
    {
        return instance.iceExplosion;
    }

    public static GameObject GetWindExplosionRoll()
    {
        return instance.windExplosionRoll;
    }

    public static GameObject GetWindExplosion()
    {
        return instance.windExplosion;
    }

    public static GameObject GetFireExplosion()
    {
        return instance.fieExplosion;
    }

    public static GameObject GetIEMExplosion()
    {
        return instance.iemExplosion;
    }

    public static GameObject GetIEMExplosionRoll()
    {
        return instance.iemExplosionRoll;
    }

    public static GameObject GetBigExplosion()
    {
        return instance.bigExplosion;
    }

    public static GameObject GetNormalExplosion()
    {
        return instance.normalExplosion;
    }

    public static GameObject GetBomb()
    {
        return instance.bomb;
    }

    public static GameObject GetTileDestroyAlone()
    {
        return instance.tileDestroyAlone;
    }
}
