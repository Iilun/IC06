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

    private int controllerSize;

    private bool isModified;

    public ButtonHolder buttonHolderPlayer3;
    public ButtonHolder buttonHolderPlayer4;
    


    void Awake() {
        instance = this;
        instance.errorText.text = "";
        availableControls = new List<PlayerControls>();
        availableControls.Add(new PlayerControls('K',"Horizontal", "Vertical", "Interact", "Action", PlayerControls.NAME_LEFT_CLAVIER));
        availableControls.Add(new PlayerControls('K',"Horizontal1", "Vertical1", "Interact1", "Action1", PlayerControls.NAME_RIGHT_CLAVIER));
        availableControls.Add(new PlayerControls('V',"VoidAxis", "VoidAxis", "VoidKey", "VoidKey", PlayerControls.VIDE));


        //availableControls.Add(new PlayerControls('C',"Horizontal1", "Vertical1", "Interact1", "Action1", PlayerControls.NAME_MANETTE_1));//DELETE
        //availableControls.Add(new PlayerControls('C',"Horizontal4", "Vertical4", "Interact4", "Action4", PlayerControls.NAME_MANETTE_2));

        
        controllerSize = Input.GetJoystickNames().Length;
        if (controllerSize > 0 && Input.GetJoystickNames()[0] == ""){
            controllerSize -= 1;
        }
        //Debug.Log("Controller size= " + controllerSize);
        if(controllerSize == 1){
            //Debug.Log(Input.GetJoystickNames()[0]);
            availableControls.Add(new PlayerControls('C',"Horizontal3", "Vertical3", "Interact3", "Action3", PlayerControls.NAME_MANETTE_1));
        }
        if(controllerSize == 2){
            availableControls.Add(new PlayerControls('C',"Horizontal4", "Vertical4", "Interact4", "Action4", PlayerControls.NAME_MANETTE_2));
        }
    }

    void Update(){
        int newControllerSize = Input.GetJoystickNames().Length;
        if (newControllerSize > 0 &&Input.GetJoystickNames()[0] == ""){
            newControllerSize -= 1;
        }
        if (newControllerSize > controllerSize){
            SetInfoMessage("Nouvelle manette connectée");
         
            if(newControllerSize == 1){
                availableControls.Add(new PlayerControls('C',"Horizontal3", "Vertical3", "Interact3", "Action3", PlayerControls.NAME_MANETTE_1));
            }
            if(newControllerSize == 2){
                availableControls.Add(new PlayerControls('C',"Horizontal4", "Vertical4", "Interact4", "Action4", PlayerControls.NAME_MANETTE_2));
            }
        } else if (newControllerSize < controllerSize){
            SetInfoMessage("Mannette déconnectée");
            
            if(newControllerSize == 0){
                availableControls.RemoveAt(availableControls.Count -1);
                foreach(CharacterSelect c in instance.allChara){
                    if(c.GetInfos().GetControls().GetName() == PlayerControls.NAME_MANETTE_1){
                        c.GetInfos().ChangeControls(new PlayerControls('V',"VoidAxis", "VoidAxis", "VoidKey", "VoidKey", PlayerControls.VIDE));
                        c.GetDisplay().Display(c.GetInfos().GetControls());
                    }
                }

            }
            if(newControllerSize == 1){
                availableControls.RemoveAt(availableControls.Count -1);

                foreach(CharacterSelect c in instance.allChara){
                    if(c.GetInfos().GetControls().GetName() == PlayerControls.NAME_MANETTE_2){
                        c.GetInfos().ChangeControls(new PlayerControls('V',"VoidAxis", "VoidAxis", "VoidKey", "VoidKey", PlayerControls.VIDE));
                        c.GetDisplay().Display(c.GetInfos().GetControls());
                    }
                 }
                //enlever le concerné ici et le passer en sans controle !
            }
        }
        controllerSize = newControllerSize;
    }
    public static void AddPlayer(ButtonHolder button){
        if (instance.availableControls.Count > 1){
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

    public static void DeletePlayer(ButtonHolder button){
        CharacterSelect toRemove = null;
        foreach(CharacterSelect c in instance.allChara){
            if(c.GetSlot() == button.slot_id){
                toRemove =c;
               
                Debug.Log("Removed "  + c.gameObject);
            }
        }
        if (toRemove != null){
            instance.allChara.Remove(toRemove);
            instance.availableControls.Add(toRemove.GetInfos().GetControls());
        }
       
        
        if(button.slot_id == 2){
                
                instance.player3.GetComponent<CharacterSelect>().Delete();
                instance.player3.SetActive(false);
            } else {
                instance.player4.GetComponent<CharacterSelect>().Delete();
                instance.player4.SetActive(false);

        }

        
        button.UnDisplay();
    }
    public static void AddCharaSelect(CharacterSelect chara){
        if (instance.allChara == null){
            instance.allChara = new List<CharacterSelect>();
        }
        instance.allChara.Add(chara);
        instance.allChara.Sort((a,b) => a.GetSlot() > b.GetSlot() ? 1 : 0);

        

    }
    public static PlayerControls GetNextAvailableControl(PlayerControls current){
        if (instance.availableControls.Count == 0){
            return null;
        }
        
        PlayerControls next = instance.availableControls[0];
        if(current != null){
            instance.availableControls.Add(current);
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

        if(instance.allChara.Count %2 != 0){
            SetErrorMessage("Le nombre de joueur dans chaque équipe n'est pas le même !");
            return;
        }

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
            Debug.Log("Add global " + c.GetInfos().GetControls().GetName());
        }
        instance.allChara = new List<CharacterSelect>();
        mainMenu.PlayGame();
    }

    public static void SetErrorMessage(string errorMsg){
        instance.errorText.color = Color.red;
        instance.errorText.text = errorMsg;
        instance.StartCoroutine(EffaceText());
    }

    public static void SetInfoMessage(string errorMsg){
        instance.errorText.color = Color.white;
        instance.errorText.text = errorMsg;
        instance.StartCoroutine(EffaceText());
    }

    private static IEnumerator EffaceText(){
        yield return new WaitForSeconds(6);
        instance.errorText.text = "";
    }


}
