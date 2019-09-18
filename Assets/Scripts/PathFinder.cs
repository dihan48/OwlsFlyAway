using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    List<GameElement> path = new List<GameElement>();
    List<GameElement> pathlink = new List<GameElement>();
    List<GameElement> open = new List<GameElement>();
    List<GameElement> close = new List<GameElement>();
    List<GameObject> pathGO = new List<GameObject>();
    public GameObject pref;

    public void nodaCheck(int nod)
    {
        int x = open[nod].x;
        int y = open[nod].y;
        object right = x + 1 < 9 ? GameLogic.objs[y, x + 1] : new GameElement(0, 0);
        object left = x - 1 >= 0 ? GameLogic.objs[y, x - 1] : new GameElement(0, 0);
        object top = y - 1 >= 0 ? GameLogic.objs[y - 1, x] : new GameElement(0, 0);
        object bottom = y + 1 < 9 ? GameLogic.objs[y + 1, x] : new GameElement(0, 0);

        if (right == null)
        {
            if (close.IndexOf(new GameElement((x + 1), y), 0) == -1)
            {
                if (path.IndexOf(new GameElement((x + 1), y)) == -1)
                {
                    path.Add(new GameElement(x + 1, y));
                    pathlink.Add(new GameElement(x, y));
                    open.Add(new GameElement(x + 1, y));
                }
            }
        }
        if (left == null)
        {
            if (close.IndexOf(new GameElement((x - 1), y)) == -1)
            {
                if (path.IndexOf(new GameElement((x - 1), y)) == -1)
                {
                    path.Add(new GameElement(x - 1, y));
                    pathlink.Add(new GameElement(x, y));
                    open.Add(new GameElement(x - 1, y));
                }
            }
        }
        if (top == null)
        {
            if (close.IndexOf(new GameElement(x, (y - 1))) == -1)
            {
                if (path.IndexOf(new GameElement(x, (y - 1))) == -1)
                {
                    path.Add(new GameElement(x, y - 1));
                    pathlink.Add(new GameElement(x, y));
                    open.Add(new GameElement(x, y - 1));
                }
            }
        }
        if (bottom == null)
        {
            if (close.IndexOf(new GameElement(x, (y + 1))) == -1)
            {
                if (path.IndexOf(new GameElement(x, (y + 1))) == -1)
                {
                    path.Add(new GameElement(x, y + 1));
                    pathlink.Add(new GameElement(x, y));
                    open.Add(new GameElement(x, y + 1));
                }
            }
        }


        if (open[nod].x == x && open[nod].y == y)
        {
            close.Add(new GameElement(x, y));
            open.RemoveAt(nod);
        }
    }


    public void findPath(int x, int y)
    {
        path = new List<GameElement>();
        pathlink = new List<GameElement>();
        open = new List<GameElement>();
        close = new List<GameElement>();

        for (int i = 0; i < pathGO.Count; i++)
        {
            Destroy(pathGO[i]);
        }
        pathGO = new List<GameObject>();

        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                if (GameLogic.objs[i, j] != null)
                {
                    close.Add(new GameElement(j, i));
                }
            }
        }

        open.Add(new GameElement(x, y));
        while (open.Count > 0)

        {
            nodaCheck(0);
        }
        for (int i = 0; i < path.Count; i++)
        {
            pathGO.Add(Instantiate(pref, transform));
            pathGO[i].transform.localPosition = new Vector3(path[i].x, path[i].y, 0);
        }

    }

    public void destroyPachGO()
    {
        for (int i = 0; i < pathGO.Count; i++)
        {
            Destroy(pathGO[i]);
        }
        pathGO = new List<GameObject>();
    }

    public List<GameElement> pathFinish(GameObject p, int xf, int yf)
    {
        List<GameElement> pathFinish = new List<GameElement>();

        int xft = xf;
        int yft = yf;

        Vector3 pc = p.GetComponent<ActivePlayer>().StartPoint;
        findPath((int)pc.x, (int)pc.y);
        pathFinish.Add(new GameElement(xf, yf));
        GameElement start = new GameElement((int)pc.x, (int)pc.y);
        GameElement finish = new GameElement(xf, yf);
        int moveto = path.IndexOf(finish);

        while ((start.x != pathlink[moveto].x) || (start.y != pathlink[moveto].y))
        {
            finish = pathlink[moveto];
            pathFinish.Add(pathlink[moveto]);
            moveto = path.IndexOf(finish);
        }
        return pathFinish;
    }
}
