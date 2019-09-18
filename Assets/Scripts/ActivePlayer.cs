using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class ActivePlayer : MonoBehaviour, IPointerDownHandler
{
    public static GameObject action;
    public AudioClip ugu;
    public Vector3 StartPoint;
    public ExchangeButton exchangeButton;
    int animCount = -1;
    float ActAnimStepL;
    float ActAnimStep;
    float animTime;
    float Lenp;
    float TimerStart;
    float animMultiply = 0.1f;
    GameLogic g;
    EqualSearch e;
    Coroutine eCheck;
    AudioSource audioS;
    Animator anim;
    Physics2DRaycaster pr;
    Vector3 LenpStartPoint;

    static GameObject OneExchangeCharacter;
    static GameObject TwoExchangeCharacter;

    


    void Start()
    {
        StartPoint = gameObject.transform.localPosition;
        animTime = -1;
        anim = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (action == gameObject)
        {

            if (parhActive.pathFinish != null)
            {
                MoveAnim();
                if (!anim.GetBool("fly"))
                {
                    anim.SetBool("fly", true);
                }
            }
            else
            {
                if (!anim.GetBool("action"))
                {
                    anim.SetBool("action", true);
                }
            }
        }
        else
        {
            gameObject.transform.localPosition = StartPoint;
            anim.SetBool("action", false);
            anim.SetBool("fly", false);
        }
    }

    private void MoveAnim()
    {
        if (animCount == -1)
        {
            animCount = parhActive.pathFinish.Count;
        }
        if (animCount >= 0)
        {
            if (Time.time > animTime)
            {
                animCount--;
                if (animCount == -1)
                {
                    MoveEndAnim();
                    return;
                }
                GameLogic.objs[(int)StartPoint.y, (int)StartPoint.x] = null;
                Vector3 v = new Vector3(parhActive.pathFinish[animCount].x, parhActive.pathFinish[animCount].y, 0);
                LenpStartPoint = gameObject.transform.localPosition;
                TimerStart = Time.time;
                StartPoint = v;
                GameLogic.objs[(int)StartPoint.y, (int)StartPoint.x] = gameObject;
                animTime = Time.time + animMultiply;
            }
            else
            {
                Lenp = (Time.time - TimerStart) / animMultiply;
                gameObject.transform.localPosition = Vector3.Lerp(LenpStartPoint, StartPoint, Lenp);
            }
        }
    }

    private void MoveEndAnim()
    {
        StartPoint = new Vector3(parhActive.pathFinish[0].x, parhActive.pathFinish[0].y, 0);
        GameLogic.objs[(int)StartPoint.y, (int)StartPoint.x] = gameObject;
        parhActive.pathFinish = null;
        action = null;
        g = (GameLogic)gameObject.GetComponentInParent(typeof(GameLogic));
        e = (EqualSearch)gameObject.GetComponentInParent(typeof(EqualSearch));


        if (!e.check())
        {
            g.RndAddObj();
            eCheck = StartCoroutine(ECheck());
        }


    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (GameA.input && action != gameObject && !ExchangeButton.active)
        {
            AudioClick.Play();
            PathFinder pf = (PathFinder)gameObject.GetComponentInParent(typeof(PathFinder));
            pf.findPath((int)gameObject.transform.localPosition.x, (int)gameObject.transform.localPosition.y);
            action = gameObject;
        }
    }
    IEnumerator ECheck()
    {
        yield return new WaitUntil(() => GameA.input == true);
        e.check();
        StopCoroutine(eCheck);
    }


}

