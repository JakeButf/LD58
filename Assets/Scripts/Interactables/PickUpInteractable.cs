using System.Collections.Generic;
using UnityEngine;

public abstract class PickUpInteractable : Interactable
{
    public override void Interact()
    {
        Debug.Log("Picked up item: " + gameObject.name);
        PickUpSpecial();
        Destroy(gameObject);
    }
    
    public abstract void PickUpSpecial();
}
