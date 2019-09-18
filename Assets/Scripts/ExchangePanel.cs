using UnityEngine;
using UnityEngine.EventSystems;

public class ExchangePanel : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public GameObject One;
    public GameObject Two;
    public ExchangeButton exchangeButton;
    public int dragCount = 0;
    LineRenderer line;

    void Start()
    {
        line = gameObject.GetComponent<LineRenderer>();
        line.positionCount = 2;
    }
    Vector2 ScreenPos()
    {
        if (Input.touchCount > 0)
        {
            return Input.GetTouch(0).position;
        }
        else
        {
            return Input.mousePosition;
        }
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 mousePos;
        if (One != null)
        {
            Two = null;
            mousePos = Camera.main.ScreenToWorldPoint(ScreenPos());
            mousePos.z = 0;
            line.SetPosition(1, mousePos);
        }

    }

    public void OnEndDrag(PointerEventData eventData)
    {

        if (GameA.input && ExchangeButton.active && One != null)
        {
            line.SetPosition(0, Vector3.zero);
            line.SetPosition(1, Vector3.zero);
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
            RaycastHit2D raycastHit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            if (raycastHit.collider != null && raycastHit.collider.gameObject.GetComponent<ActivePlayer>() != null && One != raycastHit.collider.gameObject)
            {
                Two = raycastHit.transform.gameObject;
                exchangeButton.ExchangeCharacters(One, Two);
            }

        }
        if (One == null || Two == null)
        {
            exchangeButton.OffExchange();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        One = null;
        if (GameA.input && ExchangeButton.active)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(ScreenPos());
            mousePos.z = 0;
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
            RaycastHit2D raycastHit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            if (raycastHit.collider != null && raycastHit.collider.gameObject.GetComponent<ActivePlayer>() != null)
            {
                One = raycastHit.collider.gameObject;
                mousePos.z = 0;
                line.SetPosition(0, mousePos);
                line.SetPosition(1, mousePos);
            }
        }
    }
}
