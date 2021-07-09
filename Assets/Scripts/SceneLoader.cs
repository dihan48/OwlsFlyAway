using System;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private string scene;
    private void Start()
    {
        StartCoroutine(LoadAsyncScene());
    }

    IEnumerator LoadAsyncScene()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scene);

        if(asyncLoad != null)
        {
            while (!asyncLoad.isDone)
            {
                yield return null;
                transform.Rotate(0, 0, -180 * Time.deltaTime);
            }
        }
        else
        {
            throw new NullReferenceException("Сцена не найдена, возможно не верно указано имя сцены");
        }
    }
}
