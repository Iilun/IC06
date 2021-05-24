using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BucketTile : Interactable
{
    [SerializeField]
    private GameObject bucket;

    public MainMenu mainMenu;
    

    // Start is called before the first frame update
    void Awake()
    {
        Spawn();
    }

    public void Spawn(){
        GameObject bucketInstance = Instantiate(bucket, transform.position + new Vector3(0, 3, 0), Quaternion.Euler(-90,0,0));
        if (bucketInstance != null){
            bucketInstance.GetComponent<Bucket>().mainMenu = mainMenu;
            bucketInstance.GetComponent<Bucket>().SetMotherTile(this);
        }
        
    }

    public override void Interact(Player player)
    {

    }


    public override void Enter(Player player)
    {
            
    }

    public override void Exit(Player player)
    {
    }



    public override bool IsAvailable()
    {
        return false;
    }

    private void HideTooltip(Player player)
    {
        
    }




    public override bool IsDisabled(){
        return false;
    }

    public override void SetDisabled(bool value){
    }

}
