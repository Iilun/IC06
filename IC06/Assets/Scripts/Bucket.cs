using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bucket : Item
{
    private bool isQuitting;
    private BucketTile motherTile;

    void Awake() {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
     void OnApplicationQuit()
    {
     isQuitting = true;
     }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
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
