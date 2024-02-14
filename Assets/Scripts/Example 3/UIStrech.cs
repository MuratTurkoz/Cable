using UnityEngine;
using UnityEngine.EventSystems;

//using static UnityEngine.GraphicsBuffer;
//using UnityEngine.UIElements;

public class UIStrech : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [SerializeField] private RectTransform _startImage;
    [SerializeField] private RectTransform _cableImage;

    [SerializeField] private float _rotationSpeed = 1f; // D�nme h�z�


    private float _angle;
    private Vector2 _startPos;
    private Vector2 _endPos;
    // Fare hareketlerine g�re d�nme miktar�n� hesapla


    public void OnBeginDrag(PointerEventData eventData)
    {

    }

    public void OnDrag(PointerEventData eventData)
    {
        GetAngle();
        GetDistance();
        transform.position = eventData.position;

    }
    private void GetPos()
    {
        //ilk Pos al
        _startPos = _startImage.anchoredPosition;
        //Son Pos al
        _endPos = GetComponent<RectTransform>().anchoredPosition;

    }
    private float RotatePointTowards(float y, float x)
    {
        //A��y� hesapla
        return (float)(Mathf.Atan2(y, x) * (180 / Mathf.PI));
    }


    public void OnEndDrag(PointerEventData eventData)
    {
        _startPos = Vector2.zero;
        _endPos = Vector2.zero;
    }
    private void GetDistance()
    {
        //De�erleri al
        GetPos();

        // �ki resim aras�ndaki mesafeyi hesapla
        float distance = Vector3.Distance(_startPos, _endPos);
        _cableImage.sizeDelta = new Vector2(Mathf.RoundToInt(distance), _cableImage.sizeDelta.y);


    }
    private void GetAngle()
    {
        //De�erleri al
        GetPos();

        Vector3 directionVector = _endPos - _startPos;
        Debug.Log(_angle);
        //Birim vekt�r� olu�tur.
        directionVector.Normalize();
        //A��y� hesapla
        _angle = RotatePointTowards(directionVector.y, directionVector.x);
        _cableImage.transform.rotation = Quaternion.Euler(0, 0, _angle*_rotationSpeed);

    }
}
