using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public bool bucketSpawn;
    public bool IsBucketSpawn(){
        return bucketSpawn;
    }
    public void GoToCharacter()
    {
        SceneManager.LoadScene("Character");
    }

    public void GoToSettingMenu()
    {
        SceneManager.LoadScene("SettingMenu");
    }

    public void GoToCreditMenu()
    {
        SceneManager.LoadScene("CreditMenu");
    }

    public void GotoMainMenu()
    {
        Time.timeScale = 1;
        bucketSpawn = false;
        SceneManager.LoadScene("MainMenu");
        VariablesGlobales.Reset();
        TileUtils.Reset();
        //GameTime.instance = null;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void GotoCelebration(int winnerId){
        bucketSpawn = false;
        VariablesGlobales.winnerId = winnerId;
        SceneManager.LoadScene("Winner");
        
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Main");
        
    }

    
}
