using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
//using static UnityEngine.GraphicsBuffer;
//using UnityEngine.UIElements;

public class UIStrech : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [SerializeField] private Image _cableImage;
    [SerializeField] private Image _startImage;
    Vector3 _newScale;
    RectTransform rectTransform;
    [SerializeField] private float rotationSpeed = 5f; // Dönme hýzý
    Rect _rect;

    public void OnBeginDrag(PointerEventData eventData)
    {
        //throw new System.NotImplementedException();
    }

    public void OnDrag(PointerEventData eventData)
    {

        float deltaX = eventData.delta.x;
        float deltaY = eventData.delta.y;

        // Fare hareketlerine göre dönme miktarýný hesapla
        float rotationAmount = (deltaX + deltaY) * rotationSpeed;

        RectTransform rectTransform1 = _startImage.GetComponent<RectTransform>();
        Vector2 val1 = rectTransform1.anchoredPosition;
        // Ýkinci resmin köþe noktalarýný al
        RectTransform rectTransform2 = GetComponent<RectTransform>();
        Vector2 val2 = rectTransform2.anchoredPosition;

        // Ýki nokta arasýndaki vektörü hesapla
        Vector3 directionVector = val2 - val1;

        // Yalnýzca z ekseni etrafýnda dönmek için vektörleri düzelt
        directionVector.x = 0;
        directionVector.y = 0;

        // Vektörün yönüne doðru dönme rotasyonunu oluþtur
        Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, directionVector);

        // Objenin dönme iþlemini gerçekleþtir
        _cableImage.transform.rotation = Quaternion.RotateTowards(_cableImage.transform.rotation, targetRotation, rotationAmount);

        // Objenin boyutunu güncelle
        _cableImage.GetComponent<RectTransform>().sizeDelta = new Vector2(GetDistance(), _cableImage.GetComponent<RectTransform>().sizeDelta.y);

        transform.position = eventData.position;

    }

    public void OnEndDrag(PointerEventData eventData)
    {

    }
    int GetDistance()
    {
        // Ýlk resmin köþe noktalarýný al
        RectTransform rectTransform1 = _startImage.GetComponent<RectTransform>();
        Vector2 val1 = rectTransform1.anchoredPosition;
        //Vector3[] corners1 = new Vector3[4];
        //rectTransform1.GetWorldCorners(corners1);

        // Ýkinci resmin köþe noktalarýný al
        RectTransform rectTransform2 = GetComponent<RectTransform>();
        Vector2 val2 = rectTransform2.anchoredPosition;
        //Vector3[] corners2 = new Vector3[4];
        //rectTransform2.GetWorldCorners(corners2);

        // Ýki resim arasýndaki mesafeyi hesapla
        float distance = Vector3.Distance(val1, val2);
        //float distance = Vector3.Distance(corners1[0], corners2[0]);
        return Mathf.RoundToInt(distance);
        //Debug.Log("Distance between images: " + distance);
    }
    private float GetAngle()
    {
        //Quaternion targetRotation = Quaternion.LookRotation(targetVector, Vector3.up);
        //transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * T);
        RectTransform rectTransform1 = _startImage.GetComponent<RectTransform>();
        Vector2 val1 = rectTransform1.anchoredPosition;
        //Vector3[] corners1 = new Vector3[4];
        //rectTransform1.GetWorldCorners(corners1);
        val1.Normalize();
        // Ýkinci resmin köþe noktalarýný al
        RectTransform rectTransform2 = GetComponent<RectTransform>();
        Vector2 val2 = rectTransform2.anchoredPosition;
        val2.Normalize();
        float angle = (float)(Mathf.Atan2((val2.y - val1.y), val2.x - val1.x) * (180 / Mathf.PI));
        return angle;
    }

    //private void Update()
    //{

    //}
}
