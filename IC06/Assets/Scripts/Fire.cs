using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fire : Interactable
{

    private bool isAvailable;
    private bool isInteracting;
    private bool firstClick;
    private Player interactingPlayer;
    public TextMesh tooltip;

    private Tile parent;

    private bool disabled;

    public void SetParent(Tile value)
    {
        parent = value;
    }

    //TODO : Rambardes?

    
    public override void Interact(Player player)
    {
        interactingPlayer = player;
        isInteracting = true;
        isAvailable = false;
        if (player.GetCurrentItem() != null && player.GetCurrentItem().GetComponent<Bucket>() != null)
        {
            firstClick = true;
            interactingPlayer.GetCurrentItem().GetComponent<Bucket>().isUsed = true;
            // p e BLOCK le mvt ici
            // ANIMATION
            StartCoroutine(WaitDestroy());
        } else
        {
            StartCoroutine(WaitFactoryReset());

        }


    }

    private IEnumerator WaitFactoryReset()
    {
        //Wait for .5 seconds
        yield return new WaitForSeconds(0.3f);

        FactoryReset();

    }


    private void FactoryReset()
    {
        interactingPlayer.SetIsInteracting(false);
        interactingPlayer = null;
        isInteracting = false;
        isAvailable = true;
    }

    private IEnumerator WaitDestroy()
    {
        //Wait for .5 seconds
        yield return new WaitForSeconds(0.3f);

        parent.SetFire(false);
        interactingPlayer.SetIsInteracting(false);
        interactingPlayer = null;
        isInteracting = false;
        isAvailable = true;
        interactingPlayer.GetCurrentItem().GetComponent<Bucket>().isUsed = false;
    }


    public override void Enter(Player player)
    {
        if (player.GetSelectedInteractable() == null && player != null && player.GetCurrentItem() != null && player.GetCurrentItem().GetComponent<Bucket>() != null){
            player.SetSelectedInteractable(this);
            DisplayTooltip(player);
            isAvailable = true;
        }  
    }

    private void DisplayTooltip(Player player)
    {
        string interactKey = player.GetControls().GetActionName();
        tooltip.text = "Appuyez sur (" + interactKey + ") pour eteindre";
        //26.4 -> -3.6
        //50.4 -> -3.22
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

    public override bool IsDisabled(){
        return disabled;
    }

    public override void SetDisabled(bool value){
        disabled = value;
    }

}
