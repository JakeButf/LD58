using System.Collections.Generic;
using UnityEngine;

public class DialogueReader : Interactable
{

    [SerializeField] protected List<DialogueLine> dialogueLines;

    public override void Interact()
    {
        DialogueManager.Instance.StartDialogue(dialogueLines);
    }
}