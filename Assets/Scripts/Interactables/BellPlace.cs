using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class BellPlace : Interactable
{
    [SerializeField] private GameObject bellModel;
    private bool bellToPlace = false;

    private void OnEnable() => GameState.OnBellCountChanged += ChangeState;
    private void OnDisable() => GameState.OnBellCountChanged -= ChangeState;

    public override void Interact()
    {
        if (bellToPlace)
        {
            bellModel.SetActive(true);
            GameState.Instance.RemoveBell();
            GameObject.Destroy(this);
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

}
