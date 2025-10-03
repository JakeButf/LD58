using UnityEngine;

public class DialogueTester : MonoBehaviour
{
    void Update()
    {
        // Press "T" to start a test dialogue
        if (Input.GetKeyDown(KeyCode.T))
        {
            DialogueManager.Instance.StartDialogue(new string[]
            {
                "Hey there, this is a test dialogue!",
                "It should spell out the text.",
                "And when you click or press space, it moves to the next line.",
                "Finally, it closes when it's done."
            });
        }
    }
}
