using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ControlsDisplay : MonoBehaviour
{
    public GameObject imageClavier;
    public GameObject imageManette;
    public Text nom;
    public void Display(PlayerControls controls){
        if(controls.GetType() == 'K'){
            imageClavier.SetActive(true);
            imageManette.SetActive(false);
        } else if(controls.GetType() == 'C') {
            imageManette.SetActive(true);
            imageClavier.SetActive(false);
        } else {
            imageManette.SetActive(false);
            imageClavier.SetActive(false);
        }

        nom.text = controls.GetName();
    }
}
