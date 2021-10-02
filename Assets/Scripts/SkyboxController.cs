using UnityEngine;

public class SkyboxController : MonoBehaviour
{
    public Transform Sky1;
    public Transform Sky2;
    public Transform Sky3;
    public Transform Sky4;

    public float Speed = 1;
    public float Width = 1;

    private void Update()
    {
        Sky1.transform.position -= Speed * new Vector3(1, 0, 0) * Time.deltaTime;
        Sky2.transform.position -= Speed * new Vector3(1, 0, 0) * Time.deltaTime;
        Sky3.transform.position -= Speed * new Vector3(1, 0, 0) * Time.deltaTime;
        Sky4.transform.position -= Speed * new Vector3(1, 0, 0) * Time.deltaTime;

        if (Sky1.transform.position.x < -2 * Width)
        {
            Sky1.transform.position += 4 * Width * new Vector3(1, 0, 0);
        }

        if (Sky2.transform.position.x < -2 * Width)
        {
            Sky2.transform.position += 4 * Width * new Vector3(1, 0, 0);
        }

        if (Sky3.transform.position.x < -2 * Width)
        {
            Sky3.transform.position += 4 * Width * new Vector3(1, 0, 0);
        }

        if (Sky4.transform.position.x < -2 * Width)
        {
            Sky4.transform.position += 4 * Width * new Vector3(1, 0, 0);
        }
    }
}
