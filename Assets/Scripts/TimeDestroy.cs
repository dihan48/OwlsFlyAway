using System.Collections;
using UnityEngine;

public class TimeDestroy : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(DestroyCoroutine());
    }

    IEnumerator DestroyCoroutine()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }
}
