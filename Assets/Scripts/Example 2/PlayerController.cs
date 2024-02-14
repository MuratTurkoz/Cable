using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    //private Canvas canvas;
    //private CanvasGroup canvasGroup;
    private UILineRender _lineRender;
    private Vector2 pointerOffset;
    public Image image1;
    public Image image2;

    void Start()
    {

        GameObject.Find("Grid").TryGetComponent(out _lineRender);
        rectTransform = GetComponent<RectTransform>();
        //canvas = GetComponentInParent<Canvas>();
        //canvasGroup = GetComponent<CanvasGroup>();
    }
    int GetDistance()
    {
        // Ýlk resmin köþe noktalarýný al
        RectTransform rectTransform1 = image1.GetComponent<RectTransform>();
        Vector2 val1 = rectTransform1.anchoredPosition;
        //Vector3[] corners1 = new Vector3[4];
        //rectTransform1.GetWorldCorners(corners1);

        // Ýkinci resmin köþe noktalarýný al
        RectTransform rectTransform2 = image2.GetComponent<RectTransform>();
        Vector2 val2 = rectTransform2.anchoredPosition;
        //Vector3[] corners2 = new Vector3[4];
        //rectTransform2.GetWorldCorners(corners2);

        // Ýki resim arasýndaki mesafeyi hesapla
        float distance = Vector3.Distance(val1, val2);
        //float distance = Vector3.Distance(corners1[0], corners2[0]);
        return Mathf.RoundToInt(distance);
        //Debug.Log("Distance between images: " + distance);
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        _lineRender.Width = GetDistance();
        //Vector2 localPointerPosition;
        //RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, eventData.position, eventData.pressEventCamera, out localPointerPosition);
        //pointerOffset = rectTransform.anchoredPosition - localPointerPosition;
        //canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        _lineRender.Width = GetDistance();
        //Vector2 localPointerPosition;
        //if (RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, eventData.position, eventData.pressEventCamera, out localPointerPosition))
        //{
        //    Debug.Log(localPointerPosition);
        //    //transform.position = localPointerPosition;
        //    rectTransform.anchoredPosition = localPointerPosition;
        //}
        //Debug.Log("Pointer Drag:"+eventData.pointerDrag);
        //Debug.Log("Pointer Pos:"+eventData.position);
        //Debug.Log(RectTransformUtility.scr.ScreenPointToLocalPointInRectangle(eventData.position));
        transform.position = eventData.position;
        //_lineRender.pos1 = eventData.position;
        //Vector2 localPointerPosition;
        //if (RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, eventData.position, eventData.pressEventCamera, out localPointerPosition))
        //{
        //    rectTransform.anchoredPosition = localPointerPosition + pointerOffset;
        //}
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //canvasGroup.blocksRaycasts = true;
    }
}
