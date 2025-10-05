using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class PaintUI : MonoBehaviour
{
    [SerializeField] ArtPuzzleManager puzzle;
    [SerializeField] TMP_Text buttonOne;
    [SerializeField] TMP_Text buttonTwo;

    public void ButtonOneOnClick()
    {
        puzzle.SwitchColor(puzzle.GetAltarFromPiece(puzzle.currentSelectedPiece), puzzle.GetArtColorFromString(buttonOne.text));
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        GameFlags.SetFlag("player_can_move", true);
        this.gameObject.SetActive(false);
    }

    public void ButtonTwoOnClick()
    {
        puzzle.SwitchColor(puzzle.GetAltarFromPiece(puzzle.currentSelectedPiece), puzzle.GetArtColorFromString(buttonTwo.text));
        this.gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        GameFlags.SetFlag("player_can_move", true);
    }

    void OnEnable()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        GameFlags.SetFlag("player_can_move", false);
        List<ArtColor> colors = puzzle.GetAllBut(puzzle.GetAltarFromPiece(puzzle.currentSelectedPiece).color);
        buttonOne.text = colors[0].ToString();
        buttonTwo.text = colors[1].ToString();
    } // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.activeSelf)
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            GameFlags.SetFlag("player_can_move", false);
        }
    }
}
