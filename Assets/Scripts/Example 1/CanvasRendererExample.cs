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
        Debug.Log($"Bileþen boyutu: {_rectangle.GetComponent<RectTransform>().sizeDelta}");
        Debug.Log(Vector3.Distance(_rectStart.anchoredPosition, _rectEnd.anchoredPosition));
        //rectangleImage.SetNativeSize();
        //GetDistance(_rectTransform);
    }

    //Dikdörtgen oluþtur.
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
        // Ýki resim arasýndaki mesafeyi hesapla
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

        //Vertex(çoðul: vertices), bir 3 boyutlu modelin köþelerini veya noktalarýný temsil eden matematiksel bir terimdir. Bu köþeler, üç boyutlu bir nesnenin þeklini ve yapýsýný tanýmlayan temel unsurlardýr.
        //Vertex'ler, üç boyutlu bir nesnenin herhangi bir noktasýný temsil eder. Bir vertex'in konumu, üç boyutlu bir koordinat sistemi içinde belirtilir ve genellikle(x, y, z) koordinatlarý ile ifade edilir. Örneðin, bir dikdörtgenin köþe noktalarý, her biri bir vertex'i temsil eder.
        //Üç boyutlu grafikte, vertex'lerin bir araya gelmesiyle üçgenler, dörtgenler ve diðer çokgenler gibi daha karmaþýk þekiller oluþturulur. Bu þekiller, vertex'lerin birbirine baðlanmasýyla ve belirli bir düzene göre dizilmesiyle oluþturulur. Örneðin, bir üçgen üç vertex'in birleþimiyle oluþurken, bir dikdörtgen dört vertex'in birleþimiyle oluþur.
        //Bu baðlamda, vertex'ler, üç boyutlu modelleme ve grafik programlarýnda önemli bir role sahiptir. 3D modelleme iþlemlerinde, vertex'lerin manipüle edilmesi ve düzenlenmesi, nesnenin þeklinin ve görünümünün deðiþtirilmesine olanak tanýr.
        // Köþelerin konumlarý
        vertices[0] = new Vector3(0, 0);//(0,0)
        //Debug.Log($"vertices[0]: {vertices[0]}");
        vertices[1] = new Vector3(0, 100);//(0,1)
        //Debug.Log($"vertices[1]: {vertices[1]}");
        vertices[2] = new Vector3(endPos.anchoredPosition.x + 100, endPos.anchoredPosition.y + 100);//(1,1)
        //Debug.Log($"vertices[2]: {vertices[2]}");
        vertices[3] = new Vector3(endPos.anchoredPosition.x + 100, endPos.anchoredPosition.y);//(1,0)
        //Debug.Log($"vertices[3]: {vertices[3]}");

        //UV'ler (texture coordinates), bir mesh'in (að) yüzeyindeki her noktanýn bir texture üzerindeki konumunu belirten koordinat çiftleridir. UV koordinatlarý, 2 boyutlu bir düzlemde (genellikle 0 ile 1 arasýnda) belirtilir ve her bir koordinat, texture'nin belirli bir noktasýna karþýlýk gelir.

        //  UV koordinatlarý, bir mesh'in texture'sinin nasýl yerleþtirileceðini belirler ve texture'nin mesh üzerinde nasýl görüneceðini kontrol etmek için kullanýlýr. UV koordinatlarý, texture'nin her bir pikselinin mesh'in yüzeyine nasýl eþleþtirileceðini tanýmlar. Örneðin, (0, 0) UV koordinatý, texture'nin sol üst köþesine karþýlýk gelirken, (1, 1) UV koordinatý, texture'nin sað alt köþesine karþýlýk gelir.

        //UV koordinatlarý, mesh oluþturulurken veya düzenlenirken belirlenir. Her bir vertex(köþe) noktasý için bir UV koordinatý atanýr ve bu koordinatlar, mesh'in texture'sini nasýl iþleyeceðini belirler.

        //Kodunuzdaki uv dizisi, dikdörtgenin her bir köþesi için bir UV koordinatý belirtir.Bu koordinatlar, texture'nin dikdörtgenin köþelerine nasýl yerleþtirileceðini belirler. Örneðin, sol üst köþe için (0, 0) UV koordinatý kullanýlabilirken, sað üst köþe için (1, 0) UV koordinatý kullanýlabilir. Bu, texture'nin dikdörtgenin yüzeyine doðru þekilde yerleþtirilmesini saðlar.
        // Köþelerin UV koordinatlarý
        uv[0] = new Vector2(0, 0);
        uv[1] = new Vector2(0, 1);
        uv[2] = new Vector2(1, 1);
        uv[3] = new Vector2(1, 0);

        // Üçgenlerin indeksleri
        //ilk Üçgen
        triangles[0] = 0;
        triangles[1] = 1;
        triangles[2] = 2;

        //Ýkinci Üçgen
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
