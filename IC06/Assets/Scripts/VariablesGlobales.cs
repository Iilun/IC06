using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class VariablesGlobales
{
    public static List<PlayerInfos> allPlayers;

    public static void AddToPlayers(PlayerInfos infos){
        if(allPlayers == null){
            allPlayers = new List<PlayerInfos>();
        }
        allPlayers.Add(infos);
        Debug.Log("Added " + infos.ToString());
    }

    public static List<PlayerInfos> GetAllPlayers(){
        return allPlayers;
    }

    public static void Reset(){
        allPlayers = new List<PlayerInfos>();
    }
}
