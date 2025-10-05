using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ObjectiveManager : MonoBehaviour
{
    public static ObjectiveManager Instance;
    [SerializeField] private TMP_Text objectiveText;
    [SerializeField] private List<Objective> objectives = new List<Objective>();

    private int currentObjectiveIndex = 0;

    private void OnEnable() => GameFlags.OnFlagsUpdated += CheckObjectiveProgress;
    private void OnDisable() => GameFlags.OnFlagsUpdated -= CheckObjectiveProgress;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

            objectives = new List<Objective>
        {
            new Objective("open_door", "Find a way to open the door.", new List<string> { "floor1_bell_complete" }),
            new Objective("go_upstairs", "Get to the next floor.", new List<string> { "second_room_entered" })
        };
        }
        else Destroy(gameObject);
        
        DontDestroyOnLoad(gameObject);
        Objective.ResetFlagChain(); // start clean
        UpdateObjectiveText();
    }


    //check if we completed the current objective whenever a flag is updated
    private void CheckObjectiveProgress()
    {
        if (currentObjectiveIndex >= objectives.Count)
            return;

        var current = objectives[currentObjectiveIndex];

        bool allFlagsTrue = true;
        foreach (var flag in current.requiredFlagsToComplete)
        {
            if (!GameFlags.GetFlag(flag))
            {
                allFlagsTrue = false;
                break;
            }
        }

        if (allFlagsTrue)
        {
            AdvanceObjective();
        }
    }

    //move to the next objective
    private void AdvanceObjective()

    {
        currentObjectiveIndex++;
        UpdateObjectiveText();
    }

    private void UpdateObjectiveText()
    {
        if (currentObjectiveIndex < objectives.Count)
        {
            objectiveText.text = objectives[currentObjectiveIndex].description;
        }
        else
        {
            objectiveText.text = "All objectives completed!";
        }
    }
}

