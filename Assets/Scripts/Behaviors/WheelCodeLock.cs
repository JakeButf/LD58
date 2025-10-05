using TMPro;
using UnityEngine;

public class WheelCodeLock : MonoBehaviour
{
    [SerializeField] TMP_Text button1;
    [SerializeField] TMP_Text button2;
    [SerializeField] TMP_Text button3;
    [SerializeField] static string[] completedDialog = { "You hear a clicking noise." };
    [SerializeField] private AudioSource clickSfx;

    public void CheckCode()
    {
        if (button1.text == "2" && button2.text == "3" && button3.text == "1")
        {
            GameFlags.SetFlag("maritime_chest_unlocked", true);
            GameFlags.SetFlag("player_can_move", true);
            DialogueManager.Instance.StartDialogue(completedDialog);
            clickSfx.Play();

            this.gameObject.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(PlayerInput.Escape))
        {
            GameFlags.SetFlag("player_can_move", true);
            this.gameObject.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
