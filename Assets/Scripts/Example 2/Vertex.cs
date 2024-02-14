using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Vertex : MonoBehaviour
{
    private Image image; // Inspector'dan atayaca��n�z Image bile�eni
    DrawLine drawLine;
    Vector3[] corners = new Vector3[4];
    private void Awake()
    {
        image = GetComponent<Image>();
    }
    void Start()
    {

        // Image bile�eninin RectTransform'i
        RectTransform rectTransform = image.rectTransform;

        // Image'in k��e noktalar�n� al
        Vector3[] corners = new Vector3[4];
        rectTransform.GetLocalCorners(corners);

        // K��e noktalar�n� d�ng�de kullanarak vertexleri al
        for (int i = 0; i < 4; i++)
        {
            Debug.Log("Vertex " + i + ": " + corners[i]);
        }
    }
    
    private void Update()
    {
        drawLine.SetVertex(corners);
    }
}
