using Unity.VisualScripting;
using UnityEngine;

public class CrystalPuzzle : MonoBehaviour
{
    [SerializeField] CrystalOrchestra piano;
    [SerializeField] CrystalOrchestra strings;
    [SerializeField] CrystalOrchestra bass;
    [SerializeField] CrystalOrchestra organ;
    [SerializeField] double loopLength;
    [SerializeField] float fadeInSpeed = 0.25f;
    [SerializeField] AudioClip fullPiano;
    [SerializeField] AudioClip fullStrings;
    [SerializeField] AudioClip fullBass;
    [SerializeField] AudioClip fullOrgan;
    [SerializeField] GameObject preSpectre;
    [SerializeField] GameObject postSpectre;
    [SerializeField] GameObject wall;
    [SerializeField] GameObject door;
    float rotateSpeed = 1.1f;

    double startTime;
    [SerializeField] bool started;
    bool swapped = false;
    public bool startedFull;

    void Awake()
    {
        if (GameFlags.GetFlag("orchestra_room_open"))
        {
            openWall();
        }
        startTime = AudioSettings.dspTime + 0.2f;
        PlayLoop(piano);
        PlayLoop(strings);
        PlayLoop(bass);
        PlayLoop(organ);
        piano.ChangeState(true);
        started = true;
    }

    void PlayLoop(CrystalOrchestra crystal)
    {
        AudioSource a = crystal.aud;
        a.loop = true;
        a.volume = 0f;
        a.PlayScheduled(startTime);
    }

    public void PlayFull(CrystalOrchestra c)
    {
        if (startedFull) return;
        AudioSource a = c.aud;
        a.loop = false;
        a.volume = 1f;
        a.PlayScheduled(AudioSettings.dspTime + 0.2f);
    }

    public void StartPlayFull()
    {
        PlayFull(piano);
        PlayFull(strings);
        PlayFull(organ);
        PlayFull(bass);
        GameFlags.SetFlag("orchestra_room_open", true);
        startedFull = true;
    }

    void FadeInIfActive(CrystalOrchestra crystal)
    {
        if (crystal.getActivated() && crystal.aud.volume < 1f)
        {
            crystal.aud.volume += Time.deltaTime / fadeInSpeed;
        }
        else if (!crystal.getActivated() && crystal.aud.volume > 0f)
        {
            crystal.aud.volume -= Time.deltaTime / fadeInSpeed;
        }
    }

    void StopAndSwapToFull()
    {
        if (swapped) return;
        piano.aud.Stop();
        strings.aud.Stop();
        bass.aud.Stop();
        organ.aud.Stop();
        piano.aud.clip = fullPiano;
        strings.aud.clip = fullStrings;
        bass.aud.clip = fullBass;
        organ.aud.clip = fullOrgan;
        piano.aud.loop = false;
        strings.aud.loop = false;
        bass.aud.loop = false;
        organ.aud.loop = false;
    }

    void Update()
    {
        if (!started) return;

        FadeInIfActive(piano);
        FadeInIfActive(strings);
        FadeInIfActive(bass);
        FadeInIfActive(organ);

        if (piano.linkedCrystal == strings && strings.linkedCrystal == bass && bass.linkedCrystal == organ)
        {
            GameFlags.SetFlag("orchestra_complete", true);
            preSpectre.SetActive(false);
            postSpectre.SetActive(true);
        }

        if (GameFlags.GetFlag("orchestra_complete"))
        {
            StopAndSwapToFull();
            swapped = true;
        }

        if (GameFlags.GetFlag("orchestra_room_open") && !piano.aud.isPlaying)
        {
            GameFlags.SetFlag("canleave_performancehall", true);
            FaceTarget(piano);
            FaceTarget(strings);
            FaceTarget(bass);
            FaceTarget(organ);
            openWall();
        }
    }
    void FaceTarget(CrystalOrchestra crystal)
    {
        if (door == null) return;

        Vector3 direction = door.transform.position - crystal.transform.position;
        direction.y = 0f;

        if (direction.sqrMagnitude > 0.01f)
        {
            Quaternion targetRot = Quaternion.LookRotation(direction);
            crystal.transform.rotation = Quaternion.Slerp(
                crystal.transform.rotation,
                targetRot,
                Time.deltaTime * rotateSpeed
            );
        }
    }

    void openWall()
    {
        if (wall.activeSelf) wall.SetActive(false);
        if (!door.activeSelf) door.SetActive(true);
    }

}
