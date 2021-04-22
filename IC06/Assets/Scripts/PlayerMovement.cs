using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    private Player player;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        
        if (!player.IsInteracting())
        {
            float moveHorizontal = Input.GetAxis(player.GetControls().GetHorizontal());
            float moveVertical = Input.GetAxis(player.GetControls().GetVertical());
            Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
            //transform.forward = Vector3.Normalize(movement);
            if (movement != Vector3.zero)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), 0.2F);
            }

            float slow = 1f;
            if(player.IsSlowed()){
                slow = 0.5f;
            }

            //GetComponent<Rigidbody>().AddForce(movement * speed * Time.deltaTime);
            //transform.Translate(movement * speed * Time.deltaTime);
            //transform.Translate(movement * speed * Time.deltaTime, Space.World);
            transform.position += movement * speed * Time.deltaTime * slow;
        }
    }



}

