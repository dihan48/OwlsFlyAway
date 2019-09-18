using System.Collections.Generic;
using UnityEngine;

public class EqualSearch : MonoBehaviour
{
    GameElement[,] matrix = new GameElement[36, 9] {
            {new GameElement(0,0), new GameElement(0,1), new GameElement(0,2), new GameElement(0,3), new GameElement(0,4), new GameElement(0,5), new GameElement(0,6), new GameElement(0,7), new GameElement(0,8)},
            {new GameElement(1,0), new GameElement(1,1), new GameElement(1,2), new GameElement(1,3), new GameElement(1,4), new GameElement(1,5), new GameElement(1,6), new GameElement(1,7), new GameElement(1,8)},
            {new GameElement(2,0), new GameElement(2,1), new GameElement(2,2), new GameElement(2,3), new GameElement(2,4), new GameElement(2,5), new GameElement(2,6), new GameElement(2,7), new GameElement(2,8)},
            {new GameElement(3,0), new GameElement(3,1), new GameElement(3,2), new GameElement(3,3), new GameElement(3,4), new GameElement(3,5), new GameElement(3,6), new GameElement(3,7), new GameElement(3,8)},
            {new GameElement(4,0), new GameElement(4,1), new GameElement(4,2), new GameElement(4,3), new GameElement(4,4), new GameElement(4,5), new GameElement(4,6), new GameElement(4,7), new GameElement(4,8)},
            {new GameElement(5,0), new GameElement(5,1), new GameElement(5,2), new GameElement(5,3), new GameElement(5,4), new GameElement(5,5), new GameElement(5,6), new GameElement(5,7), new GameElement(5,8)},
            {new GameElement(6,0), new GameElement(6,1), new GameElement(6,2), new GameElement(6,3), new GameElement(6,4), new GameElement(6,5), new GameElement(6,6), new GameElement(6,7), new GameElement(6,8)},
            {new GameElement(7,0), new GameElement(7,1), new GameElement(7,2), new GameElement(7,3), new GameElement(7,4), new GameElement(7,5), new GameElement(7,6), new GameElement(7,7), new GameElement(7,8)},
            {new GameElement(8,0), new GameElement(8,1), new GameElement(8,2), new GameElement(8,3), new GameElement(8,4), new GameElement(8,5), new GameElement(8,6), new GameElement(8,7), new GameElement(8,8)},

            {new GameElement(0,0), new GameElement(1,0), new GameElement(2,0), new GameElement(3,0), new GameElement(4,0), new GameElement(5,0), new GameElement(6,0), new GameElement(7,0), new GameElement(8,0)},
            {new GameElement(0,1), new GameElement(1,1), new GameElement(2,1), new GameElement(3,1), new GameElement(4,1), new GameElement(5,1), new GameElement(6,1), new GameElement(7,1), new GameElement(8,1)},
            {new GameElement(0,2), new GameElement(1,2), new GameElement(2,2), new GameElement(3,2), new GameElement(4,2), new GameElement(5,2), new GameElement(6,2), new GameElement(7,2), new GameElement(8,2)},
            {new GameElement(0,3), new GameElement(1,3), new GameElement(2,3), new GameElement(3,3), new GameElement(4,3), new GameElement(5,3), new GameElement(6,3), new GameElement(7,3), new GameElement(8,3)},
            {new GameElement(0,4), new GameElement(1,4), new GameElement(2,4), new GameElement(3,4), new GameElement(4,4), new GameElement(5,4), new GameElement(6,4), new GameElement(7,4), new GameElement(8,4)},
            {new GameElement(0,5), new GameElement(1,5), new GameElement(2,5), new GameElement(3,5), new GameElement(4,5), new GameElement(5,5), new GameElement(6,5), new GameElement(7,5), new GameElement(8,5)},
            {new GameElement(0,6), new GameElement(1,6), new GameElement(2,6), new GameElement(3,6), new GameElement(4,6), new GameElement(5,6), new GameElement(6,6), new GameElement(7,6), new GameElement(8,6)},
            {new GameElement(0,7), new GameElement(1,7), new GameElement(2,7), new GameElement(3,7), new GameElement(4,7), new GameElement(5,7), new GameElement(6,7), new GameElement(7,7), new GameElement(8,7)},
            {new GameElement(0,8), new GameElement(1,8), new GameElement(2,8), new GameElement(3,8), new GameElement(4,8), new GameElement(5,8), new GameElement(6,8), new GameElement(7,8), new GameElement(8,8)},

            {new GameElement(0,4), new GameElement(1,5), new GameElement(2,6), new GameElement(3,7), new GameElement(4,8),null,null,null,null},
            {new GameElement(0,3), new GameElement(1,4), new GameElement(2,5), new GameElement(3,6), new GameElement(4,7), new GameElement(5,8),null,null,null},
            {new GameElement(0,2), new GameElement(1,3), new GameElement(2,4), new GameElement(3,5), new GameElement(4,6), new GameElement(5,7), new GameElement(6,8),null,null},
            {new GameElement(0,1), new GameElement(1,2), new GameElement(2,3), new GameElement(3,4), new GameElement(4,5), new GameElement(5,6), new GameElement(6,7), new GameElement(7,8),null},
            {new GameElement(0,0), new GameElement(1,1), new GameElement(2,2), new GameElement(3,3), new GameElement(4,4), new GameElement(5,5), new GameElement(6,6), new GameElement(7,7), new GameElement(8,8)},
            {new GameElement(1,0), new GameElement(2,1), new GameElement(3,2), new GameElement(4,3), new GameElement(5,4), new GameElement(6,5), new GameElement(7,6), new GameElement(8,7),null},
            {new GameElement(2,0), new GameElement(3,1), new GameElement(4,2), new GameElement(5,3), new GameElement(6,4), new GameElement(7,5), new GameElement(8,6),null,null},
            {new GameElement(3,0), new GameElement(4,1), new GameElement(5,2), new GameElement(6,3), new GameElement(7,4), new GameElement(8,5),null,null,null},
            {new GameElement(4,0), new GameElement(5,1), new GameElement(6,2), new GameElement(7,3), new GameElement(8,4),null,null,null,null},

            {new GameElement(0,4), new GameElement(1,3), new GameElement(2,2), new GameElement(3,1), new GameElement(4,0),null,null,null,null},
            {new GameElement(0,5), new GameElement(1,4), new GameElement(2,3), new GameElement(3,2), new GameElement(4,1), new GameElement(5,0),null,null,null},
            {new GameElement(0,6), new GameElement(1,5), new GameElement(2,4), new GameElement(3,3), new GameElement(4,2), new GameElement(5,1), new GameElement(6,0),null,null},
            {new GameElement(0,7), new GameElement(1,6), new GameElement(2,5), new GameElement(3,4), new GameElement(4,3), new GameElement(5,2), new GameElement(6,1), new GameElement(7,0),null},
            {new GameElement(0,8), new GameElement(1,7), new GameElement(2,6), new GameElement(3,5), new GameElement(4,4), new GameElement(5,3), new GameElement(6,2), new GameElement(7,1), new GameElement(8,0)},
            {new GameElement(1,8), new GameElement(2,7), new GameElement(3,6), new GameElement(4,5), new GameElement(5,4), new GameElement(6,3), new GameElement(7,2), new GameElement(8,1),null},
            {new GameElement(2,8), new GameElement(3,7), new GameElement(4,6), new GameElement(5,5), new GameElement(6,4), new GameElement(7,3), new GameElement(8,2),null,null},
            {new GameElement(3,8), new GameElement(4,7), new GameElement(5,6), new GameElement(6,5), new GameElement(7,4), new GameElement(8,3),null,null,null},
            {new GameElement(4,8), new GameElement(5,7), new GameElement(6,6), new GameElement(7,5), new GameElement(8,4),null,null,null,null}
    };

