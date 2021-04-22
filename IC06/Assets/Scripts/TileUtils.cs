using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class TileUtils : MonoBehaviour
{
    public const float TILE_FLOOR_MAX_HEALTH = 100f;
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
        Debug.Log(allTiles);
        Debug.Log(boat);
        return allTiles.FindAll(e => e.GetBoat().GetId().Equals(boat.GetId()));
    }

    public static List<Tile> GetClosestTiles(Tile origin, int numberOfTilesInDirection, char forme)
    {
        List<Tile> tiles = GetTilesForBoat(origin.GetBoat());
        Vector3 currentPosition = origin.gameObject.transform.position;
        Debug.Log(currentPosition);
        //On vire la tile d'aterrissage
        tiles.RemoveAll((Tile t) => t.transform.position.Equals(currentPosition));

        //Ensuite dans chaque direction

        List<Tile> tileG = tiles.FindAll((Tile t) => t.transform.position.x < currentPosition.x && t.transform.position.z == currentPosition.z);
        tileG.Sort((Tile t1, Tile t2) => (t1.transform.position - currentPosition).sqrMagnitude.CompareTo((t2.transform.position - currentPosition).sqrMagnitude));
        tileG = tileG.Take(numberOfTilesInDirection).ToList();
        List<Tile> tileD = tiles.FindAll((Tile t) => t.transform.position.x > currentPosition.x && t.transform.position.z == currentPosition.z);
        tileD.Sort((Tile t1, Tile t2) => (t1.transform.position - currentPosition).sqrMagnitude.CompareTo((t2.transform.position - currentPosition).sqrMagnitude));
        tileD = tileD.Take(numberOfTilesInDirection).ToList();
        List<Tile> tileH = tiles.FindAll((Tile t) => t.transform.position.z > currentPosition.z && t.transform.position.x == currentPosition.x);
        tileH.Sort((Tile t1, Tile t2) => (t1.transform.position - currentPosition).sqrMagnitude.CompareTo((t2.transform.position - currentPosition).sqrMagnitude));
        tileH = tileH.Take(numberOfTilesInDirection).ToList();
        List<Tile> tileB = tiles.FindAll((Tile t) => t.transform.position.z < currentPosition.z && t.transform.position.x == currentPosition.x);
        tileB.Sort((Tile t1, Tile t2) => (t1.transform.position - currentPosition).sqrMagnitude.CompareTo((t2.transform.position - currentPosition).sqrMagnitude)); 
        tileB = tileB.Take(numberOfTilesInDirection).ToList();


        List<Tile> result = new List<Tile>();
        result.AddRange(tileG);
        result.AddRange(tileD);
        result.AddRange(tileH);
        result.AddRange(tileB);

        if (forme == 'c'){
            List<Tile> tileGH = tiles.FindAll((Tile t) => t.transform.position.x < currentPosition.x && t.transform.position.z > currentPosition.z);
            tileGH.Sort((Tile t1, Tile t2) => (t1.transform.position - currentPosition).sqrMagnitude.CompareTo((t2.transform.position - currentPosition).sqrMagnitude));
            tileGH = tileGH.Take(numberOfTilesInDirection).ToList();
            List<Tile> tileGB = tiles.FindAll((Tile t) => t.transform.position.x < currentPosition.x && t.transform.position.z < currentPosition.z);
            tileGB.Sort((Tile t1, Tile t2) => (t1.transform.position - currentPosition).sqrMagnitude.CompareTo((t2.transform.position - currentPosition).sqrMagnitude));
            tileGB = tileGB.Take(numberOfTilesInDirection).ToList();
            List<Tile> tileDH = tiles.FindAll((Tile t) => t.transform.position.x > currentPosition.x && t.transform.position.z > currentPosition.z);
            tileDH.Sort((Tile t1, Tile t2) => (t1.transform.position - currentPosition).sqrMagnitude.CompareTo((t2.transform.position - currentPosition).sqrMagnitude));
            tileDH = tileDH.Take(numberOfTilesInDirection).ToList();
            List<Tile> tileDB = tiles.FindAll((Tile t) => t.transform.position.x > currentPosition.x && t.transform.position.z < currentPosition.z);
            tileDB.Sort((Tile t1, Tile t2) => (t1.transform.position - currentPosition).sqrMagnitude.CompareTo((t2.transform.position - currentPosition).sqrMagnitude));
            tileDB = tileDB.Take(numberOfTilesInDirection).ToList();

            result.AddRange(tileGH);
            result.AddRange(tileGB);
            result.AddRange(tileDH);
            result.AddRange(tileDB);

        }
        
        

        


        
        return result;
    }

    public static List<Tile> GetClosestTilesFromBullet(Bullet origin, int numberOfTilesInDirection, char forme)
    {
        List<Tile> tiles = GetTilesForBoat(origin.GetBoat());
        Vector3 currentPosition = origin.gameObject.transform.position;
        List<Tile> result = new List<Tile>();
        //On recup la tile la plus
        tiles.Sort((Tile t1, Tile t2) => (t1.transform.position - currentPosition).sqrMagnitude.CompareTo((t2.transform.position - currentPosition).sqrMagnitude));
        Tile closestTile = tiles[0];
        result.Add(closestTile);
        currentPosition = closestTile.gameObject.transform.position;
        tiles.RemoveAll((Tile t) => t.transform.position.Equals(currentPosition));

        //Ensuite dans chaque direction

        List<Tile> tileG = tiles.FindAll((Tile t) => t.transform.position.x < currentPosition.x && t.transform.position.z == currentPosition.z);
        tileG.Sort((Tile t1, Tile t2) => (t1.transform.position - currentPosition).sqrMagnitude.CompareTo((t2.transform.position - currentPosition).sqrMagnitude));
        tileG = tileG.Take(numberOfTilesInDirection).ToList();
        List<Tile> tileD = tiles.FindAll((Tile t) => t.transform.position.x > currentPosition.x && t.transform.position.z == currentPosition.z);
        tileD.Sort((Tile t1, Tile t2) => (t1.transform.position - currentPosition).sqrMagnitude.CompareTo((t2.transform.position - currentPosition).sqrMagnitude));
        tileD = tileD.Take(numberOfTilesInDirection).ToList();
        List<Tile> tileH = tiles.FindAll((Tile t) => t.transform.position.z > currentPosition.z && t.transform.position.x == currentPosition.x);
        tileH.Sort((Tile t1, Tile t2) => (t1.transform.position - currentPosition).sqrMagnitude.CompareTo((t2.transform.position - currentPosition).sqrMagnitude));
        tileH = tileH.Take(numberOfTilesInDirection).ToList();
        List<Tile> tileB = tiles.FindAll((Tile t) => t.transform.position.z < currentPosition.z && t.transform.position.x == currentPosition.x);
        tileB.Sort((Tile t1, Tile t2) => (t1.transform.position - currentPosition).sqrMagnitude.CompareTo((t2.transform.position - currentPosition).sqrMagnitude)); 
        tileB = tileB.Take(numberOfTilesInDirection).ToList();


        
        result.AddRange(tileG);
        result.AddRange(tileD);
        result.AddRange(tileH);
        result.AddRange(tileB);

        if (forme == 'c'){
            List<Tile> tileGH = tiles.FindAll((Tile t) => t.transform.position.x < currentPosition.x && t.transform.position.z > currentPosition.z);
            tileGH.Sort((Tile t1, Tile t2) => (t1.transform.position - currentPosition).sqrMagnitude.CompareTo((t2.transform.position - currentPosition).sqrMagnitude));
            tileGH = tileGH.Take(numberOfTilesInDirection).ToList();
            List<Tile> tileGB = tiles.FindAll((Tile t) => t.transform.position.x < currentPosition.x && t.transform.position.z < currentPosition.z);
            tileGB.Sort((Tile t1, Tile t2) => (t1.transform.position - currentPosition).sqrMagnitude.CompareTo((t2.transform.position - currentPosition).sqrMagnitude));
            tileGB = tileGB.Take(numberOfTilesInDirection).ToList();
            List<Tile> tileDH = tiles.FindAll((Tile t) => t.transform.position.x > currentPosition.x && t.transform.position.z > currentPosition.z);
            tileDH.Sort((Tile t1, Tile t2) => (t1.transform.position - currentPosition).sqrMagnitude.CompareTo((t2.transform.position - currentPosition).sqrMagnitude));
            tileDH = tileDH.Take(numberOfTilesInDirection).ToList();
            List<Tile> tileDB = tiles.FindAll((Tile t) => t.transform.position.x > currentPosition.x && t.transform.position.z < currentPosition.z);
            tileDB.Sort((Tile t1, Tile t2) => (t1.transform.position - currentPosition).sqrMagnitude.CompareTo((t2.transform.position - currentPosition).sqrMagnitude));
            tileDB = tileDB.Take(numberOfTilesInDirection).ToList();

            result.AddRange(tileGH);
            result.AddRange(tileGB);
            result.AddRange(tileDH);
            result.AddRange(tileDB);

        }
        
        

        


        
        return result;
    }


    private static Tile GetLeftTile(Tile origin){
        List<Tile> tiles = GetTilesForBoat(origin.GetBoat());
        Vector3 currentPosition = origin.gameObject.transform.position;

        List<Tile> tileG = tiles.FindAll((Tile t) => t.transform.position.x < currentPosition.x && t.transform.position.z == currentPosition.z);
        tileG.Sort((Tile t1, Tile t2) => (t1.transform.position - currentPosition).sqrMagnitude.CompareTo((t2.transform.position - currentPosition).sqrMagnitude));
        return (Tile) tileG.Take(1);

    }

    private static Tile GetRightTile(Tile origin){
        List<Tile> tiles = GetTilesForBoat(origin.GetBoat());
        Vector3 currentPosition = origin.gameObject.transform.position;
        
        List<Tile> tileD = tiles.FindAll((Tile t) => t.transform.position.x > currentPosition.x && t.transform.position.z == currentPosition.z);
        tileD.Sort((Tile t1, Tile t2) => (t1.transform.position - currentPosition).sqrMagnitude.CompareTo((t2.transform.position - currentPosition).sqrMagnitude));
        return (Tile) tileD.Take(1);

    }

    private static Tile GetTopTile(Tile origin){
        List<Tile> tiles = GetTilesForBoat(origin.GetBoat());
        Vector3 currentPosition = origin.gameObject.transform.position;
        
        List<Tile> tileH = tiles.FindAll((Tile t) => t.transform.position.z > currentPosition.z && t.transform.position.x == currentPosition.x);
        tileH.Sort((Tile t1, Tile t2) => (t1.transform.position - currentPosition).sqrMagnitude.CompareTo((t2.transform.position - currentPosition).sqrMagnitude));
        return (Tile) tileH.Take(1);

    }

    private static Tile GetBottomTile(Tile origin){
        List<Tile> tiles = GetTilesForBoat(origin.GetBoat());
        Vector3 currentPosition = origin.gameObject.transform.position;
        
        List<Tile> tileB = tiles.FindAll((Tile t) => t.transform.position.z < currentPosition.z && t.transform.position.x == currentPosition.x);
        tileB.Sort((Tile t1, Tile t2) => (t1.transform.position - currentPosition).sqrMagnitude.CompareTo((t2.transform.position - currentPosition).sqrMagnitude)); 
        return (Tile) tileB.Take(1);

    }

    private static bool AllTilesAroundDestroyed(Tile origin){
        foreach (Tile t in GetClosestTiles(origin, 1, 'c')){
            if (!t.IsDestroyed()){
                return false;
            }
        }
        return true;
    }

    public static Mesh GetDestroyedMesh(Tile tile){
        if (AllTilesAroundDestroyed(tile)){
            return null; //FULL DESTROYED MESH
        } else {
            Tile tileG = GetLeftTile(tile);
            Tile tileD = GetRightTile(tile);
            Tile tileH = GetTopTile(tile);
            Tile tileB = GetBottomTile(tile);

            return null; //PLEIN DE CAS EN FAIT? genre si le coin haut droit est vide eetc a voir
        }
    }

    public static Mesh GetDamagedMesh(int healthRestante){
        return null;
    }



}
