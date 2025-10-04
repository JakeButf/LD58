using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private float typingSpeed = 0.03f;
    [Header("Audio")]

    [SerializeField][Range(0f, 1f)] private float advanceVolume = 1f;
    [SerializeField][Range(0f, 1f)] private float charVolume = 0.6f;
    [SerializeField] private float charPitchVariance = 0.05f;
    [SerializeField] private int charSfxEveryN = 2;
    private AudioSource audioSource;

    private Queue<DialogueLine> sentences;
    private Coroutine typingCoroutine;
    private bool isTyping;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        DontDestroyOnLoad(gameObject); // persist across scenes if needed
        sentences = new Queue<DialogueLine>();
        dialoguePanel.SetActive(false);
        // Ensure an AudioSource exists for SFX playback
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.playOnAwake = false;
        }
    }

    void Update()
    {
        if (dialoguePanel.activeSelf)
        {
            if (Input.GetMouseButtonDown(0)) // left click
            {
                DisplayNextSentence();
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                DisplayNextSentence();
            }
        }
    }
    public void StartDialogue(string[] dialogueLines, bool paperEffect = false, AudioClip advanceClip = null, AudioClip charClip = null)
    {
        List<DialogueLine> lines = buildDialogueLines(dialogueLines, paperEffect, advanceClip, charClip);
        StartDialogue(lines);
    }

    public void StartDialogue(IEnumerable<DialogueLine> dialogueLines)
    {
        sentences.Clear();

        foreach (DialogueLine line in dialogueLines)
        {
            sentences.Enqueue(line);
        }

        dialoguePanel.SetActive(true);
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {

        if (isTyping)
        {
            // Skip typing animation instantly
            StopCoroutine(typingCoroutine);
            DialogueLine nextLine = sentences.Dequeue();
            dialogueText.text = nextLine.text;
            isTyping = false;
            return;
        }

        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        DialogueLine sentence = sentences.Peek();
        Image image = dialoguePanel.GetComponent<Image>();
        if (image != null)
        {
            Color c = image.color;
            c.a = sentence.paperEffect ? 1f : 0f;
            image.color = c;
        }
        // Play advance SFX whenever DisplayNextSentence is invoked (can be skip or next)
        if (sentence.advanceClip != null && audioSource != null)
        {
            audioSource.PlayOneShot(sentence.advanceClip, advanceVolume);
        }
        typingCoroutine = StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(DialogueLine sentence)
    {
        isTyping = true;
        dialogueText.text = "";

        int visibleCount = 0; // count of visible (non-whitespace, non-tag) chars

        // sanity clamp
        if (charSfxEveryN < 1) charSfxEveryN = 1;
        //little pause before starting to type
        yield return new WaitForSeconds(.3f);
        foreach (char letter in sentence.text.ToCharArray())
        {
            dialogueText.text += letter;

            if (!char.IsWhiteSpace(letter))
            {
                visibleCount++;
                if (sentence.charClip != null && audioSource != null && (visibleCount % charSfxEveryN) == 0)
                {
                    float originalPitch = audioSource.pitch;
                    audioSource.pitch = 1f + Random.Range(-charPitchVariance, charPitchVariance);
                    audioSource.PlayOneShot(sentence.charClip, charVolume);
                    audioSource.pitch = originalPitch;
                }

                yield return new WaitForSeconds(typingSpeed);
            }
        }
        isTyping = false;
        sentences.Dequeue();
    }

    void EndDialogue()
    {
        dialoguePanel.SetActive(false);
    }

    public bool IsDialogueActive()
    {
        return dialoguePanel.activeSelf;
    }

    public List<DialogueLine> buildDialogueLines(string[] lines, bool paperEffect = false, AudioClip advanceClip = null, AudioClip charClip = null)
    {
        List<DialogueLine> dialogueLines = new List<DialogueLine>();
        foreach (string s in lines)
        {
            DialogueLine line = new DialogueLine(s, paperEffect, advanceClip, charClip);
            dialogueLines.Add(line);
        }
        return dialogueLines;
    }
}
