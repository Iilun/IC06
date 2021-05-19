using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ice : MonoBehaviour
{
    private bool startFreeze;

    private List<Player> affectedPlayers = new List<Player>();
    void Start() {
        startFreeze = true;
        StartCoroutine(StartFreeze());
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Player>() != null)
        {
            if(startFreeze){
                StartCoroutine(FreezePlayer(other.GetComponent<Player>()));
            }
            affectedPlayers.Add(other.gameObject.GetComponent<Player>());
            other.gameObject.GetComponent<Player>().SetSlowed(true);
        }
    }

    void OnTriggerExit(Collider other)//Probleme pas called on destroy donc 
    {
        if (other.gameObject.GetComponent<Player>() != null)
        {
            other.gameObject.GetComponent<Player>().SetSlowed(false);
            affectedPlayers.Remove(other.gameObject.GetComponent<Player>());
        }
    }

    private IEnumerator StartFreeze(){
        yield return new WaitForSeconds(0.1f);
        startFreeze = false;
    }

    private IEnumerator FreezePlayer(Player player){


        Vector3 playerPos = player.gameObject.transform.position;
        Vector3 pos = new Vector3(playerPos.x, playerPos.y, playerPos.z);
        GameObject iceCube = Instantiate(DestroyableUtils.GetIceCube(), pos, Quaternion.Euler(-90,0,0));
        float ice_cube_height_offset = iceCube.GetComponent<Collider>().bounds.size.y;
        float base_height = transform.position.y - (0.5f * GetComponent<Collider>().bounds.size.y);
        float final_height = base_height + (0.5f * ice_cube_height_offset);
        
        iceCube.transform.position = iceCube.transform.position + new Vector3(0, final_height, 0);
        player.SetIsInteracting(true);
        yield return new WaitForSeconds(Bullet.ICE_FREEZE_TIME);
        player.SetIsInteracting(false);
        Destroy(iceCube);
    }

    void OnDestroy()
     {
        foreach(Player p in affectedPlayers){
            p.SetSlowed(false);
        }
     }
}
