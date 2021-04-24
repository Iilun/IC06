using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : Interactable
{
    protected int item_id;
    protected Player currentPlayer;
    public const float PLAYER_HEIGHT = 5f;

    public const int BULLET = 0;
    public const int INGREDIENT = 1;
    private Vector3 baseScale;
    protected bool isDropped;
    protected bool isFake;

    private bool disabled;
    // Start is called before the first frame update
    protected void Start()
    {
        baseScale = transform.localScale;
        isFake = false;
        isDropped = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ResetScale()
    {
        transform.localScale = baseScale;
        transform.rotation = Quaternion.identity;
    }
    void FixedUpdate()
    {
        if(currentPlayer != null)
        {
            // transform.position = currentPlayer.transform.position + Vector3.Scale(currentPlayer.transform.forward,new Vector3(2f,1f,2f)) + new Vector3(0, PLAYER_HEIGHT, 0);
            // transform.RotateAround(currentPlayer.transform.position, new Vector3(0,0,0), 20);
        }
    }



    public override void Enter(Player player)
    {
        
        if(player.GetCurrentItem() == null && isDropped && !isFake)
        {
            PickUp(player); //ici seulement pour les futurs trucs je pense
        }
        
    }
    
    public override void Exit(Player player)
    {
        return;
    }

    public override void Interact(Player player)
    {

    }

    public void PickUp(Player player)
    {
        currentPlayer = player;
        currentPlayer.SetCurrentItem(this);
        GetComponent<BulletDrop>().DisablePhysics(true);
        if (GetComponent<Collider>() != null){
                GetComponent<Collider>().enabled = false;
        }
        this.transform.parent = currentPlayer.transform;
        this.transform.localPosition = new Vector3(0, PLAYER_HEIGHT, 4);
        Debug.Log("base_pickup");
    }

    public void Drop()
    {
        currentPlayer.SetCurrentItem(null);
        transform.position = transform.position + Vector3.Scale(currentPlayer.transform.forward, new Vector3(6f, 0, 6f));//+ new Vector3(0, -PLAYER_HEIGHT, 0)
        
        
        //faire egaffe
        GetComponent<BulletDrop>().DisablePhysics(false);
        if (GetComponent<Collider>() != null){
                GetComponent<Collider>().enabled = true;
        }
        SetCurrentPlayer(null);
        isDropped = true;
    }

    public override bool IsAvailable()
    {
        return true;
    }

    public Player GetCurrentPlayer()
    {
        return currentPlayer;
    }

    public void SetCurrentPlayer(Player player)
    {
        currentPlayer = player;
        if(player == null){
            transform.parent = ItemUtils.GetGame().transform;
        }
    }

    public void SetIsDropped(bool value)
    {
        isDropped = value;
    }

    public bool GetIsDropped()
    {
        return isDropped;
    }

    public void SetIsFake(bool value)
    {
        isFake = value;
        if (isFake)
        {
            GetComponent<BulletDrop>().DisablePhysics(true);
            GetComponent<Collider>().enabled = false;
        } else
        {
            GetComponent<BulletDrop>().DisablePhysics(false);
            GetComponent<Collider>().enabled = true;
        }
    }

    public override bool IsDisabled(){
        return disabled;
    }

    public override void SetDisabled(bool value){
        disabled = value;
    }
}
