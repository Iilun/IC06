using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfos
{
    private int boatId;
    private ModelInfos  modelInfos;
    private PlayerControls controls;

    public PlayerInfos(int boatId, PlayerControls controls, int slot_id){
        this.boatId = boatId;
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
