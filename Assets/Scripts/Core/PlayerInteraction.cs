using UnityEngine;
using TMPro; // for UI prompt text

public class PlayerInteractor : MonoBehaviour
{
    [SerializeField] private float interactionRange = 2f;
    [SerializeField] private LayerMask interactableMask;
    [SerializeField] private TMP_Text promptText; // UI element in canvas

    private Camera cam;

    void Start()
    {
        cam = GetComponentInChildren<Camera>();
        if (promptText != null)
            promptText.gameObject.SetActive(false); // start hidden
    }

    void Update()
    {
        HandleInteractionCheck();
    }

    private void HandleInteractionCheck()
    {
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, interactionRange, interactableMask))
        {
            Interactable interactable = hit.collider.GetComponent<Interactable>();
            if (interactable != null && !DialogueManager.Instance.IsDialogueActive())
            {
                ShowPrompt(interactable.GetPromptMessage());

                if (Input.GetKeyDown(PlayerInput.Interact))
                {
                    HidePrompt();
                    interactable.Interact();
                }
                return; // don’t hide prompt if we’re still looking at something
            }
        }

        HidePrompt();
    }

    private void ShowPrompt(string message)
    {
        if (promptText != null)
        {
            promptText.gameObject.SetActive(true);
            promptText.text = message;
        }
    }

    private void HidePrompt()
    {
        if (promptText != null)
            promptText.gameObject.SetActive(false);
    }
}