    string[] colorSprite = { "Red", "Orange", "Yellow", "Green", "Cyan", "Blue", "Purple" };

    List<string> sprites;
    List<GameElement> like = new List<GameElement>();

    public static bool soundMarker = false;

    List<GameObject> allLikeGameObjects = new List<GameObject>();
    List<GameElement> allLike = new List<GameElement>();

    public bool check_line(int n)
    {
        bool returnVar = false;
        for (int i = 0; i < 9; i++)
        {
            GameElement km = matrix[n, i];
            Debug.Log("km " + km);
            if (!(km is null))
            {
                if (!(GameLogic.objs[km.x, km.y] is null))
                {
                    Debug.Log("sprites[0]" + sprites[0]);
                    if (sprites != null)
                    {
                        GameObject o = (GameObject)GameLogic.objs[km.x, km.y];
                        List<string> spriteCheck = o.GetComponent<GameElementColor>().CheckColor(sprites);
                        Debug.Log("spriteCheck" + spriteCheck + "sprites[0]" + sprites[0]);
                        if (spriteCheck != null)
                        {
                            like.Add(new GameElement(km.x, km.y));
                            sprites = spriteCheck;
                        }
                        else
                        {
                            if (like.Count < 5)
                            {
                                like = new List<GameElement>();
                                like.Add(new GameElement(km.x, km.y));
                                o = (GameObject)GameLogic.objs[km.x, km.y];
                                sprites = o.GetComponent<GameElementColor>().GetSprites();
                            }
                            else
                            {
                                for (int g = 0; g < like.Count; g++)
                                {
                                    allLikeGameObjects.Add((GameObject)GameLogic.objs[like[g].x, like[g].y]);
                                    allLike.Add(new GameElement(like[g].x, like[g].y));

                                    returnVar = true;
                                }
                                like = new List<GameElement>();
                                sprites = null;
                            }
                        }
                    }
                    else
                    {
                        GameObject o = (GameObject)GameLogic.objs[km.x, km.y];
                        sprites = o.GetComponent<GameElementColor>().GetSprites();
                        like = new List<GameElement>();
                        like.Add(new GameElement(km.x, km.y));
                    }
                }
                else
                {
                    if (like.Count >= 5)
                    {
                        for (int g = 0; g < like.Count; g++)
                        {
                            allLikeGameObjects.Add((GameObject)GameLogic.objs[like[g].x, like[g].y]);
                            allLike.Add(new GameElement(like[g].x, like[g].y));
                            returnVar = true;
                        }
                        like = new List<GameElement>();
                        sprites = null;
                    }
                    else
                    {
                        like = new List<GameElement>();
                        sprites = null;
                    }
                }
            }
        }
        if (like.Count >= 5)
        {
            for (int g = 0; g < like.Count; g++)
            {
                allLikeGameObjects.Add((GameObject)GameLogic.objs[like[g].x, like[g].y]);
                allLike.Add(new GameElement(like[g].x, like[g].y));
                returnVar = true;
            }
            like = new List<GameElement>();
            sprites = null;
        }
        return returnVar;
    }

