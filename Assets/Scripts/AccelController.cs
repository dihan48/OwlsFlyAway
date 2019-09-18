using UnityEngine;

public class AccelController : MonoBehaviour
{
    public float mult = 1.5f;
    Vector3 pos;
    Vector3 gravity;

    void Start()
    {
        pos = transform.position;
        Input.gyro.enabled = true;
    }
    void Update()
    {

        gravity = Input.gyro.gravity;
        gravity.y *= mult;
        gravity.x *= mult;
        transform.position = pos + gravity;

    }

}
