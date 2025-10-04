using UnityEngine;

public class DialogueReader : Interactable
{

    [SerializeField] protected string[] dialogueLines;
    [SerializeField] protected bool ghostEffect = false;
    [Header("SFX (per-instance)")]
    [SerializeField] protected AudioClip advanceSfx;
    [SerializeField] protected AudioClip charSfx;

    public override void Interact()
    {
        DialogueManager.Instance.StartDialogue(dialogueLines, ghostEffect, advanceSfx, charSfx);
    }
}