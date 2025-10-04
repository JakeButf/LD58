using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueLine
{
    public string text;
    public AudioClip advanceClip;
    public AudioClip charClip;
    public bool paperEffect;

    public DialogueLine(string t, bool pe = false, AudioClip aClip = null, AudioClip cClip = null)
    {
        paperEffect = pe;
        text = t;
        advanceClip = aClip;
        charClip = cClip;
    }

}