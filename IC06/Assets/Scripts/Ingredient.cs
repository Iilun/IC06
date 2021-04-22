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
    
    public int GetType()
    {
        return type;
    }
}
