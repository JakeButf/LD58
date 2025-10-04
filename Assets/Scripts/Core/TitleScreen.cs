using UnityEditor;
using UnityEngine;
using UnityEngine.Playables;

public class TitleScreen : MonoBehaviour
{
    [SerializeField] private PlayableDirector director;
    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private GameObject titleMenu;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayTimeline()
    {
        director.Play();
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
            Application.Quit(); 
#endif
    }

    public void GoToSettings()
    {
        settingsMenu.SetActive(true);
        titleMenu.SetActive(false);
    }

    public void GoToTitle()
    {
        settingsMenu.SetActive(false);
        titleMenu.SetActive(true);
    }

    public void toggleFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
}
