using System.Collections.Generic;
using UnityEngine;

public class BellPickup: PickUpInteractable
{
    [SerializeField] private string flagToSet;
    public override void PickUpSpecial()
    {
        GameFlags.SetFlag(flagToSet, true);
        GameState.Instance.AddBell();
    }
}
