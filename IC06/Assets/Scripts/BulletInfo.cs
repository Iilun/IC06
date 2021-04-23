using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletInfo
{
    private int type;

    private Boat boat;
    
    public BulletInfo(int type, Boat boat)
    {
        this.type = type;
        this.boat = boat;
    }

    public int GetType()
    {
        return type;
    }

    public Boat GetBoat(){
        return boat;
    }
}
