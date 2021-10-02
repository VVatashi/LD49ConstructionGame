using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class CraneController : MonoBehaviour
{
    public PlayerInput PlayerInput;
    public Rigidbody2D Carrier;
    public Rigidbody2D Hook;
    public DistanceJoint2D HookJoint;
    public LineRenderer Rope;
    public Rigidbody2D Cargo;
    public AudioSource CarrierSound;
    public AudioSource GrabSound;

    public float CarrierSpeed = 1;
    public float CarrierXMin = 0;
    public float CarrierXMax = 0;

    public float HookSpeed = 1;
    public float HookDistanceMin = 0;
    public float HookDistanceMax = 0;

    private InputAction MoveInputAction;

    private bool HasCargo = false;
    private bool MovingCarrier = false;
    private bool CarrierSoundPlaying = false;

    private Coroutine CarrierVolumeFadeIn;
    private Coroutine CarrierVolumeFadeOut;

    private void Start()
    {
        MoveInputAction = PlayerInput.actions["Move"];

        Rope.positionCount = 2;
    }

    private void Update()
    {
        Rope.SetPosition(0, Carrier.transform.position);
        Rope.SetPosition(1, Hook.transform.position);
    }

    private void FixedUpdate()
    {
        Vector2 move = MoveInputAction.ReadValue<Vector2>();

        float newCarrierPos = Carrier.position.x + move.x * Time.deltaTime * CarrierSpeed;
        float clampedCarrierPos = Mathf.Clamp(newCarrierPos, CarrierXMin, CarrierXMax);
        Carrier.position = new Vector2(clampedCarrierPos, Carrier.position.y);

        float newHookDistance = HookJoint.distance - move.y * Time.deltaTime * HookSpeed;
        HookJoint.distance = Mathf.Clamp(newHookDistance, HookDistanceMin, HookDistanceMax);

        MovingCarrier = move.sqrMagnitude > 0.5f;
        if (MovingCarrier)
        {
            if (!CarrierSoundPlaying)
            {
                if (CarrierVolumeFadeOut != null)
                {
                    StopCoroutine(CarrierVolumeFadeOut);
                    CarrierVolumeFadeOut = null;
                }

                CarrierVolumeFadeIn = StartCoroutine(VolumeFade(CarrierSound, 0.5f, 0.25f));
                CarrierSound.Play();

                CarrierSoundPlaying = true;
            }
        }
        else
        {
            if (CarrierSoundPlaying)
            {
                if (CarrierVolumeFadeIn != null)
                {
                    StopCoroutine(CarrierVolumeFadeIn);
                    CarrierVolumeFadeIn = null;
                }

                CarrierVolumeFadeOut = StartCoroutine(VolumeFade(CarrierSound, 0, 0.25f));

                CarrierSoundPlaying = false;
            }
        }
    }

    private void OnGrab()
    {
        if (HasCargo)
        {
            FixedJoint2D joint = Hook.gameObject.GetComponent<FixedJoint2D>();
            Destroy(joint);

            GrabSound.Play();

            HasCargo = false;
        }
        else
        {
            if (Cargo != null)
            {
                FixedJoint2D joint = Hook.gameObject.AddComponent<FixedJoint2D>();
                joint.connectedBody = Cargo;

                GrabSound.Play();

                HasCargo = true;
            }
        }
    }

    private IEnumerator VolumeFade(AudioSource audioSource, float endVolume, float fadeLength)
    {
        float startVolume = audioSource.volume;
        float startTime = Time.time;

        while (Time.time < startTime + fadeLength)
        {
            audioSource.volume = startVolume + (endVolume - startVolume) * (Time.time - startTime) / fadeLength;

            yield return null;
        }

        if (endVolume == 0)
        {
            audioSource.Stop();
        }
    }
}
