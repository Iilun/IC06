using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    private Player player;
    public float speed;

    public bool isRunning;

    Animator m_Animator;


    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Player>();
        m_Animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame


    void Update()
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

            if (movement != new Vector3(0,0,0)){
                isRunning = true;
                m_Animator.SetBool("isRunning", true);
            } else {
                isRunning = false;
                m_Animator.SetBool("isRunning", false);
            }

            //GetComponent<Rigidbody>().AddForce(movement * speed * Time.deltaTime);
            //transform.Translate(movement * speed * Time.deltaTime);
            //transform.Translate(movement * speed * Time.deltaTime, Space.World);
            transform.position += movement * speed * Time.deltaTime * slow;
        } else {
            isRunning = false;
            m_Animator.SetBool("isRunning", false);
        }
    }



}

