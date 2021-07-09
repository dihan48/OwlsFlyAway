using UnityEngine;

[ExecuteInEditMode]
public class Background : MonoBehaviour
{
    [SerializeField] private SpriteRenderer[] spriteRenderers;
    private Camera mainCamera;
    private float cameraAspect;

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
        if(mainCamera == null)
        {
            return;
        }

        cameraAspect = mainCamera.aspect;
        var cameraX = mainCamera.orthographicSize * mainCamera.aspect * 2;
        var cameraY = mainCamera.orthographicSize * 2;

        foreach (var item in spriteRenderers)
        {
            if(item == null)
            {
                continue;
            }

            var size = item.size;

            var scale = cameraY / size.y;
            size *= scale;
            item.size = size;

            if (cameraAspect > 1 && cameraX > size.x)
            {
                scale = cameraX / size.x;
                size *= scale;
                item.size = size;
            }

            item.size *= 1.2f;
        }
    }
}
