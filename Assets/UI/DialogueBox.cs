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

    private AudioClip advanceClip;
    private AudioClip charClip;

    [SerializeField][Range(0f, 1f)] private float advanceVolume = 1f;
    [SerializeField][Range(0f, 1f)] private float charVolume = 0.6f;
    [SerializeField] private float charPitchVariance = 0.05f;
    [SerializeField] private int charSfxEveryN = 2;
    private AudioSource audioSource;

    private Queue<string> sentences;
    private Coroutine typingCoroutine;
    private bool isTyping;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        DontDestroyOnLoad(gameObject); // persist across scenes if needed
        sentences = new Queue<string>();
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

    // Overload to pass optional SFX per-dialogue
    public void StartDialogue(IEnumerable<string> dialogueLines, bool paperEffect = false, AudioClip advanceSfx = null, AudioClip charSfx = null)
    {
        // If clips provided, override the inspector defaults for this dialogue (for this dialogue session)
        if (advanceSfx != null) advanceClip = advanceSfx;
        if (charSfx != null) charClip = charSfx;

        Image image = dialoguePanel.GetComponent<Image>();
        if (image != null)
        {
            Color c = image.color;
            c.a = paperEffect ? 1f : 0f;
            image.color = c;
        }

        sentences.Clear();

        foreach (string line in dialogueLines)
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
            dialogueText.text = sentences.Dequeue();
            isTyping = false;
            return;
        }

        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Peek();
        // Play advance SFX whenever DisplayNextSentence is invoked (can be skip or next)
        if (advanceClip != null && audioSource != null)
        {
            audioSource.PlayOneShot(advanceClip, advanceVolume);
        }
        typingCoroutine = StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        isTyping = true;
        dialogueText.text = "";

        int visibleCount = 0; // count of visible (non-whitespace, non-tag) chars

        // sanity clamp
        if (charSfxEveryN < 1) charSfxEveryN = 1;
        //little pause before starting to type
        yield return new WaitForSeconds(.3f);
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;

            if (!char.IsWhiteSpace(letter))
            {
                visibleCount++;
                if (charClip != null && audioSource != null && (visibleCount % charSfxEveryN) == 0)
                {
                    float originalPitch = audioSource.pitch;
                    audioSource.pitch = 1f + Random.Range(-charPitchVariance, charPitchVariance);
                    audioSource.PlayOneShot(charClip, charVolume);
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
}
