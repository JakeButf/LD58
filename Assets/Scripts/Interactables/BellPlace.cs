using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class BellPlace : Interactable
{
    [SerializeField] private GameObject bellModel;
    private bool bellToPlace = false;

    [SerializeField] private string flagForPlacing;

    private void OnEnable() => GameState.OnBellCountChanged += ChangeState;
    private void OnDisable() => GameState.OnBellCountChanged -= ChangeState;

    public void Awake()
    {
        if (GameFlags.GetFlag(flagForPlacing))
        {
            PlaceBell();
        }
        ChangeState();
    }


    public override void Interact()
    {
        if (bellToPlace)
        {
            PlaceBell();
            GameState.Instance.RemoveBell();
            GameFlags.SetFlag(flagForPlacing, true);
            if (GameFlags.GetFlag("placed_bell1") && GameFlags.GetFlag("placed_bell2") && GameFlags.GetFlag("placed_bell3"))
            {
                GameFlags.SetFlag("all_bells_placed", true);
            }
        }
        else
        {
            DialogueManager.Instance.StartDialogue(new string[] { "It looks like something should go here." }, false);
        }

    }

    public void ChangeState()
    {
        if (GameState.Instance.bellInventory > 0)
        {
            bellToPlace = true;
            promptMessage = "Place Bell";
        }
        else
        {
            bellToPlace = false;
            promptMessage = "Inspect";
        }
    }

    public void PlaceBell()
    {
        bellModel.SetActive(true);
        GameObject.Destroy(this);
    }
}
