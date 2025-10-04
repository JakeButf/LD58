using UnityEngine;

public class WheelChest : Interactable
{
    [SerializeField] GameObject puzzleCanvas;
    public override void Interact()
    {
        puzzleCanvas.SetActive(true);
        GameFlags.SetFlag("player_can_move", false);
    }
    
}
