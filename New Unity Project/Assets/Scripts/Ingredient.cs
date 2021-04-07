using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : Item
{
    public const int TYPE_BETON = 0;
    public const int TYPE_POUDRE = 1;
    public const int TYPE_POISON = 2;
    [SerializeField]
    private int type;
    
    public int GetType()
    {
        return type;
    }
}
