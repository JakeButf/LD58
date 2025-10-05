using UnityEngine;

public class Lantern : Interactable
{
    [SerializeField] GameObject arms;

    public override void Interact()
    {
        arms.SetActive(true);
        GameFlags.SetFlag("has_lantern", true);
        GameObject.Destroy(this.gameObject);
    }

}
