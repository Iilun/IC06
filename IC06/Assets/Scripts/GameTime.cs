using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameTime : MonoBehaviour
{
    //TODO A CHANGER CEUX LA
    public GameObject defaultBlue;
    public GameObject defaultRed;
    public static GameTime instance;

    public MainMenu mainMenu;

    public const int BLUE_BOAT_ID = 0;
    public Canvas gameCanvas;

    public Canvas endCanvas;

    public Canvas pauseCanvas;

    public Text endCanvasText;


    public Boat redBoat;

    public Boat blueBoat;

    public bool isGameMode;
    public bool isEnded;

    void Awake()
    {
        Debug.Log("AWAKEEEEEEEEEEEEEEEEEEEE");
        instance = this;
        if(isGameMode){
            StartGamePhase();
        }
        
    }
    public static GameObject GetRedModel(){
        return instance.defaultRed;
    }

    public static GameObject GetBlueModel(){
        return instance.defaultBlue;
    }
    public static  Boat GetRedBoat(){
        return instance.redBoat;
    }
    public static Boat GetBlueBoat(){
        return instance.blueBoat;
    }
    private void StartGamePhase(){
       

        instance.endCanvas.gameObject.SetActive(false);
        instance.gameCanvas.gameObject.SetActive(true);
        isEnded = false;
        //Changer la cam / La scene ? 
        SpawnPlayers();
       

    }

    private void SpawnPlayers(){
        List<PlayerInfos> playerInfosList;
        playerInfosList = VariablesGlobales.GetAllPlayers();



        if (playerInfosList.Count <3){
            //Deux joueurs
            foreach(PlayerInfos p in playerInfosList){
                GameObject playerObject;
                if (p.GetBoatId() == BLUE_BOAT_ID){
                    playerObject = Instantiate(GetBlueModel(), new Vector3(0,100,0), Quaternion.identity);
                } else {
                    playerObject = Instantiate(GetRedModel(), new Vector3(0,100,0), Quaternion.identity);
                }  
                playerObject.GetComponent<Player>().Instantiate(p, 0);
            }
            
        } else {
            //4 joueurs
            playerInfosList.Sort((a,b) =>  (a.GetBoatId() != b.GetBoatId())? 1 : 0);
            float offset = 5;
            foreach(PlayerInfos p in playerInfosList){
                GameObject playerObject;
                if (p.GetBoatId() == BLUE_BOAT_ID){
                    playerObject = Instantiate(GetBlueModel(), new Vector3(0,100,0), Quaternion.identity);
                } else {
                    playerObject = Instantiate(GetRedModel(), new Vector3(0,100,0), Quaternion.identity);
                }  
                playerObject.GetComponent<Player>().Instantiate(p, offset);
                offset = offset - (Mathf.Sign(offset) * 10);
                Debug.Log(offset);
            }

        }
    }

    public static void Stop(){
        instance.gameCanvas.gameObject.SetActive(false);
        instance.endCanvas.gameObject.SetActive(true);
        
        instance.isEnded = true;
        int winnerId;
        if(instance.blueBoat.GetHealth()< 0){
            winnerId = 0;
        } else {
            winnerId = 1;
        }
        instance.mainMenu.GotoCelebration(winnerId);
       // DestroyableUtils.instance = null;
        //ItemUtils.instance = null;

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && isGameMode &&!isEnded)
        {
            pauseCanvas.gameObject.SetActive(true);
            //gameCanvas.gameObject.SetActive(false);
            Time.timeScale = 0;
        }
    }

    void OnApplicationQuit()
    {
        instance.endCanvas.gameObject.SetActive(false);
    }

    public void Depause(){
        pauseCanvas.gameObject.SetActive(false);
        //gameCanvas.gameObject.SetActive(true);
        Time.timeScale = 1;
    } 

    

}
