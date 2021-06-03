using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public int color;
    [SerializeField]
    private Boat boat;
    [SerializeField]
    private int player_id;
    private Interactable selectedInteractable;
    private Item currentItem;
    private bool isInteracting;
    private PlayerControls controls;

    private bool isSlowed;

    private bool buttonAction;
    private bool buttonRelease;

    public bool isHolding;

    Animator m_Animator;

    // Start is called before the first frame update
    void Start()
    {
        isInteracting = false;
        selectedInteractable = null;
        m_Animator = gameObject.GetComponent<Animator>();
       /*  if (player_id == 1)
        {
            controls = new PlayerControls('K',"Horizontal", "Vertical", "Interact", "Action", "");
            //controls = new PlayerControls('K',"Horizontal3", "Vertical3", "Interact3", "Action3", "");
            
        } else
        {
            controls = new PlayerControls('K',"Horizontal1", "Vertical1", "Interact1", "Action1", "");
        } */
        
    }

    public void Instantiate(PlayerInfos infos, float z_offset){
        
        controls = infos.GetControls();
        if (infos.GetBoatId() == GameTime.BLUE_BOAT_ID){
            boat = GameTime.GetBlueBoat();
        } else {
            boat = GameTime.GetRedBoat();
        }
        this.gameObject.transform.position = boat.gameObject.transform.position + new Vector3(0, 5, z_offset);
        infos.GetModelInfos().SetModelToModelParameters(this.gameObject);
        
    }

    public void InstantiateMenu(PlayerInfos infos){
        controls = new PlayerControls('V',"VoidAxis", "VoidAxis", "VoidKey", "VoidKey", PlayerControls.VIDE);
        if (infos.GetBoatId() == GameTime.BLUE_BOAT_ID){
            boat = GameTime.GetBlueBoat();
        } else {
            boat = GameTime.GetRedBoat();
        }
        infos.GetModelInfos().SetModelToModelParameters(this.gameObject);
        SetIsInteracting(true);
        transform.localScale = new Vector3(3,3,3);
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
        
    }

    private void GetMAnimator(){
        m_Animator = gameObject.GetComponent<Animator>();
    }

    public void InstantiateCeleb(PlayerInfos infos, bool front, float x_offset){
        GetMAnimator();
        controls = infos.GetControls();
        if (front){
            boat = WinnerTime.GetFront();
            this.HasWon(x_offset >= 0 ? 1 : 0);
        } else {
            boat = WinnerTime.GetBack();
            this.HasLost(x_offset >= 0 ? 1 : 0);
        }
        this.gameObject.transform.position = boat.gameObject.transform.position + new Vector3(x_offset, 0, 0);
        infos.GetModelInfos().SetModelToModelParameters(this.gameObject);
        SetIsInteracting(true);
        transform.localScale = new Vector3(3,3,3);
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown(controls.GetAction()) && selectedInteractable != null && selectedInteractable.IsAvailable() && !isInteracting)
        {
            selectedInteractable.Interact(this);
            isInteracting = true;
        }

        if (Input.GetButtonDown(controls.GetAction()) && !isInteracting && currentItem != null)
        {
            currentItem.Drop();
        }

        if(currentItem != null){
            isHolding = true;
            m_Animator.SetBool("isHolding", true);
        } else {
            isHolding = false;
            m_Animator.SetBool("isHolding", false);
        }

    }

    public void HasWon(int animNumber){
        if(animNumber ==0){
            m_Animator.SetBool("isPlayer1", true);
            m_Animator.SetBool("isWinner", true);
            
        } else {
            m_Animator.SetBool("isWinner", true);
            m_Animator.SetBool("isPlayer1", false);
        }
    }

    public void HasLost(int animNumber){
        if(animNumber ==0){
            m_Animator.SetBool("isPlayer1", true);
            m_Animator.SetBool("isLoser", true);
            
        } else {
            m_Animator.SetBool("isLoser", true);
            m_Animator.SetBool("isPlayer1", false);
        }
    }




    void OnTriggerEnter(Collider other)
    {
        if (!isInteracting)
        {
            if (other.gameObject.GetComponent<Interactable>() != null)
            {
                other.gameObject.GetComponent<Interactable>().Enter(this);
            }

        }
        if (other.gameObject.tag == "Water"){
            this.GetComponent<Respawn>().RespawnPlayer();
        }


    }

    void OnTriggerExit(Collider other)
    {
        if (!isInteracting)
        {
            if (other.gameObject.GetComponent<Interactable>() != null )
            {

                other.gameObject.GetComponent<Interactable>().Exit(this);


            }
        }
    }

    public bool IsInteracting()
    {
        return isInteracting;
    }

    public void SetIsInteracting(bool value)
    {
        isInteracting = value;
    }


    public Boat GetBoat()
    {
        return boat;
    }

    public void SetBoat(Boat value)
    {
        boat = value;
    }

    public void SetCurrentItem(Item value)
    {
        currentItem = value;
    }

    public Item GetCurrentItem()
    {
        return currentItem;
    }

    public void SetSelectedInteractable(Interactable value)
    {
        selectedInteractable = value;
    }


    public Interactable GetSelectedInteractable()
    {
        return selectedInteractable;
    }
    public PlayerControls GetControls()
    {
        return controls;
    }

    public void SetSlowed(bool value){
        isSlowed = value;
    }

    public bool IsSlowed(){
        return isSlowed;
    }
}
