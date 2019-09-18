using UnityEngine;

public class CameraScale : MonoBehaviour
{
    public RectTransform rectTransform;
    public new Camera camera;
    float sizeDelta = 0;

    void Update()
    {
        if (sizeDelta != rectTransform.sizeDelta[1])
        {
            sizeDelta = rectTransform.sizeDelta[1];
            camera.orthographicSize = rectTransform.sizeDelta[1] / 60;
        }

    }
}
