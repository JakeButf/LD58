using UnityEngine;

public class EndDialogue : MonoBehaviour
{
    [SerializeField] DialogueLine[] lines;
    void Start()
    {
        DialogueManager.Instance.StartDialogue(lines);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
