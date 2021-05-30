using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatShake : MonoBehaviour
{
    float z_speed = 2.0f;
    float x_speed = 1.0f;

    public float time = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Invoke("shake", time);
        shake();
    }

    void shake(){
        // shake with z
        if (this.transform.eulerAngles.z >= 4 && this.transform.eulerAngles.z <= 180)
        {
            z_speed = -z_speed;
        }else if (this.transform.eulerAngles.z >= 180 && this.transform.eulerAngles.z <= (360 - 4))
        {
            z_speed = -z_speed;
        }

        // shake with x
        if (this.transform.eulerAngles.x >= 4 && this.transform.eulerAngles.x <= 180)
        {
            x_speed = -x_speed;
        }
        else if (this.transform.eulerAngles.x >= 180 && this.transform.eulerAngles.x <= (360 - 4))
        {
            x_speed = -x_speed;
        }

        this.transform.Rotate(x_speed * Time.deltaTime, 0, z_speed * Time.deltaTime);
    }
}
