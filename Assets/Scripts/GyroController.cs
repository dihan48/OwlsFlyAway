using UnityEngine;
using UnityEngine.UI;

public class GyroController : MonoBehaviour
{

    public Text text;
    void Update()
    {
        Input.gyro.enabled = true;
        text.text = "acceleration = " + Input.acceleration +
            ";\n enabled = " + Input.gyro.enabled +
            ";\n attitude = " + Input.gyro.attitude +
            ";\n gravity = " + Input.gyro.gravity +
            ";\n rotationRate = " + Input.gyro.rotationRate +
            ";\n rotationRateUnbiased = " + Input.gyro.rotationRateUnbiased +
            ";\n updateInterval = " + Input.gyro.updateInterval +
            ";\n gyro.userAcceleration = " + Input.gyro.userAcceleration;

        transform.position = Input.gyro.gravity;
    }



}
