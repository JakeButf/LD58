using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameState : MonoBehaviour
{
    public static GameState Instance;

    public Image black;
    public Animator anim;
    public string scene;
    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        DontDestroyOnLoad(gameObject); 
        PlayerInput.Initialize();
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    
    public void LoadScene(string scene)
    {
        this.scene = scene;
        StartCoroutine(FadeAnimation());
    }

    IEnumerator FadeAnimation()
    {
        anim.SetBool("Fade", true);
        yield return new WaitUntil(() => black.color.a == 1);
        SceneManager.LoadScene(scene);
        anim.SetBool("Fade", false);
    }
}
