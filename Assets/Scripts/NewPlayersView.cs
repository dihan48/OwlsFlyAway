using System.Collections.Generic;
using UnityEngine;

public class NewPlayersView : MonoBehaviour
{
    public GameObject player;
    public Color color;

    List<GameObject> view = new List<GameObject>();

    void Start()
    {

    }

    public void UpdatePlayersView(List<string> ColorPrefs)
    {
        if (ColorPrefs.Count > 0)
        {
            foreach (var item in view)
            {
                Destroy(item);
            }
            view.Clear();

            for (int i = 0; i < ColorPrefs.Count; i++)
            {
                GameObject o = Instantiate(player, transform);
                switch (ColorPrefs[i])
                {
                    case "Red":
                        color = new Color(1f, 0f, 0f, 1f);
                        break;
                    case "Orange":
                        color = new Color(1f, 0.5f, 0f, 1f);
                        break;
                    case "Yellow":
                        color = new Color(1f, 1f, 0f, 1f);
                        break;
                    case "Green":
                        color = new Color(0f, 1f, 0f, 1f);
                        break;
                    case "Cyan":
                        color = new Color(0f, 1f, 1f, 1f);
                        break;
                    case "Blue":
                        color = new Color(0f, 0f, 1f, 1f);
                        break;
                    case "Purple":
                        color = new Color(1f, 0f, 1f, 1f);
                        break;
                    default:
                        break;
                }
                o.GetComponent<GameElementColor>().SetSprites(ColorPrefs[i], color);
                o.transform.SetParent(transform);
                o.name = ColorPrefs[i];
                o.transform.localPosition = new Vector3(i, 0, 0);
                view.Add(o.gameObject);
            }
        }
    }
}
