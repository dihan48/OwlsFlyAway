using UnityEngine;

[ExecuteInEditMode]
public class GameCamera : MonoBehaviour
{
    [SerializeField] private float minWidthView;
    [SerializeField] private float minHeightView;

    private float cameraAspect;
    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void Start()
    {
        Resize();
    }

    public void Resize()
    {
        if (mainCamera == null || cameraAspect == mainCamera.aspect)
        {
            return;
        }

        cameraAspect = mainCamera.aspect;
        var size = minWidthView / mainCamera.aspect;
        mainCamera.orthographicSize = size < minHeightView ? minHeightView : size;
    }
}
