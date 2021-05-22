using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModelChoice : MonoBehaviour
{
    PlayerInfos player1Infos;
    GameObject playerObject;
    // Start is called before the first frame update
    void Start()
    {
        player1Infos = new PlayerInfos(new PlayerControls('K',"Horizontal", "Vertical", "Interact1", "Action1", ""), 0);
        playerObject = Instantiate(GameTime.GetRedModel(), new Vector3(0,100,0), Quaternion.identity);
        playerObject.GetComponent<Player>().Instantiate(player1Infos, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Keypad9)){
            player1Infos.GetModelInfos().ChangeCouvreChef(true);
            player1Infos.GetModelInfos().SetModelToModelParameters(playerObject);
        }
        if(Input.GetKeyDown(KeyCode.Keypad8)){
            player1Infos.GetModelInfos().ChangeBarbe(false);
            player1Infos.GetModelInfos().SetModelToModelParameters(playerObject);
        }
        if(Input.GetKeyDown(KeyCode.Keypad7)){
            player1Infos.GetModelInfos().ChangeManteau(true);
            player1Infos.GetModelInfos().SetModelToModelParameters(playerObject);
        }
        if(Input.GetKeyDown(KeyCode.Keypad6)){
            player1Infos.GetModelInfos().ChangeJambes(false);
            player1Infos.GetModelInfos().SetModelToModelParameters(playerObject);
        }
        if(Input.GetKeyDown(KeyCode.Keypad5)){
            player1Infos.GetModelInfos().ChangeMains(false);
            player1Infos.GetModelInfos().SetModelToModelParameters(playerObject);
        }
        if(Input.GetKeyDown(KeyCode.Keypad4)){
            player1Infos.GetModelInfos().ChangeOeil(false);
            player1Infos.GetModelInfos().SetModelToModelParameters(playerObject);
        }

        if(Input.GetKeyDown(KeyCode.KeypadEnter)){
            GameObject yolo = Instantiate(GameTime.GetRedModel(), new Vector3(0,100,0), Quaternion.identity);
            yolo.GetComponent<Player>().Instantiate(player1Infos, 0);
        }
    }
}
