using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Destroyable : MonoBehaviour
{
    public const int DESTRUCTION_TOTALE = 0;
    public const int DESTRUCTION_LOURDE = 1;
    public const int DESTRUCTION_FEU = 2;
    public const int DESTRUCTION_GLACE = 3;

    protected int destruction_level;
    public abstract void Destroy(int type);
    public abstract Boat GetBoat();

}
