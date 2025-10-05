using System.Collections.Generic;
using UnityEngine;

public class BellPuzzleInteractable : Interactable
{
    [SerializeField] int bellID;
    [SerializeField] static string[] completedDialog = {"You hear a clicking noise."};
    private AudioSource audio;
    [SerializeField] private AudioSource clickSfx;
    static int[] reference = { 2, 3, 2, 3, 2, 1 };
    static List<int> tollOrder = new List<int>();

    void Awake()
    {
        audio = GetComponent<AudioSource>();
    }
    public override void Interact()
    {

        audio.Play();
        if (GameFlags.GetFlag("floor1_bell_complete")) return;
        if (reference[tollOrder.Count] == this.bellID)
        {
            tollOrder.Add(this.bellID);
        }
        if (reference.Length == tollOrder.Count)
        {
            GameFlags.SetFlag("floor1_bell_complete", true);
            DialogueManager.Instance.StartDialogue(completedDialog);
            clickSfx.Play();
        }
    }
    
}