    public bool check_line_1(int n)
    {
        bool returnVar = false;

        for (int c = 0; c < 7; c++)
        {

            List<GameElement> like = new List<GameElement>();
            List<string> sprites;
            for (int i = 0; i < 9; i++)
            {

                GameElement km = matrix[n, i];

                if (!(km is null) && !(GameLogic.objs[km.x, km.y] is null))
                {

                    GameObject o = (GameObject)GameLogic.objs[km.x, km.y];

                    sprites = o.GetComponent<GameElementColor>().GetSprites();

                    bool likebool = false;
                    for (int s = 0; s < sprites.Count; s++)
                    {

                        if (colorSprite[c] == sprites[s])
                        {
                            like.Add(new GameElement(km.x, km.y));
                            likebool = true;
                        }
                    }
                    if (!likebool)
                    {
                        if (like.Count < 5)
                        {
                            like = new List<GameElement>();
                        }
                        else
                        {
                            for (int g = 0; g < like.Count; g++)
                            {
                                allLikeGameObjects.Add((GameObject)GameLogic.objs[like[g].x, like[g].y]);
                                allLike.Add(new GameElement(like[g].x, like[g].y));
                                returnVar = true;
                            }
                            like = new List<GameElement>();
                        }
                    }
                }
                else
                {
                    if (like.Count < 5)
                    {
                        like = new List<GameElement>();
                    }
                    else
                    {
                        for (int g = 0; g < like.Count; g++)
                        {
                            allLikeGameObjects.Add((GameObject)GameLogic.objs[like[g].x, like[g].y]);
                            allLike.Add(new GameElement(like[g].x, like[g].y));
                            returnVar = true;
                        }
                        like = new List<GameElement>();
                    }
                }
            }
            if (like.Count < 5)
            {
                like = new List<GameElement>();
            }
            else
            {
                for (int g = 0; g < like.Count; g++)
                {
                    allLikeGameObjects.Add((GameObject)GameLogic.objs[like[g].x, like[g].y]);
                    allLike.Add(new GameElement(like[g].x, like[g].y));
                    returnVar = true;
                }
                like = new List<GameElement>();
            }
        }
        return returnVar;
    }

    public bool check()
    {
        var returnVar = false;
        for (int i = 0; i < 36; i++)
        {
            like = new List<GameElement>();
            returnVar = check_line_1(i) ? true : returnVar;

        }

        for (int i = 0; i < allLikeGameObjects.Count; i++)
        {
            if (GameLogic.objs[allLike[i].x, allLike[i].y] != null)
            {
                GameLogic.objs[allLike[i].x, allLike[i].y] = null;
                allLikeGameObjects[i].GetComponent<PlayerSpawnFX>().DestroyPlayer();
            }
        }

        allLikeGameObjects = new List<GameObject>();
        allLike = new List<GameElement>();

        if (returnVar)
        {
            soundMarker = true;
        }
        return returnVar;
    }
}
