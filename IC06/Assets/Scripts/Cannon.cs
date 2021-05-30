

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class Cannon : Interactable 
{
    public Text tooltip;
    [SerializeField]
    private LaunchArc arc;
    [SerializeField]
    private float velocity_factor;
    private bool isAvailable;
    private bool isInteracting;
    private bool firstClick;
    public bool isShooting;
    private Player interactingPlayer;
    public const float MAX_ROTATION = 75f;
    private Quaternion base_rotation;
    private float shooting_strength;
    private Vector3[] arcArray;
    private Bullet bullet;
    private Quaternion shoot_rotation;

    private bool disabled;

    [SerializeField]
    private Image disabledImage;
    [SerializeField]
    private GameObject endCircle;

    private Vector3 endPosition;
 
    void Start()
    {
        tooltip.text = "";
        isAvailable = false;
        isInteracting = false;
        isShooting = false;
        shooting_strength = 10f;
        base_rotation = transform.rotation;
        disabled = false;
        disabledImage.gameObject.SetActive(false);
        endCircle.SetActive(false);
        endCircle.transform.localPosition = new Vector3(0,0,0);


        //TEST
        arc.Calculate3dArcArray(new Vector3(1,0,1), new Vector3(-9,0,-9));
    }

    void Update()
    {
        if (isInteracting)
        {
            
            float rotateHorizontal = Input.GetAxis(interactingPlayer.GetControls().GetHorizontal());

            float offset = base_rotation.eulerAngles.y;

            

            if (((transform.rotation.eulerAngles.y > AngleTo360(offset - MAX_ROTATION) ) && rotateHorizontal < 0) ||
                ((transform.rotation.eulerAngles.y < AngleTo360(offset + MAX_ROTATION)) && rotateHorizontal > 0))
            {
                transform.Rotate(0, rotateHorizontal, 0, Space.Self);
                //arc.transform.Rotate(0, rotateHorizontal, 0, Space.World);

            }


            if (interactingPlayer != null && Input.GetButtonUp(interactingPlayer.GetControls().GetAction()))
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
                Vector3 end = arcArray[arcArray.Length-1] - arcArray[0];
                endCircle.transform.localPosition = new Vector3(endCircle.transform.localPosition.x,endCircle.transform.localPosition.y,end.x);

            }

            if (bullet != null && interactingPlayer != null && Input.GetButtonDown(interactingPlayer.GetControls().GetRelease()))
            {
                
                bullet.Load();
                isShooting = true;
                shooting_strength = 10;
                arc.Render();
                endCircle.SetActive(true);
            }

            if (isShooting && interactingPlayer != null && Input.GetButtonUp(interactingPlayer.GetControls().GetRelease()))
            {
                endPosition = endCircle.transform.position;
                bullet.gameObject.SetActive(true);
                if (bullet.GetComponent<Bomb>() != null){
                    Bomb b = bullet.GetComponent<Bomb>();
                    b.Disarm();
                    b.Clear();
                }
                bullet.GetComponent<BulletDrop>().DisablePhysics(true);
                bullet.GetComponent<Collider>().isTrigger = true;
                StartCoroutine(Shoot());
                shoot_rotation = transform.rotation;
                interactingPlayer.SetCurrentItem(null);
                bullet.SetCurrentPlayer(null);
                bullet.SetBoat(interactingPlayer.GetBoat());
                
                bullet.InitiateCountDown();
                bullet.transform.position = transform.position;
                FactoryReset(false);
            }


        }
    }

    IEnumerator Shoot()
    {
       
        
        arcArray = arc.Calculate3dArcArray(arc.transform.position, endPosition);
        StartCoroutine(bullet.EnableCollider(1f / arcArray.Length * 0.3f));
       
        

        foreach (Vector3 v in arcArray)
        {
            
            if (bullet != null) {
                float facteur = 1f; ;
                
                if(shoot_rotation.eulerAngles.y - base_rotation.eulerAngles.y > 0)
                {
                    facteur = -1f;

                }
                
                // Vector3.Scale(new Vector3(transform.forward.x, 1f,1f),v + new Vector3(0, 0, facteur * v.x * Mathf.Tan((Mathf.Abs(shoot_rotation.eulerAngles.y - base_rotation.eulerAngles.y)) * Mathf.PI / 180)) - bullet.transform.position + transform.position);

                bullet.transform.position = v;


                
            
                
                //bullet.transform.Translate(movement);
                
                
                yield return new WaitForSeconds(1f / arcArray.Length);
                

                }
        }

        if (bullet != null) {
            bullet.GetComponent<BulletDrop>().DisablePhysics(false);
        }

    }

    

    private void FactoryReset(bool displayTooltip = true)
    {
        isShooting = false;
        if (displayTooltip){
             DisplayTooltip(interactingPlayer);
        } else {
             HideTooltip(interactingPlayer);
        }
       
        arc.Disable();
        interactingPlayer.SetIsInteracting(false);
        interactingPlayer = null;
        isInteracting = false;
        isAvailable = true;
        transform.rotation = base_rotation;
        shooting_strength = 10;
        arc.velocity = shooting_strength;
        endCircle.SetActive(false);
        endCircle.transform.localPosition = new Vector3(0,0,0);
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
        if(player != null && player.GetCurrentItem() != null && player.GetCurrentItem().GetComponent<Bullet>() != null && !disabled && player.GetCurrentItem().GetComponent<Bullet>().IsShootable()){
            player.SetSelectedInteractable(this);
            DisplayTooltip(player);
            isAvailable = true;
        }
    }

    private void DisplayTooltip(Player player)
    {
        string interactKey = player.GetControls().GetActionName();
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
        yield return new WaitForSeconds(0.2f);

        firstClick = false;

    }

    private void Align(Player player)
    {

        float x_displacement = -transform.forward.x * (GetComponent<Collider>().bounds.size.x * 0.8f);
        Vector3 newPosition = new Vector3(transform.position.x + x_displacement, player.transform.position.y, transform.position.z);
        player.transform.position = newPosition;
        player.transform.forward = transform.forward;
    }

    public override bool IsDisabled(){
        return disabled;
    }

    public override void SetDisabled(bool value){
        disabled = value;
        if (value){
            disabledImage.gameObject.SetActive(true);
        } else {
            disabledImage.gameObject.SetActive(false);
        }
    }

}
