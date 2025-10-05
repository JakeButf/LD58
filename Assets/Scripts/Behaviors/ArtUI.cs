using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ArtUI : MonoBehaviour
{
    [SerializeField] ArtPuzzleManager puzzle;
    [SerializeField] TMP_Text buttonOne;
    [SerializeField] TMP_Text buttonTwo;

    public void ButtonOneOnClick()
    {
        puzzle.SwitchPieces(puzzle.currentSelectedPiece, puzzle.getPieceFromString(buttonOne.text));
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        GameFlags.SetFlag("player_can_move", true);
        this.gameObject.SetActive(false);
    }

    public void ButtonTwoOnClick()
    {
        puzzle.SwitchPieces(puzzle.currentSelectedPiece, puzzle.getPieceFromString(buttonTwo.text));
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        GameFlags.SetFlag("player_can_move", true);
        this.gameObject.SetActive(false);
    }

    void OnEnable()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        GameFlags.SetFlag("player_can_move", false);
        List<ArtPiece> pieces = puzzle.GetAllBut(puzzle.currentSelectedPiece);
        buttonOne.text = pieces[0].ToString();
        buttonTwo.text = pieces[1].ToString();
    }

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
