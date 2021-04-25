using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : Item
{
    public const int TYPE_FER = 0;
    public const int TYPE_POUDRE = 1;
    public const int TYPE_EAU = 2;

    public const int TYPE_DYNAMITE = 3;
    [SerializeField]
    private int type;
    
    new public void PickUp(Player player)
    {
        base.PickUp(player);
        switch(type){
            case TYPE_FER :  transform.localRotation = Quaternion.Euler(0,0,0);
            //(new Vector3(0,player.transform.rotation.eulerAngles.y,0), Space.Self);
                break;

            case TYPE_EAU : transform.localRotation = Quaternion.Euler(-90,0,0); 
            //transform.Rotate(new Vector3(-90,0,0), Space.Self);
                break;

            case TYPE_DYNAMITE : transform.localRotation = Quaternion.Euler(-90,0,0); 
            //transform.Rotate(new Vector3(-90,0,0), Space.Self);
                                Debug.Log("Yolo");
                break;

            case TYPE_POUDRE : transform.localRotation = Quaternion.Euler(0,0,0);  
            //transform.Rotate(new Vector3(0,0,0), Space.Self);
                break;
        }
        
       
    }

    new public int GetType()
    {
        return type;
    }

    public override void Enter(Player player)
    {
        
        if(player.GetCurrentItem() == null && isDropped && !isFake)
        {
            PickUp(player); //ici seulement pour les futurs trucs je pense
        }
        
    }
}
