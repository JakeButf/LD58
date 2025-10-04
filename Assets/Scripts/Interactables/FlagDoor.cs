using UnityEngine;

public class FlagDoor : Interactable
{
    [SerializeField] string unlockedFlag;
    [SerializeField] string[] lockedText;

    public override void Interact()
    {
        if (!GameFlags.GetFlag(unlockedFlag))
        {
            DialogueManager.Instance.StartDialogue(lockedText);
        }
        else
        {
            //TODO: Load
        }

    }


    
}
