using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour ,IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private Canvas canvas;
    private CanvasGroup canvasGroup;

    private Vector2 pointerOffset;
    private bool dragging = false;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Vector2 localPointerPosition;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, eventData.position, eventData.pressEventCamera, out localPointerPosition))
        {
            pointerOffset = rectTransform.anchoredPosition - localPointerPosition;
            dragging = true;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (dragging)
        {
            Vector2 localPointerPosition;
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, eventData.position, eventData.pressEventCamera, out localPointerPosition))
            {
                rectTransform.anchoredPosition = localPointerPosition + pointerOffset;
            }
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        dragging = false;
    }
}
