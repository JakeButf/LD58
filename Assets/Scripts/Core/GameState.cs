using System.Collections;
using System.Xml;
using Unity.VisualScripting;
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
    private AudioSource audioSource;


    public static System.Action OnBellCountChanged;
    private Vector3 playerPos;
    private Vector3 playerRot;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
        audioSource = GetComponent<AudioSource>();
        DisplayBells();
        PlayerInput.Initialize();
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (anim == null)
        {
            anim = GameObject.Find("BlackImage").GetComponent<Animator>();
        }
        if (black == null)
        {
            black = GameObject.Find("BlackImage").GetComponent<Image>();
        }
    }

    public void LoadScene(string scene, Vector3 playerPos = new Vector3(), Vector3 playerRot = new Vector3(), AudioClip clip = null, float volume = 1f)
    {
        this.scene = scene;
        if (clip != null && audioSource != null)
        {
            audioSource.PlayOneShot(clip, volume);
        }
        StartCoroutine(LoadScene(playerPos, playerRot));
    }

    IEnumerator LoadScene(Vector3 playerPos, Vector3 playerRot)
    {

        anim.SetBool("Fade", true);
        yield return new WaitUntil(() => black.color.a == 1);
        SceneManager.LoadScene(scene);
        this.playerPos = playerPos;
        this.playerRot = playerRot;
        SceneManager.sceneLoaded += OnSceneLoaded;
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
        Debug.Log("Displaying " + bellInventory + " bells.");
        for (int i = 0; i < 3; i++)
        {
            if (i < bellInventory) GameObject.Find("Bell" + (i + 1)).GetComponent<Image>().enabled = true;
            else GameObject.Find("Bell" + (i + 1)).GetComponent<Image>().enabled = false;
        }
    }

    public void SetSceneState()
    {
        GameFlags.SetFlag("in_maritime_room", false);
        GameFlags.SetFlag("in_orchestra_room", false);
        GameFlags.SetFlag("in_gallery_room", false);
        if (scene == "MaritimeRoom")
        {
            GameFlags.SetFlag("in_maritime_room", true);
        }
        if (scene == "PerformanceHall")
        {
            GameFlags.SetFlag("in_orchestra_room", true);
        }
        if (scene == "Gallery")
        {
            GameFlags.SetFlag("in_gallery_room", true);
        }
        if (scene == "GrandHall")
        {
            GameFlags.SetFlag("second_room_entered", true);
        }
        if (scene == "LevelOne")
        {
            GameFlags.SetFlag("entered_building", true);
        }
        DisplayBells();
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        StartCoroutine(HandleSceneLoad(scene));
    }

    IEnumerator HandleSceneLoad(Scene scene)
    {
        yield return null; // wait one frame
        anim = GameObject.Find("BlackImage")?.GetComponent<Animator>();
        black = GameObject.Find("BlackImage")?.GetComponent<Image>();
        GameObject player = GameObject.Find("Player");

        if (player != null && playerPos != Vector3.zero)
            player.transform.position = playerPos;
        if (player != null && playerRot != Vector3.zero)
            player.transform.rotation = Quaternion.Euler(playerRot);

        anim?.SetBool("Fade", false);
        SetSceneState();
    }
}
