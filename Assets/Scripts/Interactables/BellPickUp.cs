using System.Collections.Generic;
using UnityEngine;

public class BellPickup: PickUpInteractable
{
    [SerializeField] private string flagToSet;
    [SerializeField] private AudioSource bellSound;
    public override void PickUpSpecial()
    {
        GameFlags.SetFlag(flagToSet, true);
        GameState.Instance.AddBell();
        bellSound.Play();
    }
}
