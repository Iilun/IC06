using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

}
