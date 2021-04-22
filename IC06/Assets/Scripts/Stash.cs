using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stash : Interactable
{
    [SerializeField]
    private int numberOfIngredients;

    [SerializeField]
    private int initNumberOfIngredients;

    private Ingredient ing;

    [SerializeField]
    private GameObject prefab;

    [SerializeField]
    private TextMesh tooltip;

    [SerializeField]
    private float delay;

    [SerializeField]
    private int maxNumberOfIngredients;

    private bool isAvailable;

    private bool isInteracting;

    private bool firstClick;

    private bool isAdding;

    private Player interactingPlayer;

    private Ingredient[] ings;

    private bool disabled;

    // Start is called before the first frame update
    void Awake()
    {
        tooltip.text = "";
        isAvailable = false;
        isInteracting = false;
        isAdding = false;
        ings = new Ingredient[maxNumberOfIngredients];
        for (int i = 0; i< initNumberOfIngredients; i++)
        {
            AddIngredient();
        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (numberOfIngredients < maxNumberOfIngredients && !isAdding)
        {
            StartCoroutine(AddIngredientWaiter());
        }
    }


    IEnumerator AddIngredientWaiter()
    {
        isAdding = true;
        //Wait for .5 seconds
        yield return new WaitForSeconds(delay);

        AddIngredient();
        isAdding = false;

    }

    public override void Interact(Player player)
    {
        isInteracting = true;
        interactingPlayer = player;
        isAvailable = false;
        firstClick = true;
        HideTooltip(player);
        //Si null bah ca reste null
        if (player.GetCurrentItem() == null && numberOfIngredients > 0)
        {
            ing = Instantiate(prefab, transform.position + new Vector3(0, 5f, 0), Quaternion.identity).GetComponent<Ingredient>();
            
            
            PickUp(player);
            StartCoroutine(WaitFactoryReset());
        }

    }

    

    private void PickUp(Player player)
    {
        RemoveIng();
        ing.PickUp(player);
    }

    private void RemoveIng()
    {
        numberOfIngredients--;
        //gestion de l'affichage
        Displayingredients();
    }

    IEnumerator WaitFactoryReset()
    {
        //Wait for .5 seconds
        yield return new WaitForSeconds(0.4f);

        FactoryReset();

    }

    private void FactoryReset()
    {
        if (numberOfIngredients > 0)
        {
            DisplayTooltip(interactingPlayer);
        }
        interactingPlayer.SetIsInteracting(false);
        interactingPlayer = null;
        isInteracting = false;
        isAvailable = true;
    }

    public override void Enter(Player player)
    {
        if (numberOfIngredients > 0)
        {
            player.SetSelectedInteractable(this);
            DisplayTooltip(player);
            isAvailable = true;
        }
            
    }

    public override void Exit(Player player)
    {
        HideTooltip(player);
        isAvailable = false;
        player.SetSelectedInteractable(null);
    }

    private void DisplayTooltip(Player player)
    {
        string interactKey = player.GetControls().GetAction().ToString();
        tooltip.text = "Appuyez sur (" + interactKey + ") pour utiliser";
    }

    public override bool IsAvailable()
    {
        return isAvailable;
    }

    private void HideTooltip(Player player)
    {
        tooltip.text = "";
    }


    private void AddIngredient()
    {
        numberOfIngredients++;
        Displayingredients();



    }

    private void Displayingredients()
    {
        for (int i = 0; i < maxNumberOfIngredients; i++)
        {
            Ingredient currentIng = ings[i];
            if (currentIng != null)
            {
                Destroy(currentIng.gameObject);
                ings[i] = null;
            }

        }


        for (int i=0; i< numberOfIngredients; i++)
        {
            Ingredient currentIng = Instantiate(prefab, GetPos(i), Quaternion.identity).GetComponent<Ingredient>();
            float scaleChange = 0.5f * currentIng.gameObject.GetComponent<Collider>().bounds.size.z;
            currentIng.gameObject.transform.localScale = new Vector3(scaleChange, scaleChange, scaleChange);
            currentIng.SetIsFake(true);
            ings[i] = currentIng;
        }
    }

    private Vector3 GetPos(int i)
    {
        Vector3[] posXZ = { new Vector3(-0.25f,0, -0.25f), new Vector3(-0.25f, 0, 0.25f), new Vector3(0.25f, 0, -0.25f), new Vector3(0.25f,0, 0.25f), new Vector3(0, 0, 0) };
        return Vector3.Scale(GetComponent<Collider>().bounds.size, posXZ[i == maxNumberOfIngredients-1 && i%4 == 0 ? 4 : i%4]) + transform.position + GetPosY(i/4);
    }

    private Vector3 GetPosY(int i)
    {
        float ySize = ings[0] != null ? ings[0].gameObject.GetComponent<Renderer>().bounds.size.y : 0f;
        return new Vector3(0, i * ySize, 0);
    }

    public override bool IsDisabled(){
        return disabled;
    }

    public override void SetDisabled(bool value){
        disabled = value;
    }

}
