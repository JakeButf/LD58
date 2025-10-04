using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Objective
{
    public string objectiveID;
    public string description;
    public List<string> additionalFlags;
    public List<string> requiredFlagsToComplete;

    // 🔒 Static accumulator shared across all Objective instances
    private static List<string> accumulatedFlags = new List<string>();

    public Objective(string id, string desc, List<string> newFlags)
    {
        objectiveID = id;
        description = desc;
        additionalFlags = newFlags;

        // Inherit all previous flags
        requiredFlagsToComplete = new List<string>(accumulatedFlags);
        requiredFlagsToComplete.AddRange(newFlags);

        // Add these to the global tracker for future objectives
        accumulatedFlags.AddRange(newFlags);
    }

    // Optional helper to reset between sessions or tests
    public static void ResetFlagChain()
    {
        accumulatedFlags.Clear();
    }
}
