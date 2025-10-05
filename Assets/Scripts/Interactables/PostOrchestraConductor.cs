using UnityEngine;

public class PostOrchestraConductor: Interactable
{
    [SerializeField] CrystalPuzzle puzzle;
    [SerializeField] protected string[] preDoneLines;
    [SerializeField] protected string[] doneLines;
    [SerializeField] protected bool paperEffect = false;
    [Header("SFX (per-instance)")]
    [SerializeField] protected AudioClip advanceSfx;
    [SerializeField] protected AudioClip charSfx;
    public override void Interact()
    {
        if (!GameFlags.GetFlag("orchestra_room_open"))
        {
            DialogueManager.Instance.StartDialogue(preDoneLines, paperEffect, advanceSfx, charSfx);
            puzzle.StartPlayFull();
        }
        else
        {
            DialogueManager.Instance.StartDialogue(doneLines, paperEffect, advanceSfx, charSfx);
        }
    }
    
}
