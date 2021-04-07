using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class TileUtils : MonoBehaviour
{

    private static List<Tile> allTiles = new List<Tile>();
    // Start is called before the first frame update
    void Start()
    {
        foreach(GameObject g in GameObject.FindGameObjectsWithTag("Tile"))
        {
            allTiles.Add(g.GetComponent<Tile>());
        }
    }

    public static List<Tile> GetAllTiles()
    {
        return allTiles;
    }

    public static List<Tile> GetTilesForPlayer(Player player)
    {
        return allTiles.FindAll(e => e.GetBoat().GetId().Equals(player.GetBoat().GetId()));
    }

    public static List<Tile> GetTilesForBoat(Boat boat)
    {
        return allTiles.FindAll(e => e.GetBoat().GetId().Equals(boat.GetId()));
    }

    public static List<Tile> GetClosestTiles(Destroyable origin, int numberOfTiles)
    {
        List<Tile> tiles = GetTilesForBoat(origin.GetBoat());
        Vector3 currentPosition = origin.gameObject.transform.position;
        tiles.Sort((Tile t1, Tile t2) => (t1.transform.position - currentPosition).sqrMagnitude.CompareTo((t2.transform.position - currentPosition).sqrMagnitude));
        tiles.RemoveAll((Tile t) => t.transform.position.Equals(currentPosition));
        return tiles.Take(numberOfTiles).ToList();
    }

}
