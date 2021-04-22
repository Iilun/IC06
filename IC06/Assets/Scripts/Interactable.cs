using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{

    public abstract void Interact(Player player);

    public abstract void Enter(Player player);

    public abstract void Exit(Player player);

    public abstract bool IsAvailable();

    public abstract bool IsDisabled();

    public abstract void SetDisabled(bool value);
}
