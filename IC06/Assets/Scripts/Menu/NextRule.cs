using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextRule : MonoBehaviour
{
    private Sprite[] sprites1;
    private Sprite[] sprites2;
    private Image recette;
    private Text explication;
    private Image illustration;
    private int index = -1;
    private string[] texts = { "Poudre + Fer = Boulet Explosif \n Dégâts importants \n Explosion au bout d'une courte durée",
    "Dynamite gelée + Fer = Boulet Classique \n Dégats puissants au centre, et faible sur les cotés \n Attaque les points de vie sur une croix de 1 de coté",
    "Dynamite gelée + Poudre = Boulet de Feu \n Dégâts faibles mais persistants \n Désactive l'effet de glace. Peut être éteint avec un seau d'eau",
    "Dynamite gelée + Potion magique = Boulet de Gel \n Désactive l'effet de feu. Ralentissement sur zone (3x3) \n Blocage du personnage si touché (2-3 secondes)",
    "Fer + Potion magique = Boulet IEM \n Désactive les objets sur zone (3x3) \n Désactive les canons, la table de craft, les zones d'ingrédien",
    "Poudre + Potion magique = Boulet de Vent \n Agrandit la zone du feu"
    };

    // Start is called before the first frame update
    void Start()
    {
        sprites1 = Resources.LoadAll<Sprite>("Image/recette");
        sprites2 = Resources.LoadAll<Sprite>("Image/illustration");
        recette = GameObject.Find("Recette").GetComponent<Image>();
        explication = GameObject.Find("Explication").GetComponent<Text>();
        illustration = GameObject.Find("Illustration").GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Dochange()
    {
        
        index++;
        index %= 6;
        Debug.Log(index);
        recette.sprite = sprites1[index];
        explication.text = texts[index];
        if (index != 0)
        {
            illustration.rectTransform.sizeDelta = new Vector2(441.87f, 385.9178f);
        }
        else
        {
            illustration.rectTransform.sizeDelta = new Vector2(1325.6f, 385.9178f);
        }
        illustration.sprite = sprites2[index];
    }
}
