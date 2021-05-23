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

    // Start is called before the first frame update
    void Start()
    {
        isInteracting = false;
        selectedInteractable = null;
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
        Debug.Log("Controle" + controls.GetAction());
        if (infos.GetBoatId() == GameTime.BLUE_BOAT_ID){
            boat = GameTime.GetBlueBoat();
        } else {
            boat = GameTime.GetRedBoat();
        }
        this.gameObject.transform.position = boat.gameObject.transform.position + new Vector3(0, 5, z_offset);
        infos.GetModelInfos().SetModelToModelParameters(this.gameObject);
        
    }

    public void InstantiateMenu(PlayerInfos infos){
        controls = infos.GetControls();
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

        if (Input.GetButtonDown(controls.GetAction()))
        {
            Debug.Log("action");
        }

        if (Input.GetButtonDown(controls.GetRelease())){
            Debug.Log("release");
        }
    }

    void FixedUpdate()
    {
        //Debug.Log(currentItem);
        
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
        Debug.Log(other.gameObject);
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
