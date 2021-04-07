using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private Slider slider;

    [SerializeField]
    private Gradient gradient;

    [SerializeField]
    private Image fill;

    [SerializeField]
    private Text info1;

    [SerializeField]
    private Text info2;

    [SerializeField]
    private Text info3;

    [SerializeField]
    private float fadeOutLength = 4f;

    public void Start()
    {
        info1.text = "";
        info2.text = "";
        info3.text = "";
    }

    public void SetHealth(int health)
    {
        slider.value = health;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
        fill.color = gradient.Evaluate(1f);

    }

    public int GetHealth()
    {
        return (int) slider.value;
    }

    public void AddInfo(int dmg, int damageType)
    {
        //Génération de la string
        string infoStr = "- " + dmg.ToString();

        //Choix du mesh en fonction des dispos
        Text infoText;

        if(info1.text == "")
        {
            infoText = info1;
        } else if(info2.text == "")
        {
            infoText = info2;
        } else if(info3.text == "")
        {
            infoText = info3;
        } else
        {
            //Suppression de l'info 1 pour roulement
            Roll();
            infoText = info3;
        }

        Color newColor = Color.black;
        //Set de la couleur du mesh
        if (damageType.Equals(Bullet.DIRECT_DAMAGE))
        {
            newColor = new Color(251, 103, 14, 1);
        } else
        {
            //TODO
        }

        //Routine pour que le truc fade out
        StartCoroutine(ChangeInfo(1f, infoText, infoStr, newColor, false));

    }

    public void Roll()
    {
        
        if (info1.text != "")
        {
            //Declenche les fades et puis change l'info
            StartCoroutine(ChangeInfo(1f, info1, info2.text, info2.color, true));
            StartCoroutine(ChangeInfo(1f, info2, info3.text, info3.color, false));
            StartCoroutine(ChangeInfo(1f, info3, "", Color.black, false));
        }
        

    }

    private IEnumerator FadeTextToZeroAlpha(float t, Text i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
        while (i.color.a > 0.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t));
            yield return null;
        }

        //Reset
        i.text = "";
        i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
    }

    private IEnumerator ChangeInfo(float t, Text i, string newInfo, Color newColor, bool fade)
    {
        if (i.text != "" && fade)
        {
            yield return StartCoroutine(FadeTextToZeroAlpha(t, i));
        } else if (i.text != "")
        {
            yield return new WaitForSeconds(t);
        }
        i.text = newInfo;
        i.color = newColor;

        
        yield return new WaitForSeconds(fadeOutLength);
        Roll();
    }
}
