using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDrop : MonoBehaviour
{
    void Start() {
        GetComponent<Collider>().isTrigger = false;
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<TileFloor>() != null)
        {
            Debug.Log("Tile");
            DisablePhysics(true);
            GetComponent<Collider>().isTrigger = true;
        }

        if (other.gameObject.tag == "Water")
        {
            Destroy(gameObject);
        }
    }

    public void DisablePhysics(bool value)
    {
        if (value)
        {
            GetComponent<Rigidbody>().isKinematic = true;
        } else
        {
            GetComponent<Rigidbody>().isKinematic = false;
        }
    }
}
