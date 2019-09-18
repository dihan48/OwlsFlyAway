using UnityEngine;

public class TestTrigger : MonoBehaviour
{
    public bool Trigger { set; get; }
    void Update()
    {
        if (Trigger)
        {
            Trigger = !Trigger;
        }
    }
}
