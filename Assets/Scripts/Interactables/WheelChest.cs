using UnityEngine;

public class WheelChest : Interactable
{
    [SerializeField] GameObject puzzleCanvas;
    public override void Interact()
    {
        if(GameFlags.GetFlag("maritime_chest_unlocked")) return;
        puzzleCanvas.SetActive(true);
        GameFlags.SetFlag("player_can_move", false);
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }
    
}
