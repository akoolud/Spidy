using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHasInteract
{

    public void OnHover(bool state);
    public void Interact(PlayerController player);
}
