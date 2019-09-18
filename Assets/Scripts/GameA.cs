using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameA : MonoBehaviour
{
    GameLogic g;
    Coroutine GameOverCor = null;
    PathFinder pf;
    public static bool input = true;
    bool gameOver = false;
    public Text txt;
    public Text txt2;
    public SaveLoadManager slm;


    void Start()
    {
        input = true;
        g = GetComponent<GameLogic>();
        slm.LoadGame();
        pf = GetComponent<PathFinder>();
    }

    void Update()
    {
        if (GameLogic.EmptyObj() == 0 && GameOverCor == null && !gameOver)
        {
            gameOver = true;
            GameOverCor = StartCoroutine(GameOverCoroutine());
        }
    }

    public void Restart()
    {
        if (GameOverCor == null)
            GameOverCor = StartCoroutine(GameOverCoroutine());
    }

    IEnumerator GameOverCoroutine()
    {
        yield return new WaitForSeconds(1);
        g.Restart();
        pf.destroyPachGO();
        gameOver = false;
        StopCoroutine(GameOverCor);
        GameOverCor = null;
    }

}
