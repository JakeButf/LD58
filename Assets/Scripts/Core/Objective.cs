using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Objective
{
    public string objectiveID;
    public string description;
    public List<string> requiredFlags;



    public Objective(string id, string desc, List<string> newFlags)
    {
        objectiveID = id;
        description = desc;
        requiredFlags = newFlags;
    }
}
