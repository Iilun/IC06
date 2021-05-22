using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfos
{
    private int boatId;
    private ModelInfos  modelInfos;
    private PlayerControls controls;

    public PlayerInfos(PlayerControls controls, int slot_id){
        Debug.Log("Creating new player, slot =" + slot_id);
        boatId = (slot_id +1)  %2 ;//TODO
        Debug.Log("Boat =" + boatId);
        this.modelInfos = new ModelInfos(slot_id);
        this.controls = controls;
    }

    public ModelInfos GetModelInfos(){
        return modelInfos;
    }

    public void ChangeControls(PlayerControls newControls){
        controls = newControls;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public int GetBoatId(){
        return boatId;
    }
    public PlayerControls GetControls(){
        return controls;
    }
}
