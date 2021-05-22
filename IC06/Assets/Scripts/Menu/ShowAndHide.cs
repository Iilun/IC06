using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowAndHide : MonoBehaviour
{
    private CanvasGroup cg;

    public void Start()
    {
        cg = this.transform.GetComponent<CanvasGroup>();
    }

    public void Show()
    {
        cg.alpha = 1;
        cg.interactable = true;
        cg.blocksRaycasts = true;
    }

    public void Hide()
    {
        cg.alpha = 0;
        cg.interactable = true;
        cg.blocksRaycasts = false;
    }
}
