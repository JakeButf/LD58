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
            new Objective("enter_building", "Enter the bell tower.", new List<string> { }),
            //level 1
            new Objective("level1", "Investigate the area.", new List<string> {"entered_building"}),
            new Objective("open_door", "Find a way to open the door.", new List<string> {"entered_building", "tried_door" }),
            new Objective("go_upstairs", "Get to the next floor.", new List<string> { "floor1_bell_complete" }),
            //grand hall
            new Objective("explore", "Explore the area...", new List<string> { "floor1_bell_complete", "second_room_entered" }),
            //maritime
            new Objective("maritime", "Investigate the Treasure Map.", new List<string> { "floor1_bell_complete", "second_room_entered", "in_maritime_room" }),
            new Objective("unlock_chest", "Discover new lands.", new List<string> { "floor1_bell_complete", "second_room_entered", "in_maritime_room", "maritime_chest_unlocked"}),
            new Objective("get_bell", "Grab the Maritime Bell.", new List<string> { "floor1_bell_complete", "second_room_entered", "in_maritime_room", "maritime_chest_unlocked", "maritime_puzzle_solved" }),
            new Objective("maritime_done", "Investigate the rest of the tower.", new List<string> { "floor1_bell_complete", "second_room_entered", "in_maritime_room", "maritime_bell_complete" }),
            //orchestra
            new Objective("orchestra", "Investigate the crystal orchestra.", new List<string> { "floor1_bell_complete", "second_room_entered", "in_orchestra_room" }),
            new Objective("orchestra_puzzle_complete", "Speak with the conductor.", new List<string> { "floor1_bell_complete", "second_room_entered", "in_orchestra_room", "orchestra_complete" }),
            new Objective("orchestra_music_playing", "Listen to the performance.", new List<string> { "floor1_bell_complete", "second_room_entered", "in_orchestra_room", "orchestra_complete", "orchestra_room_open" }),
            new Objective("orchestra_get_bell", "Follow the light.", new List<string> { "floor1_bell_complete", "second_room_entered", "in_orchestra_room", "orchestra_complete", "orchestra_room_open", "canleave_performancehall" }),
            new Objective("orchestra_done", "Investigate the rest of the tower.", new List<string> { "floor1_bell_complete", "second_room_entered", "in_orchestra_room", "orchestra_bell_complete" }),
            //painting
            new Objective("gallery", "Investigate the strange artwork.", new List<string> { "floor1_bell_complete", "second_room_entered", "in_gallery_room" }),
            new Objective("gallery_get_bell", "Find the matching room.", new List<string> { "floor1_bell_complete", "second_room_entered", "in_gallery_room", "gallery_puzzle_done", }),
            new Objective("gallery_done", "Investigate the rest of the tower.", new List<string> { "floor1_bell_complete", "second_room_entered", "in_gallery_room", "gallery_bell_complete" }),
            //all bells got
            new Objective("all_bells", "Return to the mysterious door.", new List<string> { "floor1_bell_complete", "maritime_bell_complete", "orchestra_bell_complete", "gallery_bell_complete" }),
            //all bells placed 
            new Objective("bell_tower", "Ascend to the tower.", new List<string> { "floor1_bell_complete", "placed_bell1", "placed_bell2", "placed_bell3" }),
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

