using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    [SerializeField] protected string promptMessage = "(E) to Inspect";

    public virtual string GetPromptMessage()
    {
        return promptMessage;
    }

    // Force child classes to define their interaction
    public abstract void Interact();
}
