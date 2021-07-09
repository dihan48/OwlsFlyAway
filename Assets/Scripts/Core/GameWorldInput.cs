using UnityEngine;

[RequireComponent(typeof(GameWorld))]

public class GameWorldInput : MonoBehaviour
{
    private GameWorld gameWorld;
    private Camera gameCamera;

    private void Start()
    {
        gameCamera = Camera.main;
        gameWorld = GetComponent<GameWorld>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Click(Input.mousePosition);
        }
    }

    private void Click(Vector2 screenPosition)
    {
        var worldPosition = gameCamera.ScreenToWorldPoint(screenPosition);
        var loaclPosition = worldPosition - transform.position;
        var position = Vector2Int.RoundToInt(loaclPosition);

        gameWorld.SelectPosition(position);
    }
}
