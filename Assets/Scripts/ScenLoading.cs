using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScenLoading : MonoBehaviour
{
    public int SceneID;
    public Image loadingImg;
    public Text progressText;

    void Start()
    {
        StartCoroutine(AsyncLoad());
    }


    IEnumerator AsyncLoad()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(SceneID);
        while (!operation.isDone)
        {
            float progress = operation.progress / 0.9f;
            loadingImg.fillAmount = progress;
            progressText.text = string.Format("{0:0}%", progress * 100);
            yield return null;
        }
    }
}
