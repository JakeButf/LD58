using Unity.VisualScripting;
using UnityEngine;

public class FlagDoor : Interactable
{
    [SerializeField] string unlockedFlag;
    [SerializeField] string[] lockedText;
    [SerializeField] string sceneToLoad;
    [SerializeField] Vector3 playerPos;
    [SerializeField] AudioClip doorOpenSfx;
 
    [SerializeField][Range(0f, 1f)] private float volume = .3f;

    public override void Interact()
    {
        if (!GameFlags.GetFlag(unlockedFlag))
        {
            DialogueManager.Instance.StartDialogue(lockedText);
        }
        else
        {
            GameState.Instance.LoadScene(sceneToLoad, playerPos, doorOpenSfx, volume);
        }
    }
}
