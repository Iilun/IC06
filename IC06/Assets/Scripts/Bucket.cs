using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bucket : Item
{
    public MainMenu mainMenu;
    private bool isQuitting;

    public bool isUsed;
    private BucketTile motherTile;

     void OnApplicationQuit()
    {
     isQuitting = true;
     }

    void OnDestroy()
    {
        if (!isQuitting && motherTile != null && mainMenu.IsBucketSpawn())
        {
           motherTile.Spawn();
        }
    }

    public void SetMotherTile(BucketTile mother){
        motherTile = mother;
    }
}
