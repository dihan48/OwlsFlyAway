using UnityEngine;

[ExecuteInEditMode]
public class ScreenResize : MonoBehaviour
{
    [SerializeField] private GameCamera cameraScale;
    [SerializeField] private Background background;
    [SerializeField] private SafeArea safeArea;

    private void OnRectTransformDimensionsChange()
    {
        cameraScale.Resize();
        background.Resize();
        safeArea.Resize();
    }
}
