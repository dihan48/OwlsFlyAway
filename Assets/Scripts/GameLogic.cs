using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    public GameObject player;
    public GameObject parent;
    public string[] oneColors = { "Red", "Orange", "Yellow", "Green", "Cyan", "Blue", "Purple" };
    public Color color;
    public GameObject[] Obg_double_color;
    public NewPlayersView newPlayersView;
    public static object[,] objs = new object[9, 9];
    public SaveLoadManager slm;
    public AdMobController adc;
    public static List<string> ColorPrefs = new List<string>();

    private static List<int[]> entryPlayer;

    int entryPlayerCount;
    int addPlCount;

    void Start()
    {
        object[,] objs = new object[9, 9];
    }

    public void Clear()
    {
        for (int x = 0; x < 9; x++)
        {
            for (int y = 0; y < 9; y++)
            {
                if (objs[x, y] != null)
                {
                    GameObject o = (GameObject)objs[x, y];
                    Destroy(o);
                    objs[x, y] = null;
                }
            }
        }
    }

    public void Restart()
    {
        Clear();
        RandColorPref();
        RndAddObj();
        CountingQuantity.count = 0;
        slm.DeleteSave();
        adc.InterstitialShow();
    }

    public void LoadData(string sprt, int i, int j)
    {
        if (sprt == null)
        {
            objs[i, j] = null;
        }
        else
        {
            string pref = sprt;
            SetObjs(i, j, pref);
        }
    }

    public object GetObjs(int y, int x)
    {
        return objs[y, x];
    }

    public string GetPrefOneColor(int pref)
    {
        return oneColors[pref - 1];
    }

    public GameObject GetPrefDoubleColor(int pref)
    {
        return Obg_double_color[pref - 1];
    }

    public void SetObjs(int y, int x, string pref)
    {
        GameObject o = Instantiate(player, transform);
        switch (pref)
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
        o.GetComponent<GameElementColor>().SetSprites(pref, color);
        o.name = pref;
        o.transform.localPosition = new Vector3(x, y, 0);
        objs[y, x] = o;
    }

    public static int EmptyObj()
    {
        entryPlayer = new List<int[]>();

        for (int y = 0; y < 9; y++)
        {
            for (int x = 0; x < 9; x++)
            {
                if (objs[y, x] == null)
                {
                    entryPlayer.Add(new int[] { y, x });
                }
            }
        }
        return entryPlayer.Count;
    }

    public List<string> RandColorPref()
    {
        entryPlayerCount = EmptyObj();
        ColorPrefs = new List<string>();
        if (entryPlayerCount > 0)
        {
            addPlCount = entryPlayerCount > 3 ? 3 : 2;
            addPlCount = entryPlayerCount > 2 ? addPlCount : 1;
            for (int i = 0; i < addPlCount; i++)
            {

                int index = Random.Range(1, 8);
                ColorPrefs.Add(GetPrefOneColor(index));
            }
        }
        newPlayersView.UpdatePlayersView(ColorPrefs);
        return ColorPrefs;
    }

    public void SetColorPref(List<string> sprt)
    {
        ColorPrefs = new List<string>();
        for (int i = 0; i < sprt.Count; i++)
        {
            ColorPrefs.Add(sprt[i]);
        }
    }

    public int RndAddObj()
    {
        List<int> addPlayers = new List<int>();
        entryPlayerCount = EmptyObj();

        if (entryPlayerCount > 0)
        {
            for (int g = 0; g < ColorPrefs.Count; g++)
            {
                int tempAdd = Random.Range(0, entryPlayerCount);

                if (addPlayers.IndexOf(tempAdd, 0) == -1)
                {
                    addPlayers.Add(tempAdd);
                }
                else
                {
                    g--;
                }
            }

            for (int p = 0; p < addPlayers.Count; p++)
            {
                SetObjs(entryPlayer[addPlayers[p]][0], entryPlayer[addPlayers[p]][1], ColorPrefs[p]);
            }
            RandColorPref();
            slm.SaveGame();
            return addPlayers.Count;
        }
        else
        {
            return addPlayers.Count;//gameOver

        }
    }


    public void UpdateNewPlayersView()
    {
        newPlayersView.UpdatePlayersView(ColorPrefs);
    }

}
