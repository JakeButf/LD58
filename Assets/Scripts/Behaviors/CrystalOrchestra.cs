using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public enum CrystalInstruments
{
    Piano,
    Strings,
    Bass,
    Organ
}

public class CrystalOrchestra : Interactable
{
    [SerializeField] LineRenderer beam;
    [SerializeField] Transform beamOrigin;
    [SerializeField] float beamMaxDistance = 50f;

    [SerializeField] CrystalInstruments instrument;
    [SerializeField] private bool activated;
    [SerializeField] LayerMask crystalMask;
    [SerializeField] GameObject spotLight;
    [SerializeField] public AudioSource aud;
    [SerializeField] float defaultVolume;
    [SerializeField] float desiredRotation = 0;
    public bool alwaysActive = false;
    float rotationSpeed = 45f;
    public float currentY = 0f;
    public CrystalOrchestra linkedCrystal;

    void Start()
    {
        if (beam != null)
        {
            beam.positionCount = 2;
            beam.enabled = false;
        }
    }

    void Update()
    {
        Vector3 dir = transform.forward;
        if (beam != null)
        {
            beam.enabled = true;
            beam.SetPosition(0, beamOrigin.position);
            beam.SetPosition(1, beamOrigin.position + dir * 50f);

            // Match color to spotlight (if it has a Light component)
            Light l = spotLight.GetComponent<Light>();
            if (l != null)
            {
                beam.startColor = l.color;
                beam.endColor = l.color * 0.6f; // fade a bit toward the end
            }
        }
        if (GameFlags.GetFlag("orchestra_complete")) return;
        if (!activated && spotLight.activeSelf) { spotLight.SetActive(false); }
        if (activated && !spotLight.activeSelf) { spotLight.SetActive(true); }
        currentY = transform.eulerAngles.y;
        float newY = Mathf.MoveTowardsAngle(currentY, desiredRotation, rotationSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(-2f, newY, 0f);


        if (linkedCrystal != null && !linkedCrystal.alwaysActive) linkedCrystal.ChangeState(false);
        linkedCrystal = null;
        if (!activated)
        {
            if (beam != null) beam.enabled = false;
            return;
        }
        if (Physics.Raycast(transform.position, dir, out RaycastHit hit, 50f, crystalMask))
        {
            CrystalOrchestra other = hit.collider.GetComponent<CrystalOrchestra>();
            if (other != null)
            {
                linkedCrystal = other;
                linkedCrystal.ChangeState(true);
                return;
            }
        }


    }

    public override void Interact()
    {
        if (GameFlags.GetFlag("orchestra_complete")) return;
        desiredRotation = (desiredRotation + 45f);
    }
    void OnDrawGizmos()
    {
        // visualize the -X ray in editor
        Gizmos.color = activated ? Color.cyan : Color.red;
        Gizmos.DrawRay(transform.position, transform.forward * 50f);
    }


    public void ChangeState(bool activated)
    {
        this.activated = activated;
        if (activated)
        {
            this.promptMessage = "(E) to rotate " + instrument.ToString();
        }
        else
        {
            this.promptMessage = "(E) to rotate";
        }
    }

    public bool getActivated()
    {
        return activated;
    }


}
