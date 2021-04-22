using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Destroyable : MonoBehaviour
{
    public const int DESTRUCTION_TOTALE = 0;
    public const int DESTRUCTION_LOURDE = 1;
    public const int DESTRUCTION_FEU = 2;
    public const int DESTRUCTION_GLACE = 3;

    public const int DESTRUCTION_WIND = 4;

    public const int DESTRUCTION_IEM = 5;

    public const int DESTRUCTION_LEGERE = 6;

    public const int BOMB_SPAWN = 7;

    protected int destruction_level;
    public abstract void Destroy(int type, bool isBaseTile, float delay);
    public abstract Boat GetBoat();

    public abstract Tile GetTile();

}
