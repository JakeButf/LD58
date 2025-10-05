using System.Collections.Generic;
using UnityEngine;

public static class GameFlags
{

    public static System.Action OnFlagsUpdated;

    private static Dictionary<string, bool> flags = new Dictionary<string, bool>()
    {
        {"has_lantern", false},
        {"entered_building", false},
        {"tried_door", false},
        { "floor1_bell_complete", false},
        {"second_room_entered", false},
        {"player_can_move", true},
        {"in_maritime_room", false},
        {"maritime_chest_unlocked", false},
        {"maritime_puzzle_solved", false},
        {"maritime_bell_complete", false},
        { "orchestra_complete", false},
        {"orchestra_open", false},
        {"orchestra_room_open", false},
        {"in_orchestra_room", false},
        {"in_art_room", false},
        {"canleave_performancehall",true },
        {"orchestra_bell_complete", false},
        { "art_puzzle_done", false}
    };

    public static bool GetFlag(string flagName)
    {
        if (flags.ContainsKey(flagName))
        {
            return flags[flagName];
        }
        else
        {
            Debug.LogWarning($"Flag '{flagName}' does not exist. Returning false by default.");
            return false;
        }
    }

    public static void SetFlag(string flagName, bool value)
    {
        if (flags.ContainsKey(flagName))
        {
            flags[flagName] = value;
        }
        else
        {
            Debug.LogWarning($"Flag '{flagName}' does not exist. Creating it with value {value}.");
            flags[flagName] = value;
        }

        OnFlagsUpdated?.Invoke();
    }

}
