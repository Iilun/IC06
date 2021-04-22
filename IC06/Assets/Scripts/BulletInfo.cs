using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletInfo
{
    private int type;
    
    public BulletInfo(int type)
    {
        this.type = type;
    }

    public int GetType()
    {
        return type;
    }
}
