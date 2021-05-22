using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftStation : Interactable
{

    private Ingredient[] ings;
    private bool isAvailable;
    private bool isInteracting;
    private bool firstClick;
    private Player interactingPlayer;

    private bool disabled;
    public Text tooltip;


    void Start()
    {
        tooltip.text = "";
        isAvailable = false;
        isInteracting = false;
        ings = new Ingredient[]{null, null, null, null };
    }

    void Update()
    {
        if (isInteracting)
        {

            if (Input.GetButtonUp(interactingPlayer.GetControls().GetAction()))
            {
                if (firstClick)
                {
                    StartCoroutine(ClickWaiter());
                }
                else
                {
                    FactoryReset();
                }

            }

            if (interactingPlayer != null && interactingPlayer.GetCurrentItem() == null && Input.GetButtonDown(interactingPlayer.GetControls().GetRelease()))
            {
                Ingredient ing = Remove(0, false);
                if (ing != null){
                    ing.GetComponent<Collider>().enabled = true;
                    ing.ResetScale();     
                    ing.PickUp(interactingPlayer);
                }
                FactoryReset();
            }

        }

        if (ings[0] != null)
        {
            ings[0].transform.RotateAround(transform.position, Vector3.up, 20 * Time.deltaTime);  
        }
    }

    public override void Interact(Player player)
    {
        interactingPlayer = player;
        isInteracting = true;
        isAvailable = false;
        firstClick = true;
        //Si null bah ca reste null
        if (player.GetCurrentItem() != null && player.GetCurrentItem().GetComponent<Ingredient>() != null && ( ings[0] == null || (player.GetCurrentItem().GetComponent<Ingredient>().GetType() != ings[0].GetType())))
        {
            
            PlaceIngredient(player.GetCurrentItem().GetComponent<Ingredient>());
            
            StartCoroutine(WaitFactoryReset());
        } else if(player.GetCurrentItem() == null)
        {
            HideTooltip(player);
            Align(player);
        } else
        {
            StartCoroutine(WaitFactoryReset());
        }


    }

    IEnumerator WaitFactoryReset()
    {
        //Wait for .5 seconds
        yield return new WaitForSeconds(0.3f);

        FactoryReset();

    }


    private void FactoryReset()
    {

        DisplayTooltip(interactingPlayer);
        interactingPlayer.SetIsInteracting(false);
        interactingPlayer = null;
        isInteracting = false;
        isAvailable = true;
    }

    private void PlaceIngredient(Ingredient ing)
    {
        for (int i = 0; i< ings.Length; i++)
        {
            
            if (ings[i] == null)
            {
                ings[i] = ing;
                break;
            }
        }
        ing.GetCurrentPlayer().SetCurrentItem(null);
        ing.SetCurrentPlayer(null);
        ing.GetComponent<Collider>().enabled = false;
        DisplayIngredients();
        
        ItemUtils.CreateBullet(ItemUtils.CraftBullet(this), transform.position + new Vector3(0, 8f, 0));
        
    }

    private void DisplayIngredients()
    {
        
        if (ings[0] != null)
        {
            float scaleChange = 0.7f;
           
            ings[0].transform.Rotate(new Vector3(30,0,0), Space.Self);
            ings[0].transform.localScale = Vector3.Scale(ings[0].transform.localScale, new Vector3(scaleChange, scaleChange, scaleChange));
             ings[0].transform.position = transform.position + new Vector3(0, (0.5f * this.GetComponent<Collider>().bounds.size.y) + (0.3f*ings[0].transform.localScale.y), 0);
        }
        
    }

    public Ingredient Remove(int ingType, bool isTypeRemove)
    {

        Ingredient ing = null;
        if (isTypeRemove)
        {
            for (int i = 0; i < ings.Length; i++)
            {
                if (ings[i] != null && ings[i].GetType() == ingType)
                {
                    Destroy(ings[i].gameObject);
                    ings[i] = null;
                    break;
                }
            }
            ResortTab();
        } else
        {

            for (int i = ings.Length - 1; i >=0 ; i--)
            {
                if (ings[i] != null)
                {
                    ing = ings[i];
                    ings[i] = null;
                    break;
                }
            }
            ResortTab();
            return ing;
        }

        return null;
    }
    private void ResortTab()//Marche pas TODO
    {
        for (int i = 0; i < ings.Length; i++)
        {
            if (ings[i] == null )
            {
                for (int j = i; j < ings.Length; j++)
                {
                    if (ings[j] != null)
                    {
                        ings[i] = ings[j];
                        ings[j] = null;
                        break;
                    }
                }
            }
        }

    }

    public override void Enter(Player player)
    {
        if (player.GetCurrentItem() == null ||( player.GetCurrentItem() != null && player.GetCurrentItem().GetComponent<Bucket>() == null)){
            player.SetSelectedInteractable(this);
            DisplayTooltip(player);
            isAvailable = true;
        }
        
        
    }

    private void DisplayTooltip(Player player)
    {
        if (player != null)
        {
            string interactKey = player.GetControls().GetAction().ToString();
            tooltip.text = "Appuyez sur (" + interactKey + ") pour utiliser";
        }
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
        Debug.Log(GetComponent<Collider>().bounds.size);
        float facteur = 1;
        if (this.transform.position.x > 0){
            facteur = -1;
        }
        float x_displacement = facteur * transform.right.x * (GetComponent<Collider>().bounds.size.y * 1f);
        Vector3 newPosition = new Vector3(transform.position.x + x_displacement, player.transform.position.y, transform.position.z);
        player.transform.position = newPosition;
        player.transform.forward = - facteur * transform.right;
        //  player.transform.LookAt(transform.position + transform.forward);
    }

    public Ingredient GetIng(int pos)
    {
        return ings[pos];
    }

    public override bool IsDisabled(){
        return disabled;
    }

    public override void SetDisabled(bool value){
        disabled = value;
    }

}
