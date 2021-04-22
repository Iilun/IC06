using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls
{
    private char type;// K ou C

    private string horizontal;
    private string vertical;
    private KeyCode action;
    private KeyCode release;

    public PlayerControls(char t, string h, string v, KeyCode a, KeyCode r)
    {
        type = t;
        horizontal = h;
        vertical = v;
        action = a;
        release = r;
    }

    public char GetType()
    {
        return type;
    }

    public string GetHorizontal()
    {
        return horizontal;
    }

    public string GetVertical()
    {
        return vertical;
    }

    public KeyCode GetAction()
    {
        return action;
    }

    public KeyCode GetRelease()
    {
        return release;
    }

}
