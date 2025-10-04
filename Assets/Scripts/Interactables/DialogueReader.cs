using UnityEngine;

public class DialogueReader : Interactable
{

    [SerializeField] protected string[] dialogueLines;
    [SerializeField] protected bool paperEffect = false;
    [Header("SFX (per-instance)")]
    [SerializeField] protected AudioClip advanceSfx;
    [SerializeField] protected AudioClip charSfx;

    public override void Interact()
    {
        DialogueManager.Instance.StartDialogue(dialogueLines, paperEffect, advanceSfx, charSfx);
    }
}