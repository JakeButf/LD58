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
    public int bellInventory = 0;

    public static System.Action OnBellCountChanged;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
        DisplayBells();
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

    public void AddBell()
    {
        bellInventory++;
        OnBellCountChanged?.Invoke();
        DisplayBells();
    }

    public void RemoveBell()
    {
        bellInventory--;
        OnBellCountChanged?.Invoke();
        DisplayBells();
    }
    
    // display as many bells as in inventory
    public void DisplayBells()
    {
        for (int i = 0; i < 3; i++)
        {
            if (i < bellInventory) GameObject.Find("Bell" + (i + 1)).GetComponent<Image>().enabled = true;
            else GameObject.Find("Bell" + (i + 1)).GetComponent<Image>().enabled = false;
        }
    }
}
