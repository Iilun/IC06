using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelect : MonoBehaviour
{

    [SerializeField]
    private int slot_id = CharaMenuHandler.DEFAULT_SLOT;
    private PlayerInfos infos;
    public Camera mainCamera;
    private Vector3 baseCamera;

    public Canvas boatCanvas;

    public Canvas playerCanvas;

    private GameObject playerObject;

    public ControlsDisplay display; 

    public ControlsDisplay GetDisplay(){
        return display;
    }

    public int GetSlot(){
        return slot_id;
    }


    // Start is called before the first frame update
    void Start()
    {
        /* length = characterPrefabs.Length;
        characterShow = new GameObject[length];

        for (int i = 0; i < length; i++)
        {
            characterShow[i] = (GameObject)Instantiate(characterPrefabs[i], transform.position, transform.rotation);
        }

        UpdateCharacterShow(); */
        baseCamera = mainCamera.transform.position;
        if (slot_id != CharaMenuHandler.DEFAULT_SLOT) {
            infos = new PlayerInfos(CharaMenuHandler.GetNextAvailableControl(null),slot_id);
        
            if (slot_id == 0){
                playerObject = Instantiate(GameTime.GetRedModel(), transform.position, transform.rotation);
                playerObject.GetComponent<Player>().InstantiateMenu(infos);
                infos.GetModelInfos().SetModelToModelParameters(playerObject);
            } else if (slot_id ==1){
                playerObject = Instantiate(GameTime.GetBlueModel(), transform.position, transform.rotation);
                playerObject.GetComponent<Player>().InstantiateMenu(infos);
                infos.GetModelInfos().SetModelToModelParameters(playerObject);
            }
            CharaMenuHandler.AddCharaSelect(this);
            if (display != null)
            display.Display(infos.GetControls());
        } 
        
        
    }

    public void SetSlot(int slot_id){
            infos = new PlayerInfos(CharaMenuHandler.GetNextAvailableControl(null),slot_id);
            this.slot_id = slot_id;
            if (slot_id == 2){
                playerObject = Instantiate(GameTime.GetRedModel(), transform.position, transform.rotation);
                playerObject.GetComponent<Player>().InstantiateMenu(infos);
                infos.GetModelInfos().SetModelToModelParameters(playerObject);
            } else if (slot_id ==3){
                playerObject = Instantiate(GameTime.GetBlueModel(), transform.position, transform.rotation);
                playerObject.GetComponent<Player>().InstantiateMenu(infos);
                infos.GetModelInfos().SetModelToModelParameters(playerObject);
            }
            CharaMenuHandler.AddCharaSelect(this);
            display.Display(infos.GetControls());
    }

    public PlayerInfos GetInfos(){
        return infos;
    }

    public GameObject GetGameObject(){
        return playerObject;
    }

    void UpdateCharacterShow()
    {
        
    }


    // Update is called once per frame
    void Update()
    {

    }

    public void PreviousControl()
    {
        if(slot_id != CharaMenuHandler.DEFAULT_SLOT){
            PlayerControls tmpControls = CharaMenuHandler.GetNextAvailableControl(infos.GetControls());
            if (tmpControls != null){
                infos.ChangeControls(tmpControls);
               
                //UPDATE AFFICHAGE CONTROLS
                display.Display(infos.GetControls());
            } else {
                //ERROR

                //CharaMenuHandler.SetErrorMessage("Plus de controles disponibles, ajoutez une manette");
               
            }
        }
        
    }

    public void NextControl()
    {
        PreviousControl();
    }

    public void Personalize(){
        if(slot_id != CharaMenuHandler.DEFAULT_SLOT){
            mainCamera.transform.position = transform.position + new Vector3(0, 14, -15);
            playerCanvas.gameObject.SetActive(true);
            boatCanvas.gameObject.SetActive(false);
            CharaMenuHandler.SetCurrentPersonnalizationSlot(slot_id);
        }
    }

    public void Delete(){
        this.slot_id = 99;
        Destroy(playerObject);
        infos = null;
    }

    public void UnPersonalize(){
        mainCamera.transform.position = baseCamera;
        playerCanvas.gameObject.SetActive(false);
        boatCanvas.gameObject.SetActive(true);
        CharaMenuHandler.SetCurrentPersonnalizationSlot(CharaMenuHandler.DEFAULT_SLOT);
    }

}
