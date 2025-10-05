using UnityEngine;

public enum ArtPiece
{
    VASE,
    BOTTLE,
    CRYSTAL
}

public enum ArtColor
{
    BLUE,
    PINK,
    YELLOW
}

public class ArtAltar : Interactable
{
    [SerializeField] public ArtPiece currentPiece;
    [SerializeField] public ArtColor color;
    [SerializeField] GameObject puzzleUI;
    [SerializeField] DialogueLine[] interactText;
    [SerializeField] public Transform artTransform;
    [SerializeField] ArtPuzzleManager puzzle;
    bool interacted;
    public override void Interact()
    {
        if (GameFlags.GetFlag("gallery_puzzle_done")) return;
        GameFlags.SetFlag("player_can_move", false);
        DialogueManager.Instance.StartDialogue(interactText);
        puzzle.UpdateSelectedPiece(this.currentPiece);
        interacted = true;
    }

    void Update()
    {
        if (DialogueManager.Instance.IsDialogueActive())
        {
            // Cursor.lockState = CursorLockMode.Confined;
            // Cursor.visible = true;
            // GameFlags.SetFlag("player_can_move", false);
        }
        if (!DialogueManager.Instance.IsDialogueActive() && interacted)
        {
            puzzleUI.SetActive(true);
            interacted = false;
        }

        if (puzzleUI.activeSelf && Input.GetKeyDown(PlayerInput.Escape))
        {
            puzzleUI.SetActive(false);
        }
    }

}
