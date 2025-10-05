using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ObjectiveManager : MonoBehaviour
{
    public static ObjectiveManager Instance;
    [SerializeField] private TMP_Text objectiveText;
    [SerializeField] private List<Objective> objectives = new List<Objective>();

    private Objective currentObjective;

    private void OnEnable() => GameFlags.OnFlagsUpdated += UpdateObjective;
    private void OnDisable() => GameFlags.OnFlagsUpdated -= UpdateObjective;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

            objectives = new List<Objective>
        {
            //entrance
            new Objective("enter_building", "Enter the mysterious place.", new List<string> { }),
            //level 1
            new Objective("level1", "Investigate the area.", new List<string> {"entered_building"}),
            new Objective("open_door", "Find a way to open the door.", new List<string> {"entered_building", "tried_door" }),
            new Objective("go_upstairs", "Get to the next floor.", new List<string> { "floor1_bell_complete" }),
            new Objective("explore", "Explore the area...", new List<string> { "floor1_bell_complete", "second_room_entered" }),
            //maritime
            new Objective("maritime", "Look for treasure.", new List<string> { "floor1_bell_complete", "second_room_entered", "in_maritime_room" }),
            new Objective("unlock_chest", "Discover new lands.", new List<string> { "floor1_bell_complete", "second_room_entered", "in_maritime_room", "maritime_chest_unlocked"}),
            new Objective("get_bell", "Grab the treasure.", new List<string> { "floor1_bell_complete", "second_room_entered", "in_maritime_room", "maritime_chest_unlocked", "maritime_puzzle_solved" }),
            new Objective("maritime_done", "Look for some more bells.", new List<string> { "floor1_bell_complete", "second_room_entered", "in_maritime_room", "maritime_bell_complete" }),
            //orchestra
            new Objective("orchestra", "Investigate the peculiar orchestra.", new List<string> { "floor1_bell_complete", "second_room_entered", "in_orchestra_room" }),
            new Objective("orchestra_puzzle_complete", "Speak with the conductor.", new List<string> { "floor1_bell_complete", "second_room_entered", "in_orchestra_room", "orchestra_complete" }),
            new Objective("orchestra_music_playing", "Enjoy the music.", new List<string> { "floor1_bell_complete", "second_room_entered", "in_orchestra_room", "orchestra_complete", "orchestra_room_open" }),
            new Objective("orchestra_get_bell", "Follow the light.", new List<string> { "floor1_bell_complete", "second_room_entered", "in_orchestra_room", "orchestra_complete", "orchestra_room_open", "canleave_performancehall" }),
            new Objective("orchestra_done", "Look for some more bells.", new List<string> { "floor1_bell_complete", "second_room_entered", "in_orchestra_room", "orchestra_bell_complete" }),
            //painting
            

            //grand hall

            };
            //check from last to first to find the furthest possible objective
            objectives.Reverse();
        }
        else Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
        UpdateObjective();
    }

    void Update()
    {
        if (objectiveText == null)
        {
            objectiveText = GameObject.Find("ObjectiveText").GetComponent<TMP_Text>();
            UpdateObjective();
        }
    }

    //check if we completed the current objective whenever a flag is updated
    public void UpdateObjective()
    {
        foreach (var obj in objectives)
        {
            bool allFlagsTrue = true;

            foreach (var flag in obj.requiredFlags)
            {
                if (!GameFlags.GetFlag(flag))
                {
                    allFlagsTrue = false;
                    break;
                }
            }

            if (allFlagsTrue)
            {
                currentObjective = obj;
                objectiveText.text = obj.description;
                return;
            }
        }
    }
}

