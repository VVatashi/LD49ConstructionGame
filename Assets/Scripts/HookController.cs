using UnityEngine;

public class HookController : MonoBehaviour
{
    public CraneController CraneController;

    private void OnTriggerEnter2D(Collider2D other)
    {
        CraneController.Cargo = other.attachedRigidbody;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        CraneController.Cargo = null;
    }
}
