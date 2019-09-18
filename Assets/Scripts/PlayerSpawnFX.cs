using System.Collections;
using UnityEngine;

public class PlayerSpawnFX : MonoBehaviour
{
    float multiply;
    Coroutine scale;
    Coroutine destroyCoroutine;
    Coroutine WinkCoroutine;
    Animator anim;

    public GameObject target;

    void Start()
    {
        transform.localScale = new Vector3(0f, 0f, 0f);
        scale = StartCoroutine(Scale());
        anim = GetComponent<Animator>();
        WinkCoroutine = StartCoroutine(Wink());
    }

    IEnumerator Scale()
    {
        GameA.input = false;
        while (transform.localScale != new Vector3(1, 1, 1))
        {
            multiply = 3 * Time.deltaTime;
            transform.localScale += new Vector3(multiply, multiply, multiply);

            if (transform.localScale.x > 1)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            yield return null;
        }
        StopCoroutine(scale);
        GameA.input = true;
    }



    public void DestroyPlayer()
    {
        destroyCoroutine = StartCoroutine(DestroyCoroutine());

    }

    IEnumerator DestroyCoroutine()
    {
        GameA.input = false;
        Vector3 currentPos = transform.position;

        float t = 0f;
        float timeToMove = 1f;
        while (transform.localScale != new Vector3(0, 0, 0))
        {
            multiply = 1.5f * Time.deltaTime;
            transform.localScale -= new Vector3(multiply, multiply, multiply);
            t += Time.deltaTime / timeToMove;
            transform.position = Vector3.Lerp(currentPos, target.transform.position, t);

            if (transform.localScale.x < 0)
            {
                transform.localScale = new Vector3(0, 0, 0);
            }
            anim.SetBool("fly", true);
            yield return null;
        }

        Destroy(gameObject);
        GameA.input = true;
        CountingQuantity.count++;
    }

    IEnumerator Wink()
    {
        yield return new WaitForSeconds(Random.Range(5f, 300f));
        anim.SetBool("wink", true);
        yield return new WaitForSeconds(1);
        anim.SetBool("wink", false);
        StopCoroutine(WinkCoroutine);
        WinkCoroutine = StartCoroutine(Wink());
    }
}
