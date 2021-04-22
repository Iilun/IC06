using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftStation : Interactable
{

    private Ingredient[] ings;
    private bool isAvailable;
    private bool isInteracting;
    private bool firstClick;
    private Player interactingPlayer;

    private bool disabled;
    public TextMesh tooltip;


    void Start()
    {
        tooltip.text = "";
        isAvailable = false;
        isInteracting = false;
        ings = new Ingredient[]{null, null, null, null };
    }

    void FixedUpdate()
    {
        if (isInteracting)
        {

            if (Input.GetKeyUp(interactingPlayer.GetControls().GetAction()))
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

            if (interactingPlayer != null && interactingPlayer.GetCurrentItem() == null && Input.GetKeyDown(interactingPlayer.GetControls().GetRelease()))
            {
                Ingredient ing = Remove(0, false);
                ing.GetComponent<Collider>().enabled = true;
                ing.ResetScale();     
                ing.PickUp(interactingPlayer);
                FactoryReset();
            }

        }
    }

    public override void Interact(Player player)
    {
        interactingPlayer = player;
        isInteracting = true;
        isAvailable = false;
        firstClick = true;
        //Si null bah ca reste null
        if (player.GetCurrentItem() != null && player.GetCurrentItem().GetComponent<Ingredient>() != null)
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
        
        ItemUtils.CreateBullet(ItemUtils.CraftBullet(this), interactingPlayer.transform.position + new Vector3(0, 5f, 0));
        
    }

    private void DisplayIngredients()
    {
        int i = 0;
        float scaleChange = 0.3f * GetComponent<Collider>().bounds.size.z;
        foreach (Ingredient ing in ings)
        {
            if (ing != null)
            {
                ing.transform.position = GetPos(i);
                ing.transform.localScale = new Vector3(scaleChange, scaleChange, scaleChange);
                i++;
            }
        }
    }

    private Vector3 GetPos(int i)
    {
        Vector3[] pos = { new Vector3(-0.25f, 0.7f, -0.25f), new Vector3(-0.25f, 0.7f, 0.25f), new Vector3(0.25f, 0.7f, -0.25f), new Vector3(0.25f, 0.7f, 0.25f) };
        // Debug.Log(transform.position);
        //Debug.Log(Vector3.Scale(GetComponent<Collider>().bounds.size, pos[i]) + transform.position);
        return Vector3.Scale(GetComponent<Collider>().bounds.size, pos[i]) + transform.position;
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
        player.SetSelectedInteractable(this);
        DisplayTooltip(player);
        isAvailable = true;
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
        yield return new WaitForSeconds(0.5f);

        firstClick = false;

    }

    private void Align(Player player)
    {

        float x_displacement = -transform.forward.x * (GetComponent<Collider>().bounds.size.x * 1.7f);
        Vector3 newPosition = new Vector3(transform.position.x + x_displacement, player.transform.position.y, transform.position.z);
        player.transform.position = newPosition;
        player.transform.forward = transform.forward;
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
