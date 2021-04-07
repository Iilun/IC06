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
    public Text tooltip;

    private Tile parent;

    public void SetParent(Tile value)
    {
        parent = value;
    }


    public override void Interact(Player player)
    {
        interactingPlayer = player;
        isInteracting = true;
        isAvailable = false;
        if (player.GetCurrentItem() != null && player.GetCurrentItem().GetComponent<Bucket>() != null)
        {
            firstClick = true;
            
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
    }


    public override void Enter(Player player)
    {
        player.SetSelectedInteractable(this);
        DisplayTooltip(player);
        isAvailable = true;
        Debug.Log("Detected");
    }

    private void DisplayTooltip(Player player)
    {
        
        if (player != null && player.GetCurrentItem() != null && player.GetCurrentItem().GetComponent<Bucket>() != null)
        {
            Debug.Log("Tool");
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

}
