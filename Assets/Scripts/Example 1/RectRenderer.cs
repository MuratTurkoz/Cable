using UnityEngine;
using UnityEngine.UI;

public class RectRenderer : MonoBehaviour
{
    RectTransform rectTransform;
    Image rectImage;
    CanvasRenderer canvasRenderer;

    void Start()
    {
        // Dikd�rtgen olu�turmak i�in bir UI eleman� olu�turun
        GameObject rectObject = new GameObject("Rectangle");
        rectTransform = rectObject.AddComponent<RectTransform>();
        rectTransform.SetParent(transform, false); // Parent olarak bu objeyi belirtin

        // Image bile�enini ekleyin ve rengini ayarlay�n
        rectImage = rectObject.AddComponent<Image>();
        rectImage.color = Color.green; // Dikd�rtgenin rengi

        // CanvasRenderer bile�enini al�n ve dikd�rtgeni �izmek i�in kullan�n
        canvasRenderer = rectObject.AddComponent<CanvasRenderer>();
        canvasRenderer.SetMaterial(Graphic.defaultGraphicMaterial, null); // Materyali varsay�lan olarak ayarlay�n
    }

    void Update()
    {
        // Mouse pozisyonuna g�re dikd�rtgenin uzunlu�unu g�ncelle
        Vector2 mousePos = Input.mousePosition;
        Vector2 localMousePos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, mousePos, null, out localMousePos);
        float newWidth = Mathf.Abs(localMousePos.x - rectTransform.localPosition.x) * 2f;
        rectTransform.sizeDelta = new Vector2(newWidth, rectTransform.sizeDelta.y);

        // CanvasRenderer'da �izim i�lemi i�in boyutlar� ve rengi ayarla
        canvasRenderer.SetColor(rectImage.color);
        canvasRenderer.SetMesh(CreateRectMesh(rectTransform.rect));
    }

    // Mesh olu�turmak i�in yard�mc� bir fonksiyon
    Mesh CreateRectMesh(Rect rect)
    {
        Mesh mesh = new Mesh();
        Vector3[] vertices = new Vector3[4];
        Vector2[] uv = new Vector2[4];
        int[] triangles = new int[6];

        // K��elerin konumlar�
        vertices[0] = new Vector3(rect.x, rect.y);
        vertices[1] = new Vector3(rect.x, rect.y + rect.height);
        vertices[2] = new Vector3(rect.x + rect.width, rect.y + rect.height);
        vertices[3] = new Vector3(rect.x + rect.width, rect.y);

        // K��elerin UV koordinatlar�
        uv[0] = new Vector2(0, 0);
        uv[1] = new Vector2(0, 1);
        uv[2] = new Vector2(1, 1);
        uv[3] = new Vector2(1, 0);

        // ��genlerin indeksleri
        triangles[0] = 0;
        triangles[1] = 1;
        triangles[2] = 2;
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
