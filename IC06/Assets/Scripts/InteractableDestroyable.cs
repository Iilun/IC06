using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableDestroyable : Destroyable
{
   private Tile motherTile;
        
    [SerializeField]
    private bool isDestroyable;

    public bool IsDestroyable(){
        return isDestroyable;
    }

    public void SetTile(Tile value){
        motherTile = value;
    }

    public override void Destroy(int i, bool isBaseTile, float delay)
    {
        motherTile.Destroy(i, isBaseTile, delay);
    }

    public override Boat GetBoat()
    {
        return motherTile.GetBoat();
    }

    public override Tile GetTile()
    {
        return motherTile;
    }
}
