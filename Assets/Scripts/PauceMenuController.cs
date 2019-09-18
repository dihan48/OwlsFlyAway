using UnityEngine;

public class PauceMenuController : MonoBehaviour
{
    public GameObject paucePanel;
    public GameObject pauceButton;
    public GameObject blurLayer;
    public GameObject exchangeButton;

    public void IsActive(bool active)
    {
        paucePanel.SetActive(active);
        pauceButton.SetActive(!active);
        exchangeButton.SetActive(!active);
        if (active)
            blurLayer.GetComponent<SpriteRenderer>().sortingOrder = 11;
        else
            blurLayer.GetComponent<SpriteRenderer>().sortingOrder = -101;
    }
}
