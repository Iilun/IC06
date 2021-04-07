

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class Cannon : Interactable
{
    public TextMesh tooltip;
    [SerializeField]
    private LaunchArc arc;
    [SerializeField]
    private float velocity_factor;
    private bool isAvailable;
    private bool isInteracting;
    private bool firstClick;
    private bool isShooting;
    private Player interactingPlayer;
    public const float MAX_ROTATION = 75f;
    private Quaternion base_rotation;
    private float shooting_strength;
    private Vector3[] arcArray;
    private Bullet bullet;
    private Quaternion shoot_rotation;
    // Start is called before the first frame update
    void Start()
    {
        tooltip.text = "";
        isAvailable = false;
        isInteracting = false;
        isShooting = false;
        shooting_strength = 10f;
        base_rotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if (isInteracting)
        {
            
            float rotateHorizontal = Input.GetAxis(interactingPlayer.GetControls().GetHorizontal());

            float offset = base_rotation.eulerAngles.y;

            

            if (((transform.rotation.eulerAngles.y > AngleTo360(offset - MAX_ROTATION) ) && rotateHorizontal < 0) ||
                ((transform.rotation.eulerAngles.y < AngleTo360(offset + MAX_ROTATION)) && rotateHorizontal > 0))
            {
                transform.Rotate(0, rotateHorizontal, 0, Space.Self);
                arc.transform.Rotate(0, rotateHorizontal, 0, Space.World);

            }


            if (interactingPlayer != null && Input.GetKeyUp(interactingPlayer.GetControls().GetAction()))
            {
                if (firstClick)
                {
                    StartCoroutine(ClickWaiter());
                } else
                {
                    FactoryReset();
                }

                
            }

            if (isShooting)
            {
                shooting_strength += velocity_factor * Time.deltaTime;
                arc.velocity = shooting_strength;
                arcArray = arc.Render();

            }

            if (bullet != null && interactingPlayer != null && Input.GetKeyDown(interactingPlayer.GetControls().GetRelease()))
            {

                bullet.Load();
                isShooting = true;
                arc.Render();
            }

            if (isShooting && interactingPlayer != null && Input.GetKeyUp(interactingPlayer.GetControls().GetRelease()))
            {
                StartCoroutine(Shoot());
                shoot_rotation = transform.rotation;
                FactoryReset();
            }


        }
    }

    IEnumerator Shoot()
    {

        interactingPlayer.SetCurrentItem(null);
        StartCoroutine(bullet.EnableCollider(1f / arcArray.Length * 0.5f));
        bullet.gameObject.SetActive(true);
        bullet.InitiateCountDown();
        bullet.transform.position = transform.position;
        Vector3 previous = Vector3.zero;

        foreach (Vector3 v in arcArray)
        {

            float facteur = 1f; ;
            
            if(shoot_rotation.eulerAngles.y - base_rotation.eulerAngles.y > 0)
            {
                facteur = -1f;

            }
            
            // Vector3.Scale(new Vector3(transform.forward.x, 1f,1f),v + new Vector3(0, 0, facteur * v.x * Mathf.Tan((Mathf.Abs(shoot_rotation.eulerAngles.y - base_rotation.eulerAngles.y)) * Mathf.PI / 180)) - bullet.transform.position + transform.position);


            Vector3 movement;
            if (base_rotation.eulerAngles.y > 0)
            {
                movement  = v + new Vector3(0, 0, facteur * v.x * Mathf.Tan((Mathf.Abs(shoot_rotation.eulerAngles.y - base_rotation.eulerAngles.y)) * Mathf.PI / 180)) - bullet.transform.position + transform.position;

            }
            else
            {
                movement = Vector3.Scale(new Vector3(-1f, 1f, 1f), v + new Vector3(0, 0, facteur * v.x * Mathf.Tan((Mathf.Abs(shoot_rotation.eulerAngles.y - base_rotation.eulerAngles.y)) * Mathf.PI / 180)) - bullet.transform.position + transform.position);

            }
           
            previous = bullet.transform.position;
            bullet.transform.Translate(movement);
            
            //bullet.transform.position = transform.position + v + new Vector3(0, 0, facteur * v.x* Mathf.Tan((Mathf.Abs(shoot_rotation.eulerAngles.y - base_rotation.eulerAngles.y)) * Mathf.PI / 180));
            yield return new WaitForSeconds(1f / arcArray.Length);
            

            
        }
        if (bullet != null) {
            bullet.GetComponent<BulletDrop>().DisablePhysics(false);
        }

    }

    

    private void FactoryReset()
    {
        isShooting = false;
        DisplayTooltip(interactingPlayer);
        arc.Disable();
        interactingPlayer.SetIsInteracting(false);
        interactingPlayer = null;
        isInteracting = false;
        isAvailable = true;
        transform.rotation = base_rotation;
        shooting_strength = 7f;
    }

    public override void Interact(Player player)
    {
        isInteracting = true;
        interactingPlayer = player;
        isAvailable = false;
        firstClick = true;
        HideTooltip(player);
        Align(player);
        //Si null bah ca reste null
        if(player.GetCurrentItem() != null)
        {
            bullet = player.GetCurrentItem().GetComponent<Bullet>();
        }

    }

    private float AngleTo360(float value)
    {
        value = value % 360;
        if (value < 0)
            value += 360;
        return value;
    }

    public override void Enter(Player player)
    {
        player.SetSelectedInteractable(this);
        DisplayTooltip(player);
        isAvailable = true;
    }

    private void DisplayTooltip(Player player)
    {
        string interactKey = player.GetControls().GetAction().ToString();
        tooltip.text = "Appuyez sur ("+ interactKey + ") pour utiliser";
    }

    public override void Exit(Player player)
    {
        HideTooltip(player);
        isAvailable = false;
        player.SetSelectedInteractable(null);
    }

    private void HideTooltip(Player player)
    {
        tooltip.text = "";
    }

    public override bool IsAvailable()
    {
        return isAvailable;
    }

    IEnumerator ClickWaiter()
    {
        //Wait for .5 seconds
        yield return new WaitForSeconds(0.5f);

        firstClick = false;

    }

    private void Align(Player player)
    {

        float x_displacement = -transform.forward.x * (GetComponent<Collider>().bounds.size.x * 0.7f);
        Vector3 newPosition = new Vector3(transform.position.x + x_displacement, player.transform.position.y, transform.position.z);
        player.transform.position = newPosition;
        player.transform.forward = transform.forward;
    }

}
