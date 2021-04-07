using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDrop : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Tile")
        {
            DisablePhysics(true);
            transform.position = transform.position + new Vector3(0, 0.216f* GetComponent<Collider>().bounds.size.y, 0);
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
