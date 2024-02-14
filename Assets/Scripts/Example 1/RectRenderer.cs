using UnityEngine;
using UnityEngine.UI;

public class RectRenderer : MonoBehaviour
{
    RectTransform rectTransform;
    Image rectImage;
    CanvasRenderer canvasRenderer;

    void Start()
    {
        // Dikdörtgen oluþturmak için bir UI elemaný oluþturun
        GameObject rectObject = new GameObject("Rectangle");
        rectTransform = rectObject.AddComponent<RectTransform>();
        rectTransform.SetParent(transform, false); // Parent olarak bu objeyi belirtin

        // Image bileþenini ekleyin ve rengini ayarlayýn
        rectImage = rectObject.AddComponent<Image>();
        rectImage.color = Color.green; // Dikdörtgenin rengi

        // CanvasRenderer bileþenini alýn ve dikdörtgeni çizmek için kullanýn
        canvasRenderer = rectObject.AddComponent<CanvasRenderer>();
        canvasRenderer.SetMaterial(Graphic.defaultGraphicMaterial, null); // Materyali varsayýlan olarak ayarlayýn
    }

    void Update()
    {
        // Mouse pozisyonuna göre dikdörtgenin uzunluðunu güncelle
        Vector2 mousePos = Input.mousePosition;
        Vector2 localMousePos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, mousePos, null, out localMousePos);
        float newWidth = Mathf.Abs(localMousePos.x - rectTransform.localPosition.x) * 2f;
        rectTransform.sizeDelta = new Vector2(newWidth, rectTransform.sizeDelta.y);

        // CanvasRenderer'da çizim iþlemi için boyutlarý ve rengi ayarla
        canvasRenderer.SetColor(rectImage.color);
        canvasRenderer.SetMesh(CreateRectMesh(rectTransform.rect));
    }

    // Mesh oluþturmak için yardýmcý bir fonksiyon
    Mesh CreateRectMesh(Rect rect)
    {
        Mesh mesh = new Mesh();
        Vector3[] vertices = new Vector3[4];
        Vector2[] uv = new Vector2[4];
        int[] triangles = new int[6];

        // Köþelerin konumlarý
        vertices[0] = new Vector3(rect.x, rect.y);
        vertices[1] = new Vector3(rect.x, rect.y + rect.height);
        vertices[2] = new Vector3(rect.x + rect.width, rect.y + rect.height);
        vertices[3] = new Vector3(rect.x + rect.width, rect.y);

        // Köþelerin UV koordinatlarý
        uv[0] = new Vector2(0, 0);
        uv[1] = new Vector2(0, 1);
        uv[2] = new Vector2(1, 1);
        uv[3] = new Vector2(1, 0);

        // Üçgenlerin indeksleri
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
