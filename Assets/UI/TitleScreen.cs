using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleScreen : MonoBehaviour
{
    [SerializeField] private PlayableDirector director;
    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private GameObject titleMenu;
    [SerializeField] private string nextSceneName = "Entrance"; 
    public Image black;
    public Animator anim;

    void Start()
    {
        // Register the event callback for when the timeline finishes
        director.stopped += OnTimelineFinished;
    }

    private void OnDestroy()
    {
        // Always good practice to clean up event subscriptions
        director.stopped -= OnTimelineFinished;
    }

    public void PlayTimeline()
    {
        director.Play();
    }

    private void OnTimelineFinished(PlayableDirector obj)
    {
        // Make sure the event is coming from our own director (not another one)
        if (obj == director)
        {
            StartCoroutine(LoadScene());
        }
    }

    private IEnumerator LoadScene()
    {

        anim.SetBool("Fade", true);
        yield return new WaitUntil(() => black.color.a == 1);
        SceneManager.LoadScene(nextSceneName);
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

    public void ToggleFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
}
