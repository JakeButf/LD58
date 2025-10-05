using UnityEngine;

public class PaintingInteractable : Interactable
{
    [SerializeField] ArtPuzzleManager puzzle;
    [SerializeField] DialogueLine[] interactLines;
    [SerializeField] GameObject puzzleUI;
    [SerializeField] ArtAltar altar;
    bool interacted = false;
    public override void Interact()
    {
        if (GameFlags.GetFlag("art_puzzle_done")) return;
        GameFlags.SetFlag("player_can_move", false);
        DialogueManager.Instance.StartDialogue(interactLines);
        interacted = true;
        puzzle.UpdateSelectedPiece(altar.currentPiece);
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
