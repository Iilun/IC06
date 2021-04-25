using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bucket : Item
{
    private bool isQuitting;
    private BucketTile motherTile;
     void OnApplicationQuit()
    {
     isQuitting = true;
     }
    void OnDestroy()
    {
        if (!isQuitting)
        {
           motherTile.Spawn();
        }
    }

    public void SetMotherTile(BucketTile mother){
        motherTile = mother;
    }
}
