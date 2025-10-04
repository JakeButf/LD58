using UnityEngine;

public class WheelMoveLeft: Interactable
{
    [SerializeField] WheelPuzzle wheel;
    public override void Interact()
    {
        wheel.MoveLeft();
        
    }

    
}
