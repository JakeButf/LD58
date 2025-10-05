using System.Collections.Generic;
using UnityEngine;

public class ArtPuzzleManager : MonoBehaviour
{
    public ArtPiece currentSelectedPiece;

    public ArtAltar[] altars;
    public GameObject vase;
    public GameObject bottle;
    public GameObject crystal;
    public DialogueLine[] completePuzzle;

    public Material blue;
    public Material purple;
    public Material yellow;

    public GameObject prePainter;
    public GameObject postPainter;

    public GameObject wall;
    public GameObject door;

    public void UpdateSelectedPiece(ArtPiece piece)
    {
        currentSelectedPiece = piece;
    }

    public void SwitchPieces(ArtPiece interacted, ArtPiece switchTo)
    {
        ArtAltar start = null;
        ArtAltar switcher = null;
        foreach (ArtAltar a in altars)
        {
            if (a.currentPiece == interacted)
            {
                start = a;
            }

            if (a.currentPiece == switchTo)
            {
                switcher = a;
            }
        }
        if (start != null && switcher != null)
        {
            GameObject c = GetPieceFromEnum(start.currentPiece);
            GameObject s = GetPieceFromEnum(switcher.currentPiece);

            c.transform.position = switcher.artTransform.position;
            s.transform.position = start.artTransform.position;

            ArtPiece t = start.currentPiece;
            ArtColor co = start.color;          

            start.currentPiece = switcher.currentPiece;
            switcher.currentPiece = t;

            start.color = switcher.color;
            switcher.color = co;

        }

        



    }

    public GameObject GetPieceFromEnum(ArtPiece a)
    {
        if (a == ArtPiece.VASE)
            return vase;
        if (a == ArtPiece.CRYSTAL)
            return crystal;
        if (a == ArtPiece.BOTTLE)
            return bottle;
        return null;
    }

    public void SwitchColor(ArtAltar altar, ArtColor color)
    {
        GameObject g = GetPieceFromEnum(altar.currentPiece);
        altar.color = color;
        g.GetComponent<MeshRenderer>().material = GetMaterialFromColor(color);
    }

    public ArtColor GetColorFromMaterial(Material mat)
    {
        if (mat.name == "Orange") return ArtColor.YELLOW;
        if (mat.name == "Blue") return ArtColor.BLUE;
        if (mat.name == "Purple") return ArtColor.PINK;
        return ArtColor.BLUE;
    }

    public ArtColor GetArtColorFromString(string s)
    {
        if (s == "YELLOW") return ArtColor.YELLOW;
        if (s == "BLUE") return ArtColor.BLUE;
        if (s == "PINK") return ArtColor.PINK;
        return ArtColor.BLUE;
    }

    public Material GetMaterialFromColor(ArtColor color)
    {
        if (color == ArtColor.BLUE) return blue;
        if (color == ArtColor.PINK) return purple;
        if (color == ArtColor.YELLOW) return yellow;
        return blue;
    }

    public ArtPiece getPieceFromString(string s)
    {
        if (s.ToLower() == "vase")
            return ArtPiece.VASE;
        if (s.ToLower() == "crystal")
            return ArtPiece.CRYSTAL;
        if (s.ToLower() == "bottle")
            return ArtPiece.BOTTLE;

        return ArtPiece.VASE;
    }

    public List<ArtPiece> GetAllBut(ArtPiece a)
    {
        List<ArtPiece> returnList = new List<ArtPiece>();

        if (a == ArtPiece.VASE)
        {
            returnList.Add(ArtPiece.BOTTLE);
            returnList.Add(ArtPiece.CRYSTAL);
        }

        if (a == ArtPiece.BOTTLE)
        {
            returnList.Add(ArtPiece.VASE);
            returnList.Add(ArtPiece.CRYSTAL);
        }

        if (a == ArtPiece.CRYSTAL)
        {
            returnList.Add(ArtPiece.BOTTLE);
            returnList.Add(ArtPiece.VASE);
        }
        return returnList;
    }

    public List<ArtColor> GetAllBut(ArtColor c)
    {
        List<ArtColor> returnList = new List<ArtColor>();

        if (c == ArtColor.BLUE)
        {
            returnList.Add(ArtColor.PINK);
            returnList.Add(ArtColor.YELLOW);
        }

        if (c == ArtColor.PINK)
        {
            returnList.Add(ArtColor.BLUE);
            returnList.Add(ArtColor.YELLOW);
        }

        if (c == ArtColor.YELLOW)
        {
            returnList.Add(ArtColor.PINK);
            returnList.Add(ArtColor.BLUE);
        }
        return returnList;
    }

    public ArtAltar GetAltarFromPiece(ArtPiece a)
    {
        foreach (ArtAltar altar in altars)
        {
            if (altar.currentPiece == a)
                return altar;
        }
        return null;
    }


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameFlags.GetFlag("gallery_puzzle_done"))
        {
            if (prePainter.activeSelf) prePainter.SetActive(false);
            if (!postPainter.activeSelf) postPainter.SetActive(true);
            if (wall.activeSelf) wall.SetActive(false);
            if (!door.activeSelf) door.SetActive(true);
            return;
        }
        if (altars[0].currentPiece == ArtPiece.VASE && altars[1].currentPiece == ArtPiece.BOTTLE && altars[2].currentPiece == ArtPiece.CRYSTAL)
            {
                if (altars[0].color == ArtColor.YELLOW && altars[1].color == ArtColor.BLUE && altars[2].color == ArtColor.PINK)
                {
                    GameFlags.SetFlag("gallery_puzzle_done", true);
                    DialogueManager.Instance.StartDialogue(completePuzzle);
                }
            }
    }
}
