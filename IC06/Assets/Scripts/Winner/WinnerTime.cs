using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WinnerTime : MonoBehaviour
{
    //TODO A CHANGER CEUX LA
    public GameObject defaultBlue;
    public GameObject defaultRed;
    public static WinnerTime instance;

    public const int BLUE_BOAT_ID = 0;

    public Text endCanvasText;


    public Boat front;

    public Boat back;


    public static Boat GetFront(){
        return instance.front;
    }

    public static Boat GetBack(){
        return instance.back;
    }
    public static GameObject GetBlueModel(){
        return instance.defaultBlue;
    }

    public static GameObject GetRedModel(){
        return instance.defaultRed;
    }
    void Awake()
    {
        instance = this; 
        StartWinnerPhase(VariablesGlobales.winnerId);
    }
    public void StartWinnerPhase(int winnerId){
        Debug.Log(winnerId);
        Debug.Log(instance);
        SpawnPlayers(winnerId);
        if(winnerId != BLUE_BOAT_ID){
            instance.endCanvasText.color = Color.red;
        } else {
            instance.endCanvasText.color = Color.blue;
        }

    }

    private void SpawnPlayers(int winnerId){
        List<PlayerInfos> playerInfosList;
        playerInfosList = VariablesGlobales.GetAllPlayers();



        if (playerInfosList.Count <3){
            //Deux joueurs
            foreach(PlayerInfos p in playerInfosList){
                GameObject playerObject;
                if (p.GetBoatId() == BLUE_BOAT_ID){
                    playerObject = Instantiate(GetBlueModel(), new Vector3(0,100,0), Quaternion.Euler(0,180,0));
                } else {
                    Debug.Log(GetRedModel());
                    playerObject = Instantiate(GetRedModel(), new Vector3(0,100,0), Quaternion.Euler(0,180,0));
                }

                playerObject.GetComponent<Player>().InstantiateCeleb(p, p.GetBoatId() == winnerId, p.GetBoatId() == winnerId ? 0 : 5);
                
                
            }
            
        } else {
            //4 joueurs
            playerInfosList.Sort((a,b) =>  (a.GetBoatId() != b.GetBoatId())? 1 : 0);
            float blue_offset = 6;
            float red_offset = 6;
            foreach(PlayerInfos p in playerInfosList){
                GameObject playerObject;
                if (p.GetBoatId() == BLUE_BOAT_ID){
                    playerObject = Instantiate(GetBlueModel(), new Vector3(0,100,0), Quaternion.Euler(0,180,0));
                    playerObject.GetComponent<Player>().InstantiateCeleb(p, p.GetBoatId() == winnerId, blue_offset);
                    blue_offset = -6;
                } else {
                    playerObject = Instantiate(GetRedModel(), new Vector3(0,100,0), Quaternion.Euler(0,180,0));
                    playerObject.GetComponent<Player>().InstantiateCeleb(p, p.GetBoatId() == winnerId, red_offset);
                    red_offset = -6;
                }  
                
            }

        }
    }

    

}
