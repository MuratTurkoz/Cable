using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;
using UnityEngine.UI;

public class CanvasRendererExample : MonoBehaviour
{
    [SerializeField] private Canvas _canvas;
    [SerializeField] private Sprite _sprite;
    [SerializeField] private RectTransform _rectStart;
    [SerializeField] private RectTransform _rectEnd;
    [SerializeField] private Material _material;
    CanvasRenderer _canvasRenderer;
    RectTransform _rectTransform;
    GameObject _rectangle;
    Image rectangleImage;
    float distance;
    // Start is called before the first frame update
    void Start()
    {
        CreateUI();
    }

    // Update is called once per frame
    void Update()
    {
        _canvasRenderer.SetMesh(SetObjectMesh(_rectStart, _rectEnd));
        distance = Vector3.Distance(_rectStart.anchoredPosition, _rectEnd.anchoredPosition);
        Debug.Log($"Bile�en boyutu: {_rectangle.GetComponent<RectTransform>().sizeDelta}");
        Debug.Log(Vector3.Distance(_rectStart.anchoredPosition, _rectEnd.anchoredPosition));
        //rectangleImage.SetNativeSize();
        //GetDistance(_rectTransform);
    }

    //Dikd�rtgen olu�tur.
    private void CreateUI()
    {
        _rectangle = new GameObject();
        _rectangle.name = "Test";
        _rectTransform = _rectangle.AddComponent<RectTransform>();
        _rectTransform.parent = _canvas.transform;
        GetDistance(_rectTransform);
        _rectTransform.anchoredPosition = new Vector2(0, -_rectTransform.sizeDelta.y/2);
        _rectTransform.localRotation = Quaternion.identity;
        _rectTransform.localScale = Vector3.one;
        _rectTransform.pivot = Vector2.zero;
        rectangleImage = _rectangle.AddComponent<Image>();
       
        rectangleImage.sprite = _sprite;
        rectangleImage.material = _material;
        _canvasRenderer = _rectangle.GetComponent<CanvasRenderer>();

        _canvasRenderer.SetMesh(SetObjectMesh(_rectStart, _rectEnd));

        //_canvasRenderer.SetMaterial(_material, 0);
    }

    private void GetDistance(RectTransform rectTransform)
    {
        // �ki resim aras�ndaki mesafeyi hesapla
        //float distance = Vector3.Distance(_rectStart.anchoredPosition, _rectEnd.anchoredPosition);
        distance = Vector3.Distance(_rectStart.anchoredPosition, _rectEnd.anchoredPosition);
        rectTransform.sizeDelta = new Vector2(Mathf.RoundToInt(distance), rectTransform.sizeDelta.y);
    }
    private Mesh SetObjectMesh(RectTransform startPos, RectTransform endPos)
    {
        //GetDistance(_rectTransform);
        Mesh mesh = new Mesh();
        Vector3[] vertices = new Vector3[4];
        Vector2[] uv = new Vector2[4];
        int[] triangles = new int[6];

        //Vertex(�o�ul: vertices), bir 3 boyutlu modelin k��elerini veya noktalar�n� temsil eden matematiksel bir terimdir. Bu k��eler, �� boyutlu bir nesnenin �eklini ve yap�s�n� tan�mlayan temel unsurlard�r.
        //Vertex'ler, �� boyutlu bir nesnenin herhangi bir noktas�n� temsil eder. Bir vertex'in konumu, �� boyutlu bir koordinat sistemi i�inde belirtilir ve genellikle(x, y, z) koordinatlar� ile ifade edilir. �rne�in, bir dikd�rtgenin k��e noktalar�, her biri bir vertex'i temsil eder.
        //�� boyutlu grafikte, vertex'lerin bir araya gelmesiyle ��genler, d�rtgenler ve di�er �okgenler gibi daha karma��k �ekiller olu�turulur. Bu �ekiller, vertex'lerin birbirine ba�lanmas�yla ve belirli bir d�zene g�re dizilmesiyle olu�turulur. �rne�in, bir ��gen �� vertex'in birle�imiyle olu�urken, bir dikd�rtgen d�rt vertex'in birle�imiyle olu�ur.
        //Bu ba�lamda, vertex'ler, �� boyutlu modelleme ve grafik programlar�nda �nemli bir role sahiptir. 3D modelleme i�lemlerinde, vertex'lerin manip�le edilmesi ve d�zenlenmesi, nesnenin �eklinin ve g�r�n�m�n�n de�i�tirilmesine olanak tan�r.
        // K��elerin konumlar�
        vertices[0] = new Vector3(0, 0);//(0,0)
        //Debug.Log($"vertices[0]: {vertices[0]}");
        vertices[1] = new Vector3(0, 100);//(0,1)
        //Debug.Log($"vertices[1]: {vertices[1]}");
        vertices[2] = new Vector3(endPos.anchoredPosition.x + 100, endPos.anchoredPosition.y + 100);//(1,1)
        //Debug.Log($"vertices[2]: {vertices[2]}");
        vertices[3] = new Vector3(endPos.anchoredPosition.x + 100, endPos.anchoredPosition.y);//(1,0)
        //Debug.Log($"vertices[3]: {vertices[3]}");

        //UV'ler (texture coordinates), bir mesh'in (a�) y�zeyindeki her noktan�n bir texture �zerindeki konumunu belirten koordinat �iftleridir. UV koordinatlar�, 2 boyutlu bir d�zlemde (genellikle 0 ile 1 aras�nda) belirtilir ve her bir koordinat, texture'nin belirli bir noktas�na kar��l�k gelir.

        //  UV koordinatlar�, bir mesh'in texture'sinin nas�l yerle�tirilece�ini belirler ve texture'nin mesh �zerinde nas�l g�r�nece�ini kontrol etmek i�in kullan�l�r. UV koordinatlar�, texture'nin her bir pikselinin mesh'in y�zeyine nas�l e�le�tirilece�ini tan�mlar. �rne�in, (0, 0) UV koordinat�, texture'nin sol �st k��esine kar��l�k gelirken, (1, 1) UV koordinat�, texture'nin sa� alt k��esine kar��l�k gelir.

        //UV koordinatlar�, mesh olu�turulurken veya d�zenlenirken belirlenir. Her bir vertex(k��e) noktas� i�in bir UV koordinat� atan�r ve bu koordinatlar, mesh'in texture'sini nas�l i�leyece�ini belirler.

        //Kodunuzdaki uv dizisi, dikd�rtgenin her bir k��esi i�in bir UV koordinat� belirtir.Bu koordinatlar, texture'nin dikd�rtgenin k��elerine nas�l yerle�tirilece�ini belirler. �rne�in, sol �st k��e i�in (0, 0) UV koordinat� kullan�labilirken, sa� �st k��e i�in (1, 0) UV koordinat� kullan�labilir. Bu, texture'nin dikd�rtgenin y�zeyine do�ru �ekilde yerle�tirilmesini sa�lar.
        // K��elerin UV koordinatlar�
        uv[0] = new Vector2(0, 0);
        uv[1] = new Vector2(0, 1);
        uv[2] = new Vector2(1, 1);
        uv[3] = new Vector2(1, 0);

        // ��genlerin indeksleri
        //ilk ��gen
        triangles[0] = 0;
        triangles[1] = 1;
        triangles[2] = 2;

        //�kinci ��gen
        triangles[3] = 0;
        triangles[4] = 2;
        triangles[5] = 3;


        // Mesh'i ayarla
        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();

        return mesh;


    }

}
