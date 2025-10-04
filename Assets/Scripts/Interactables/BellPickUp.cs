using System.Collections.Generic;
using UnityEngine;

public class BellPickup: PickUpInteractable
{
    public override void PickUpSpecial()
    {
        GameState.Instance.AddBell();
    }
}
