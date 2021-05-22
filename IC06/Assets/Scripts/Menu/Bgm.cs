using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bgm : MonoBehaviour
{
    public static Bgm instance = null;
    private string nameOfScene;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        
        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        nameOfScene = SceneManager.GetActiveScene().name;
        if (nameOfScene.Equals("Main"))
        {
            Destroy(this.gameObject);
        }
    }
}
