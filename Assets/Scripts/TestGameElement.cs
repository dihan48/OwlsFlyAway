using System.Collections.Generic;
using UnityEngine;

public class TestGameElement : MonoBehaviour
{
    List<GameElement> g = new List<GameElement>();
    void Start()
    {
        g.Add(new GameElement(0, 0));
        g.Add(new GameElement(1, 0));
        g.Add(new GameElement(0, 1));
        g.Add(new GameElement(1, 1));
        g.Add(new GameElement(1, 1));
        g.Add(null);
        g.Add(null);
    }
}
