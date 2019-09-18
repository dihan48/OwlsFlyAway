using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class parhActive : MonoBehaviour, IPointerDownHandler
{
    public static List<GameElement> pathFinish = null;
    GameLogic gl;
    PathFinder pf;

    void Start()
    {
        GameLogic gl = (GameLogic)gameObject.GetComponentInParent(typeof(GameLogic));
        PathFinder pf = (PathFinder)gameObject.GetComponentInParent(typeof(PathFinder));
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (GameA.input)
        {
            AudioClick.Play();
            pf = (PathFinder)gameObject.GetComponentInParent(typeof(PathFinder));
            pathFinish = pf.pathFinish(ActivePlayer.action, (int)gameObject.transform.localPosition.x, (int)gameObject.transform.localPosition.y);
            pf.destroyPachGO();
            GameA.input = false;
        }
    }

}
