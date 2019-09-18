using System.Collections.Generic;
using UnityEngine;

public class GameElementColor : MonoBehaviour
{
    public GameObject[] ColorizedObjects;
    public GameObject[] AllObjects;

    public Color Color
    {
        get
        {
            return Color;
        }
        set
        {
            for (int i = 0; i < ColorizedObjects.Length; i++)
            {
                ColorizedObjects[i].GetComponent<SpriteRenderer>().color = value;
            }
        }
    }
    public List<string> sprites = new List<string>();

    public List<string> CheckColor(List<string> sprts)
    {
        for (int i = 0; i < sprites.Count; i++)
        {
            for (int j = 0; j < sprts.Count; j++)
            {
                if (sprts[j] == sprites[i])
                {
                    List<string> spritesReturn = new List<string>();
                    spritesReturn.Add(sprites[i]);
                    return spritesReturn;
                }
            }

        }
        return null;
    }

    public List<string> GetSprites()
    {
        return sprites;
    }

    public void SetSprites(string str, Color color)
    {
        sprites.Add(str);
        this.Color = color;
        for (int i = 0; i < ColorizedObjects.Length; i++)
        {
            ColorizedObjects[i].GetComponent<SpriteRenderer>().color = color;
        }
    }

    public void SetSprites(string str)
    {
        sprites.Add(str);
        Color = StringToColor(str);
        for (int i = 0; i < ColorizedObjects.Length; i++)
        {
            ColorizedObjects[i].GetComponent<SpriteRenderer>().color = Color;
        }
    }

    public Color StringToColor(string str)
    {
        Color Color;
        switch (str)
        {
            case "Red":
                Color = new Color(1f, 0f, 0f, 1f);
                break;
            case "Orange":
                Color = new Color(1f, 0.5f, 0f, 1f);
                break;
            case "Yellow":
                Color = new Color(1f, 1f, 0f, 1f);
                break;
            case "Green":
                Color = new Color(0f, 1f, 0f, 1f);
                break;
            case "Cyan":
                Color = new Color(0f, 1f, 1f, 1f);
                break;
            case "Blue":
                Color = new Color(0f, 0f, 1f, 1f);
                break;
            case "Purple":
                Color = new Color(1f, 0f, 1f, 1f);
                break;
            default:
                Color = new Color(0f, 0f, 0f, 1f);
                break;
        }
        return Color;
    }

    public void ChangeSortingOrder(int n)
    {
        for (int i = 0; i < AllObjects.Length; i++)
        {
            AllObjects[i].GetComponent<SpriteRenderer>().sortingOrder += n;
        }
    }
}
