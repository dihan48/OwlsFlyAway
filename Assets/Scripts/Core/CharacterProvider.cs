using System.Collections.Generic;
using UnityEngine;

public class CharacterProvider : MonoBehaviour
{
    [SerializeField] private int width = 9;
    [SerializeField] private int heght = 9;
    [SerializeField] private List<Character> characterPrefabs;

    public int Width { get => width; }

    public int Height { get => heght; }

    private Character[,] allCharacters;

    public Character Create(int x, int y, int prefabIndex)
    {
        if (0 > x || x >= width || 0 > y || y >= heght)
        {
            return null;
        }

        if (prefabIndex < 0 || prefabIndex >= characterPrefabs.Count)
        {
            return null;
        }

        Vector3 position = transform.position + new Vector3(x, y, 0);
        Character character = Character.Instantiate(characterPrefabs[prefabIndex], prefabIndex, transform, position);
        allCharacters[x, y] = character;
        return character;
    }

    public Character GetPrefab(int index)
    {
        if (index < 0 || index >= characterPrefabs.Count)
        {
            return null;
        }

        return characterPrefabs[index];
    }

    public int GetRandomPrefabIndex() => Random.Range(0, characterPrefabs.Count);

    public void Move(Vector2Int moveTo, Character character)
    {
        Remove(character);
        allCharacters[moveTo.x, moveTo.y] = character;
    }

    public void Destroy(Character destroyed)
    {
        for (int y = 0; y < heght; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (allCharacters[x, y] == destroyed)
                {
                    destroyed.DestroyWithEffect(true, true);
                    allCharacters[x, y] = null;
                    return;
                }
            }
        }
    }

    public void Clear()
    {
        for (int y = 0; y < heght; y++)
        {
            for (int x = 0; x < width; x++)
            {
                Character character = allCharacters[x, y];
                if (character != null)
                {
                    Destroy(character.gameObject);
                    allCharacters[x, y] = null;
                }
            }
        }
    }

    public List<Vector2Int> GetAllEmptyPosition()
    {
        List<Vector2Int> empty = new List<Vector2Int>();

        for (int y = 0; y < heght; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (allCharacters[x, y] == null)
                {
                    empty.Add(new Vector2Int(x, y));
                }
            }
        }

        return empty;
    }

    public List<Vector2Int> GetAllFillPosition()
    {
        List<Vector2Int> allFill = new List<Vector2Int>();

        for (int y = 0; y < heght; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (allCharacters[x, y] != null)
                {
                    allFill.Add(new Vector2Int(x, y));
                }
            }
        }

        return allFill;
    }

    public Character Get(int x, int y) => IsExistsPosition(new Vector2Int(x, y)) ? allCharacters[x, y] : null;

    public bool IsExistsPosition(Vector2Int pos) => pos.x > -1 && pos.x < width && pos.y > -1 && pos.y < heght;

    private void Awake()
    {
        allCharacters = new Character[width, heght];
    }

    private void Remove(Character character)
    {
        for (int y = 0; y < heght; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (allCharacters[x, y] == character)
                {
                    allCharacters[x, y] = null;
                    return;
                }
            }
        }
    }
}
