using UnityEngine;

public class IntroController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] string[] text;
    [SerializeField] AudioClip poemAudio;
    void Start()
    {
        DialogueManager.Instance.StartDialogue(text, true, poemAudio, null);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
