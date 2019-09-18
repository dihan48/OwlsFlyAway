using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class ExchangeButton : MonoBehaviour, IPointerClickHandler
{
    public static bool active = false;
    public PathFinder pf;
    public GameObject One;
    public GameObject Two;
    public GameObject ExchangePanel;
    public EqualSearch e;
    Coroutine SelectedButtom;
    RectTransform rt;
    Animator animOne;
    Animator animTwo;

    void Start()
    {
        rt = GetComponent<RectTransform>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (active)
        {
            rt.sizeDelta = new Vector2(30f, 30f);
            StopCoroutine(SelectedButtom);
            ExchangePanel.SetActive(false);
            active = false;
        }
        else
        {
            rt.sizeDelta = new Vector2(35f, 35f);
            SelectedButtom = StartCoroutine(OnSelectedButtom());
            ActivePlayer.action = null;
            pf.destroyPachGO();
            ExchangePanel.SetActive(true);
            active = true;
        }
    }

    public void OffExchange()
    {
        if (active)
        {
            rt.sizeDelta = new Vector2(30f, 30f);
            StopCoroutine(SelectedButtom);
            ExchangePanel.SetActive(false);
            active = false;
        }
    }

    IEnumerator OnSelectedButtom()
    {
        float startTime = Time.time;
        while (true)
        {
            float delta = Mathf.PingPong((Time.time - startTime) * 10, 5);
            rt.sizeDelta = new Vector2(30f + delta, 30f + delta);
            yield return null;
        }
    }

    public void ExchangeCharacters(GameObject one, GameObject two)
    {
        StartCoroutine(Exchange(one, two));
    }

    IEnumerator Exchange(GameObject one, GameObject two)
    {
        GameA.input = false;
        float startTime = Time.time;
        Vector3 onePos = one.transform.localPosition;
        Vector3 twoPos = two.transform.localPosition;
        one.GetComponent<GameElementColor>().ChangeSortingOrder(15);
        animOne = one.GetComponent<Animator>();
        animTwo = two.GetComponent<Animator>();


        while ((Time.time - startTime) < 1)
        {
            animOne.SetBool("fly", true);
            animTwo.SetBool("fly", true);
            two.GetComponent<ActivePlayer>().StartPoint = Vector3.Lerp(twoPos, onePos, Time.time - startTime);
            one.GetComponent<ActivePlayer>().StartPoint = Vector3.Lerp(onePos, twoPos, Time.time - startTime);
            yield return null;
        }

        GameLogic.objs[(int)onePos.y, (int)onePos.x] = two;
        GameLogic.objs[(int)twoPos.y, (int)twoPos.x] = one;
        one.GetComponent<GameElementColor>().ChangeSortingOrder(-15);
        two.GetComponent<ActivePlayer>().StartPoint = onePos;
        one.GetComponent<ActivePlayer>().StartPoint = twoPos;
        animOne.SetBool("fly", false);
        animTwo.SetBool("fly", false);
        GameA.input = true;
        e.check();
    }


}
