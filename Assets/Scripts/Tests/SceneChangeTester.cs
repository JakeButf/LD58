using UnityEngine;

public class SceneChangeTester : MonoBehaviour
{
    void Update()
    {
        // Press "T" to start a test dialogue
        if (Input.GetKeyDown(KeyCode.Semicolon))
        {
           GameState.Instance.LoadScene("Scene2");
        }
    }
}
  