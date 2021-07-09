using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField] private TextMesh textMesh;
    public int Value { get; private set; } = 0;
    
    public void Add(int value)
    {
        if(value > 0)
        {
            Value += value;
        }

        textMesh.text = Value.ToString();
    }

    public void Reset()
    {
        Value = 0;
        textMesh.text = Value.ToString();
    }

    private void Start()
    {
        Reset();
    }
}
