using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDrop : MonoBehaviour
{
    void Start() {
        
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<TileFloor>() != null)
        {
            DisablePhysics(true);

            float other_offset = other.gameObject.GetComponent<Collider>().bounds.size.y;
            float this_offset = GetComponent<Collider>().bounds.size.y;
            float final_pos_y = other.gameObject.transform.position.y + (0.5f * other_offset) + (0.5f * this_offset);
            transform.position = new Vector3(transform.position.x, final_pos_y, transform.position.z);
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
