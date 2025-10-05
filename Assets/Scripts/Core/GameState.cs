using System.Collections;
using System.Xml;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
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

    public void LoadScene(string scene, Vector3 playerPos = new Vector3(), AudioClip clip = null, float volume = 1f)
    {
        this.scene = scene;
        if (clip != null && audioSource != null)
        {
            audioSource.PlayOneShot(clip, volume);
        }
        StartCoroutine(LoadScene(playerPos));
    }

    IEnumerator LoadScene(Vector3 playerPos)
    {

        anim.SetBool("Fade", true);
        yield return new WaitUntil(() => black.color.a == 1);
        SceneManager.LoadScene(scene);
        anim.SetBool("Fade", false);
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null && playerPos != Vector3.zero)
        {
            player.transform.position = playerPos;
        }
        GameFlags.SetFlag("in_maritime_room", false);
        GameFlags.SetFlag("in_orchestra_room", false);
        GameFlags.SetFlag("in_art_room", false);
        if (scene == "MaritimeRoom")
        {
            GameFlags.SetFlag("in_maritime_room", true);
        }
        if (scene == "OrchestraRoom")
        {
            GameFlags.SetFlag("in_orchestra_room", true);
        }
        if (scene == "ArtRoom")
        {
            GameFlags.SetFlag("in_art_room", true);
        }
        if (scene == "GrandHall")
        {
            GameFlags.SetFlag("second_room_entered", true);
        }

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
