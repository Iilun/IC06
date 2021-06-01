using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

public class PlayerControls
{
    private char type;// K ou C

    private string horizontal;
    private string vertical;
    private string action;
    private string release;

    private string name;

    public const string NAME_LEFT_CLAVIER = "QZSD, E, Espace";
    public const string NAME_RIGHT_CLAVIER = "KOLM, P, Ctrl";
    public const string VIDE = "Pas de controles";
    public const string NAME_MANETTE_1 = "Manette 1";
    public const string NAME_MANETTE_2 = "Manette 2";

    public PlayerControls(char t, string h, string v, string a, string r, string name)
    {
        type = t;
        horizontal = h;
        vertical = v;
        action = a;
        release = r;
        this.name=name;
    }

    public char GetType()
    {
        return type;
    }

    public string GetName(){
        return name;
    }

    public string GetHorizontal()
    {
        return horizontal;
    }

    public string GetVertical()
    {
        return vertical;
    }

    public string GetAction()
    {
        return action;
    }

    public string GetRelease()
    {
        return release;
    }

    public string GetActionName(){
        switch(GetAction()){
            case "Interact":
                return "E";
            case "Interact1":
                return "P";
                //CAS MANETTES
            case "Interact3":
                if (Regex.IsMatch(Input.GetJoystickNames()[0], Regex.Escape("XBOX"), RegexOptions.IgnoreCase)){
                    return "A";
                } else {
                    return "CARRE";//PLAY ?
                }
            case "Interact4":
                if (Regex.IsMatch(Input.GetJoystickNames()[1], Regex.Escape("XBOX"), RegexOptions.IgnoreCase)){
                    return "A";
                } else {
                    return "CARRE";//PLAY ?
                }
        }
        return "";
    }

}
