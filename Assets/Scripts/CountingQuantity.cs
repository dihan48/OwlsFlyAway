using UnityEngine;
using UnityEngine.UI;

public class CountingQuantity : MonoBehaviour
{
    public static int count;
    public Text txt;
    public TextMesh textMesh;
    public SaveLoadManager slm;
    string stxt;
    string stextMesh;
    int pCount;

    void Start()
    {
        count = 0;
        textMesh = GetComponent<TextMesh>();
        stextMesh = textMesh.text;
        textMesh.text = stextMesh + count;

        pCount = count;
    }

    void Update()
    {
        if (pCount != count)
        {
            textMesh.text = stextMesh + count;
            pCount = count;
            slm.SaveGame();
        }
    }
}
