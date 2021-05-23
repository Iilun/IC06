using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHolder : MonoBehaviour
{
    public GameObject buttonPersonalize;
    public GameObject buttonNext;
    public GameObject buttonPrevious;
    public GameObject buttonAdd;
    public GameObject buttonDelete;
    public GameObject display;

    public int slot_id;

    public void Display(){
        buttonPersonalize.SetActive(true);
        buttonNext.SetActive(true);
        buttonPrevious.SetActive(true);
        display.SetActive(true);
        buttonAdd.SetActive(false);
        buttonDelete.SetActive(true);
    }

    public void UnDisplay(){
        display.SetActive(false);
        buttonAdd.SetActive(true);
        buttonDelete.SetActive(false);
    }
}
