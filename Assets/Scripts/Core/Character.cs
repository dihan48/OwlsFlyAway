using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour, IMatchable
{
    [SerializeField] private GameObject SelectedFX;
    [SerializeField] private float speedFlyAway = 5f;
    [SerializeField] private float speedScale = 3f;
    [SerializeField] private float speedMovement = 10f;
    [SerializeField] private List<MatchType> matches;

    public int PrefabIndex { get; private set; }

    public event Action<Character> OnSpawned;
    public event Action<Character> OnDestroyed;
    public event Action<Character, Vector2Int> OnMoved;

    private Animator anim;
    private IEnumerator winkCoroutine;

    public List<MatchType> GetMatchables()
    {
        return new List<MatchType>(matches);
    }

    public static Character Instantiate(Character prefab, int prefabIndex, Transform parent, Vector3 position)
    {
        Character character = Instantiate(prefab, parent).GetComponent<Character>()
            ?? throw new NullReferenceException("Не правильный префаб персонажа!");
        character.Init(position, prefabIndex);
        return character;
    }

    public void DestroyWithEffect(bool scale = false, bool flyAway = false)
    {
        float timeout = 0;

        if(scale && flyAway)
        {
            timeout = 1 / speedFlyAway;
            StartCoroutine(FlyAwayEffect());
            StartCoroutine(ScaleEffect(false, speedFlyAway * 1.1f));
        }
        else if(scale)
        {
            timeout = 1 / speedScale;
            StartCoroutine(ScaleEffect(false, speedScale));
        }
        else if (flyAway)
        {
            timeout = 1 / speedFlyAway;
            StartCoroutine(FlyAwayEffect());
        }

        StartCoroutine(DestroyWithTimeout(timeout));
    }

    public void StartMove(List<Vector2Int> path)
    {
        StartCoroutine(Move(path));
    }

    public void Select()
    {
        if (!anim.GetBool("action"))
        {
            anim.SetBool("action", true);
        }

        SelectedFX.SetActive(true);
    }

    public void Unselect()
    {
        anim.SetBool("action", false);
        anim.SetBool("fly", false);
        SelectedFX.SetActive(false);
    }

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Init(Vector3 position, int prefabIndex)
    {
        PrefabIndex = prefabIndex;
        transform.position = position;
        transform.localScale = Vector3.zero;
        gameObject.SetActive(true);
        StartCoroutine(Spawn());
        winkCoroutine = WinkEffect();
        StartCoroutine(winkCoroutine);
    }

    private void UpOrder()
    {
        int maxOrder = 0;
        var allSpriteRenderers = GetComponentsInChildren<SpriteRenderer>();

        foreach (var item in allSpriteRenderers)
        {
            maxOrder = item.sortingOrder > maxOrder ? item.sortingOrder : maxOrder;
        }

        foreach (var item in allSpriteRenderers)
        {
            item.sortingOrder += maxOrder;
        }
    }

    private IEnumerator Spawn()
    {
        yield return ScaleEffect(true, speedScale);

        OnSpawned?.Invoke(this);
    }

    private IEnumerator Move(List<Vector2Int> path)
    {
        if (!anim.GetBool("fly"))
        {
            anim.SetBool("fly", true);
        }

        var pathCount = path.Count - 1;
        var startPosition = transform.localPosition;
        var targetPosition = new Vector3(path[pathCount].x, path[pathCount].y, 0);
        var time = 0f;

        while (pathCount >= 0)
        {
            time += Time.deltaTime;

            if (time > 1 / speedMovement)
            {
                time = 0f;
                pathCount--;
                if (pathCount == -1)
                {
                    break;
                }

                startPosition = transform.localPosition;
            }
            else
            {
                targetPosition = new Vector3(path[pathCount].x, path[pathCount].y, 0);
                transform.localPosition = Vector3.Lerp(startPosition, targetPosition, speedMovement * time);
            }

            yield return null;
        }

        transform.localPosition = targetPosition;

        OnMoved?.Invoke(this, path[0]);
    }

    private IEnumerator DestroyWithTimeout(float secondTimeout)
    {
        UpOrder();
        yield return new WaitForSeconds(secondTimeout);

        Destroy(gameObject);
        OnDestroyed?.Invoke(this);
    }

    private IEnumerator ScaleEffect(bool isUp, float speed)
    {
        var scaleTo = isUp ? Vector3.one : Vector3.zero;
        Vector3 scale = transform.localScale;
        float time = 0;

        while (time < 1 / speed)
        {
            time += Time.deltaTime;
            transform.localScale = Vector3.Lerp(scale, scaleTo, speed * time);

            yield return null;
        }

        transform.localScale = scaleTo;
    }

    private IEnumerator FlyAwayEffect()
    {
        anim.SetBool("flyAway", true);

        float time = 0;
        Vector2 direction = UnityEngine.Random.onUnitSphere;

        while (time < 1 / speedFlyAway)
        {
            time += Time.deltaTime;
            transform.Translate(direction * speedFlyAway * 10 * Time.deltaTime);
            yield return null;
        }
    }

    private IEnumerator WinkEffect()
    {
        yield return new WaitForSeconds(UnityEngine.Random.Range(5f, 300f));
        anim.SetBool("wink", true);
        yield return new WaitForSeconds(1);
        anim.SetBool("wink", false);
        StartCoroutine(winkCoroutine);
    }

}
