using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Respawn : MonoBehaviour
{
    public float min_height;
    public const float RESPAWN_HEIGHT = 5f;


    void FixedUpdate()
    {//Changer mettre ca sur l'eau en collider

        if (transform.position.y < min_height)
        {
            transform.position = GetClosestTile(TileUtils.GetTilesForPlayer(GetComponent<Player>())).position + new Vector3(0, RESPAWN_HEIGHT, 0);
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            transform.rotation = Quaternion.identity;
        }
            
    }
        
    Transform GetClosestTile(List<Tile> tiles)
    {
        Transform bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;
        foreach (Tile t in tiles)
        {
            Transform potentialTarget = t.transform;
            Vector3 directionToTarget = potentialTarget.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr && t.GetTileFloor() != null && !t.GetTileFloor().IsDestroyed())
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = potentialTarget;
            }
        }

        return bestTarget;
    }
}
