using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
 

    public void OnBeginDrag(PointerEventData eventData)
    {

    }

    public void OnDrag(PointerEventData eventData)
    {

        transform.position = eventData.position;

    }

    public void OnEndDrag(PointerEventData eventData)
    {

    }
}
