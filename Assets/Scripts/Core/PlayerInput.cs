using UnityEngine;

public static class PlayerInput
{
    public static KeyCode MoveUp { get; private set; }
    public static KeyCode MoveDown { get; private set; }
    public static KeyCode MoveLeft { get; private set; }
    public static KeyCode MoveRight { get; private set; }
    public static KeyCode Jump { get; private set; }
    public static KeyCode Interact { get; private set; }
    public static float MouseSensitivity { get; private set; } 

    public static void Initialize()
    {
        MoveUp = KeyCode.W;
        MoveDown = KeyCode.S;
        MoveLeft = KeyCode.A;
        MoveRight = KeyCode.D;
        Jump = KeyCode.Space;
        Interact = KeyCode.E;
        MouseSensitivity = 1f; 

    }
}
