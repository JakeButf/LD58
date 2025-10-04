using UnityEngine;

public class WheelMoveRight: Interactable
{
    [SerializeField] WheelPuzzle wheel;
    public override void Interact()
    {
        wheel.MoveRight();
    }
    
}
