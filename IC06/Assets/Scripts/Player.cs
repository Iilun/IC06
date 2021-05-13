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

    // Start is called before the first frame update
    void Start()
    {
        isInteracting = false;
        selectedInteractable = null;
        if (player_id == 1)
        {
            controls = new PlayerControls('K',"Horizontal", "Vertical", KeyCode.E, KeyCode.Space);
        } else
        {
            controls = new PlayerControls('K',"Horizontal1", "Vertical1", KeyCode.P, KeyCode.RightControl);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        //Debug.Log(currentItem);
        if (Input.GetKeyDown(controls.GetAction()) && selectedInteractable != null && selectedInteractable.IsAvailable() && !isInteracting)
        {
            selectedInteractable.Interact(this);
            isInteracting = true;
        }

        if (Input.GetKeyDown(controls.GetAction()) && !isInteracting && currentItem != null)
        {
            currentItem.Drop();
        }

        if (Input.GetKeyDown(controls.GetAction()))
        {
            //?
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
