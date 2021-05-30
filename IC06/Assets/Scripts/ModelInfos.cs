using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelInfos
{
    
    private List<GameObject> CouvreChef;
    private int couvreChefIndex;
    private List<GameObject> Barbe;
    private int barbeIndex;
    private List<GameObject> Mains;
    private int mainIndex;
    private List<GameObject> Jambes;
    private int jambeIndex;
    private List<GameObject> Manteau;
    private int manteauIndex;
    private List<GameObject> Oeil;
    private int oeilIndex;

    public const int MODEL_ID_COUVRECHEF = 0;
    public const int MODEL_ID_BARBE = 1;
    public const int MODEL_ID_MAIN = 2;
    public const int MODEL_ID_JAMBES = 3;
    public const int MODEL_ID_MANTEAU = 4;
    public const int MODEL_ID_OEIL = 5;
    public ModelInfos(int slot_id)
    {
        GameObject defaultModel;
        
        if(slot_id %2 == 0){
            defaultModel = GameTime.GetBlueModel();
        } else {
            defaultModel = GameTime.GetRedModel();
        }
        InitListes(defaultModel);

        
        CouvreChef[0].SetActive(true);
        couvreChefIndex = 0;

        
        Barbe[0].SetActive(true);
        barbeIndex = 0;
        
        Mains[0].SetActive(true);
        mainIndex = 0;
        Jambes[0].SetActive(true);
        jambeIndex = 0;
        Manteau[0].SetActive(true);
        manteauIndex = 0;
        Oeil[0].SetActive(true);
        oeilIndex = 0;

        if (slot_id > 1){//New model, doit introduire variation
            ChangeCouvreChef(true);
            ChangeBarbe(true);
        }



    }

    public void ChangeCouvreChef(bool next){
        if (next){
            CouvreChef[couvreChefIndex++].SetActive(false);
            couvreChefIndex %= CouvreChef.Count;
            CouvreChef[couvreChefIndex].SetActive(true);
        } else {
            CouvreChef[couvreChefIndex--].SetActive(false);
            if(couvreChefIndex < 0) {
                couvreChefIndex = CouvreChef.Count - 1;
            }
            CouvreChef[couvreChefIndex].SetActive(true);
        }  
    }

    public void ChangeBarbe(bool next){
        if (next){
            Barbe[barbeIndex++].SetActive(false);
            barbeIndex %= Barbe.Count;
        } else {
            Barbe[barbeIndex--].SetActive(false);
            if(barbeIndex < 0) {
                barbeIndex = Barbe.Count - 1;
            } 
        }
        Barbe[barbeIndex].SetActive(true);
    }
    public void ChangeMains(bool next){
        if (next){
            Mains[mainIndex++].SetActive(false);
            mainIndex %= Mains.Count;
        } else {
            Mains[mainIndex--].SetActive(false);
            if(mainIndex < 0) {
                mainIndex = Mains.Count - 1;
            } 
        }
        Mains[mainIndex].SetActive(true);
    }
    public void ChangeJambes(bool next){
        if (next){
            Jambes[jambeIndex++].SetActive(false);
            jambeIndex %= Jambes.Count;
        } else {
            Jambes[jambeIndex--].SetActive(false);
            if(jambeIndex < 0) {
                jambeIndex = Jambes.Count - 1;
            } 
        }
        Jambes[jambeIndex].SetActive(true);
    }
    public void ChangeManteau(bool next){
        
        if (next){
            Manteau[manteauIndex++].SetActive(false);
            manteauIndex %= Manteau.Count;
        } else {
            Manteau[manteauIndex--].SetActive(false);
            if(manteauIndex < 0) {
                manteauIndex = Manteau.Count - 1;
            } 
        }
        
        Manteau[manteauIndex].SetActive(true);
    }
    public void ChangeOeil(bool next){
        if (next){
            Oeil[oeilIndex++].SetActive(false);
            oeilIndex %= Oeil.Count;
        } else {
            Oeil[oeilIndex--].SetActive(false);
            if(oeilIndex < 0) {
                oeilIndex = Oeil.Count - 1;
            } 
        }
        Oeil[oeilIndex].SetActive(true);
    }

    private void InitListes(GameObject model){
        CouvreChef = new List<GameObject>();
        Barbe = new List<GameObject>();
        Mains = new List<GameObject>();
        Jambes = new List<GameObject>();
        Manteau = new List<GameObject>();
        Oeil = new List<GameObject>();
        for (int i = 0; i < model.transform.childCount; i++){
            if (model.transform.GetChild(i).name != "rig"){
                switch(int.Parse(model.transform.GetChild(i).name.Substring(0,2))){
                    case MODEL_ID_COUVRECHEF:
                        CouvreChef.Add(model.transform.GetChild(i).gameObject);
                        break;
                    case MODEL_ID_BARBE:
                        Barbe.Add(model.transform.GetChild(i).gameObject);
                        break;
                    case MODEL_ID_MAIN:
                        Mains.Add(model.transform.GetChild(i).gameObject);
                        break;
                    case MODEL_ID_JAMBES:
                        Jambes.Add(model.transform.GetChild(i).gameObject);
                        break;
                    case MODEL_ID_MANTEAU:
                        Manteau.Add(model.transform.GetChild(i).gameObject);
                        break;
                    case MODEL_ID_OEIL:
                        Oeil.Add(model.transform.GetChild(i).gameObject);
                        break;
                }
            }
            
        }

        foreach(GameObject obj in CouvreChef){
            obj.SetActive(false);
        }

        foreach(GameObject obj in Barbe){
            obj.SetActive(false);
        }
        foreach(GameObject obj in Mains){
            obj.SetActive(false);
        }
        foreach(GameObject obj in Jambes){
            obj.SetActive(false);
        }
        foreach(GameObject obj in Manteau){
            obj.SetActive(false);
        }
        foreach(GameObject obj in Oeil){
            obj.SetActive(false);
        }
    }
    public GameObject SetModelToModelParameters(GameObject model){
        InitListes(model);
        CouvreChef[couvreChefIndex].SetActive(true);
        Barbe[barbeIndex].SetActive(true);
        Mains[mainIndex].SetActive(true);
        Jambes[jambeIndex].SetActive(true);
        Manteau[manteauIndex].SetActive(true);
        Oeil[oeilIndex].SetActive(true);
        return model;
    }


}
