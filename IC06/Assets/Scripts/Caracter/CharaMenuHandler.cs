using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharaMenuHandler : MonoBehaviour
{
    public static CharaMenuHandler instance;

    private List<PlayerControls> availableControls;

    private List<CharacterSelect> allChara;

    public const int DEFAULT_SLOT = 99;

    private int currentPersonnalizationSlot = DEFAULT_SLOT;

    public GameObject player3;

    public GameObject player4;

    public MainMenu mainMenu;

    public Text errorText;


    void Awake() {
        instance = this;
        instance.errorText.text = "";
        availableControls = new List<PlayerControls>();
        availableControls.Add(new PlayerControls('K',"Horizontal", "Vertical", "Interact", "Action", PlayerControls.NAME_LEFT_CLAVIER));
        availableControls.Add(new PlayerControls('K',"Horizontal1", "Vertical1", "Interact1", "Action1", PlayerControls.NAME_RIGHT_CLAVIER));
        availableControls.Add(new PlayerControls('V',"", "", "", "", PlayerControls.VIDE));




        availableControls.Add(new PlayerControls('C',"Horizontal1", "Vertical1", "Interact1", "Action1", "YOLO"));
        availableControls.Add(new PlayerControls('C',"Horizontal1", "Vertical1", "Interact1", "Action1", "YOLO2"));
    }

    public static void AddPlayer(ButtonHolder button){
        if (instance.availableControls.Count > 0){
            //On a un controle de disponible, on instancie un objet
            if(button.slot_id == 2){
                instance.player3.SetActive(true);
                instance.player3.GetComponent<CharacterSelect>().SetSlot(button.slot_id);
                instance.player3.GetComponent<CharacterSelect>().GetDisplay().Display(instance.player3.GetComponent<CharacterSelect>().GetInfos().GetControls());
            } else {
                instance.player4.SetActive(true);
                instance.player4.GetComponent<CharacterSelect>().SetSlot(button.slot_id);
                instance.player4.GetComponent<CharacterSelect>().GetDisplay().Display(instance.player4.GetComponent<CharacterSelect>().GetInfos().GetControls());

            }
            
            //MODIF DE L AFFICHAGE, AJOUT DES BOUTONS, NOTAMMENT CELUI DE PERSO
            button.Display();
            

        } else {
            //ERROR PAS DE MANNETTE
            SetErrorMessage("Ajout de manette necessaire");
            Debug.Log("Ajout de manette necessaire");
        }
    }
    public static void AddCharaSelect(CharacterSelect chara){
        if (instance.allChara == null){
            instance.allChara = new List<CharacterSelect>();
        }
        instance.allChara.Add(chara);
        instance.allChara.Sort((a,b) => a.GetSlot() > b.GetSlot() ? 1 : 0);

        foreach(CharacterSelect c in instance.allChara){
            Debug.Log(c.gameObject);
        }

    }
    public static PlayerControls GetNextAvailableControl(PlayerControls current){
        if (instance.availableControls.Count == 0){
            return null;
        }
        
        PlayerControls next = instance.availableControls[0];
        if(current != null){
            instance.availableControls.Add(current);
        } else {
            if (next.GetName() == PlayerControls.VIDE){
                next = instance.availableControls[1];//PAS SUR
            }
        }
        instance.availableControls.RemoveAt(0);
        return next;

    }

    public static void SetCurrentPersonnalizationSlot(int slot){
        instance.currentPersonnalizationSlot = slot;
    }

    public static int GetCurrentPersonnalizationSlot(){
        return instance.currentPersonnalizationSlot;
    }

    public void ChangeBarbe(bool sens){
        CharacterSelect chara = instance.allChara[GetCurrentPersonnalizationSlot()];
        chara.GetInfos().GetModelInfos().ChangeBarbe(sens);
        chara.GetInfos().GetModelInfos().SetModelToModelParameters(chara.GetGameObject());
    }

    public void ChangeCouvreChef(bool sens){
        Debug.Log(GetCurrentPersonnalizationSlot());
        Debug.Log(instance.allChara);
        CharacterSelect chara = instance.allChara[GetCurrentPersonnalizationSlot()];
        chara.GetInfos().GetModelInfos().ChangeCouvreChef(sens);
        chara.GetInfos().GetModelInfos().SetModelToModelParameters(chara.GetGameObject());
    }

    public void ChangeOeil(bool sens){
        CharacterSelect chara = instance.allChara[GetCurrentPersonnalizationSlot()];
        chara.GetInfos().GetModelInfos().ChangeOeil(sens);
        chara.GetInfos().GetModelInfos().SetModelToModelParameters(chara.GetGameObject());
    }
    public void ChangeMains(bool sens){
        CharacterSelect chara = instance.allChara[GetCurrentPersonnalizationSlot()];
        chara.GetInfos().GetModelInfos().ChangeMains(sens);
        chara.GetInfos().GetModelInfos().SetModelToModelParameters(chara.GetGameObject());
    }
    public void ChangeJambes(bool sens){
        CharacterSelect chara = instance.allChara[GetCurrentPersonnalizationSlot()];
        chara.GetInfos().GetModelInfos().ChangeJambes(sens);
        chara.GetInfos().GetModelInfos().SetModelToModelParameters(chara.GetGameObject());
    }
    public void ChangeManteau(bool sens){
        CharacterSelect chara = instance.allChara[GetCurrentPersonnalizationSlot()];
        chara.GetInfos().GetModelInfos().ChangeManteau(sens);
        chara.GetInfos().GetModelInfos().SetModelToModelParameters(chara.GetGameObject());
    }

    public void ValidateAndGoToGame(){
        bool errorControls = false;
        CharacterSelect problemCharacter = null;
        foreach (CharacterSelect c in instance.allChara){
            if (c.GetInfos().GetControls().GetName() == PlayerControls.VIDE){
                errorControls = true;
                problemCharacter =c;
            }
        }

        if (errorControls){
            SetErrorMessage("Le joueur " + (problemCharacter.GetSlot() +1) + " n'a pas de controles");
            return;
        }
        foreach (CharacterSelect c in instance.allChara){
            VariablesGlobales.AddToPlayers(c.GetInfos());
        }
        mainMenu.PlayGame();
    }

    public static void SetErrorMessage(string errorMsg){
        instance.errorText.text = errorMsg;
        instance.StartCoroutine(EffaceText());
    }

    private static IEnumerator EffaceText(){
        yield return new WaitForSeconds(6);
        instance.errorText.text = "";
    }


}
