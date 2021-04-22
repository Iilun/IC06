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

    //Faire un pipeline des degats de feu en commun
    //Couleur des degats

    //Enlever l'attrapage de boulet ennemi
    //Quid du cumul des effets ? Genre si je tire un boulet de glace sur une tile en feu ?
    //Dernier effet supérieur, pas de probleme quand damage a tile.
    //Attnetion bug du feu a resoudre
    //Avoir le systeme tile -> tilefloor

    //De mon coté : UI 

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
        if (player.GetSelectedInteractable() == null && player != null && player.GetCurrentItem() != null && player.GetCurrentItem().GetComponent<Bucket>() != null){
            player.SetSelectedInteractable(this);
            DisplayTooltip(player);
            isAvailable = true;
        }  
    }

    private void DisplayTooltip(Player player)
    {
        string interactKey = player.GetControls().GetAction().ToString();
        tooltip.text = "Appuyez sur (" + interactKey + ") pour éteindre";
        //26.4 -> -3.6
        //50.4 -> -3.22
    }

    public override void Exit(Player player)
    {
        HideTooltip(player);
        isAvailable = false;
        Debug.Log("Exit");
        Debug.Log(this);
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
