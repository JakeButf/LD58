using UnityEngine;

public class DialogueReader : Interactable
{

    [SerializeField] protected string[] dialogueLines;
    [SerializeField] protected bool paperEffect = false;

    public override void Interact()
    {
        DialogueManager.Instance.StartDialogue(dialogueLines);
    }
}