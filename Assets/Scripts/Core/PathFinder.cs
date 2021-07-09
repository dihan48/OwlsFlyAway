using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterProvider))]

public class PathFinder : MonoBehaviour
{
    [SerializeField]
    private GameObject pathMarker;

    private CharacterProvider characterProvider;

    private readonly List<Vector2Int> path = new List<Vector2Int>();
    private readonly List<Vector2Int> pathlink = new List<Vector2Int>();
    private readonly List<Vector2Int> open = new List<Vector2Int>();
    private readonly List<Vector2Int> close = new List<Vector2Int>();

    private readonly List<GameObject> pathMarkers = new List<GameObject>();

    public void CreatePathMap(Vector2Int position)
    {
        DestroyPathMarkers();

        path.Clear();
        pathlink.Clear();
        open.Clear();
        close.Clear();

        close.AddRange(characterProvider.GetAllFillPosition());

        open.Add(position);

        while (open.Count > 0)
        {
            NodaCheck();
        }

        foreach (var item in path)
        {
            pathMarkers.Add(Instantiate(pathMarker, transform));
            pathMarkers[pathMarkers.Count - 1].transform.localPosition = new Vector3(item.x, item.y, 0);
        }
    }

    public void DestroyPathMarkers()
    {
        foreach (var item in pathMarkers)
        {
            Destroy(item);
        }

        pathMarkers.Clear();
    }

    public List<Vector2Int> GetPath(Vector2Int start, Vector2Int finish)
    {
        List<Vector2Int> pathFinish = new List<Vector2Int>();

        pathFinish.Add(finish);
        int moveto = path.IndexOf(finish);

        while (start != pathlink[moveto])
        {
            finish = pathlink[moveto];
            pathFinish.Add(pathlink[moveto]);
            moveto = path.IndexOf(finish);
        }

        return pathFinish;
    }

    public bool IsInPathMap(Vector2Int position) => path.IndexOf(position) != -1;

    private void Start()
    {
        characterProvider = GetComponent<CharacterProvider>();
    }

    private void NodaCheck()
    {
        List<Vector2Int> dirVectors = new List<Vector2Int> { Vector2Int.right, Vector2Int.left, Vector2Int.up, Vector2Int.down };

        foreach (var item in dirVectors)
        {
            Vector2Int vector = open[0] + item;

            if (characterProvider.IsExistsPosition(vector) && characterProvider.Get(vector.x, vector.y) == null)
            {
                if (close.IndexOf(vector) == -1)
                {
                    if (path.IndexOf(vector) == -1)
                    {
                        path.Add(vector);
                        pathlink.Add(open[0]);
                        open.Add(vector);
                    }
                }
            }
        }

        close.Add(open[0]);
        open.RemoveAt(0);
    }
}