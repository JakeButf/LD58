using UnityEngine;

public class StartEnd : Interactable
{
    [SerializeField] Animation anim;
    [SerializeField] Camera cutsceneCam;
    [SerializeField] Animation camAnim;
    [SerializeField] StartEnd other;
    [SerializeField] AudioSource bell;

    public override void Interact()
    {
        anim.Play();
        
        GameFlags.SetFlag("player_can_move", false);
        
    }

    public void StartOther()
    {
        other.SwitchCamera();
        bell.Play();
    }

    public void SwitchCamera()
    {
        Camera mainCam = Camera.main;
        if (mainCam != null)
            mainCam.gameObject.SetActive(false);

        cutsceneCam.gameObject.SetActive(true);
        camAnim.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
