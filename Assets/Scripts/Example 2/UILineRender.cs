using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class UILineRender : Graphic
{
    public Vector2[] points;
    public RectTransform[] _recTransform = new RectTransform[2];
    public float thickness = 10f;
    public bool center = true;
    [SerializeField] private int width = 20;
    public int Width { get => width; set => width = value; }
    public Vector2 pos1 { get => pos; set => pos = value; }
    private Vector2 pos;
    private VertexHelper vertexHelper;
    //"VertexHelper", Unity'de bir UI öðesinin (User Interface - Kullanýcý Arayüzü) grafiksel içeriðini oluþturmak, düzenlemek ve optimize etmek için kullanýlan bir yardýmcý sýnýftýr. Bu sýnýf, bir UI öðesinin vertex'lerini (noktalarýný), üçgenlerini ve diðer grafik bileþenlerini doðrudan manipüle etmeyi saðlar. "VertexHelper" genellikle bir "MaskableGraphic" alt sýnýfýnda (örneðin, bir "Image" veya "Text" öðesi) kullanýlýr.
    protected override void OnPopulateMesh(VertexHelper vh)
    {
        base.OnPopulateMesh(vh);
        vh.Clear();
        pos1 = _recTransform[1].anchoredPosition;
        float width = rectTransform.rect.width;
        float height = rectTransform.rect.height;
        Debug.Log(pos1);


        SetMesh(vh);
        //vh.AddUIVertexQuad(vertexs);




        //"UIVertex.simpleVert", Unity'de bir UI öðesinin grafiksel içeriðini temsil etmek için kullanýlan bir yapýdýr
        // position: Vertex'in dünya uzayýndaki konumu.
        //color: Vertex'in renk bilgisi (RGBA).
        //normal: Vertex'in normal vektörü (3 boyutlu).
        //tangent: Vertex'in tanjant vektörü (3 boyutlu).
        //uv0: Vertex'in ilk UV koordinatý.
        //uv1: Vertex'in ikinci UV koordinatý.
        //uv2: Vertex'in üçüncü UV koordinatý.
        //uv3: Vertex'in dördüncü UV koordinatý.


    }

    public void SetMesh(VertexHelper vh)
    {

        UIVertex vertex1 = UIVertex.simpleVert;
        vertex1.color = Color.white;
        vertex1.position = new Vector3(_recTransform[0].anchoredPosition.x, 0);
        vh.AddVert(vertex1);
        //vertexs[0] = vertex;
        vertex1.position = new Vector3(_recTransform[0].anchoredPosition.x, thickness);
        vh.AddVert(vertex1);
        //vertexs[1] = vertex;
        vertex1.position = new Vector3(_recTransform[1].anchoredPosition.x, thickness);
        vh.AddVert(vertex1);
        //vertexs[2] = vertex;
        vertex1.position = new Vector3(_recTransform[1].anchoredPosition.x, 0);
        vh.AddVert(vertex1);
        vertexHelper = vh;
        DrawShape(vh);
        //vh.AddUIVertexQuad(0,1,2,3);

    }
    public void RotateLine(float angle)
    {
        for (int i = 0; i < points.Length; i++)
        {
            // Baþlangýç ve bitiþ noktalarýný belirli bir pivot etrafýnda döndür
            points[i] = RotatePointAroundPivot(points[i], _recTransform[1].anchoredPosition, angle);
        }
        //SetVerticesDirty(); // Deðiþiklikleri UI'ye bildir
    }

    // Bir noktayý belirli bir pivot etrafýnda belirli bir açýyla döndürmek için yardýmcý fonksiyon
    private Vector2 RotatePointAroundPivot(Vector2 point, Vector2 pivot, float angle)
    {
        angle *= Mathf.Deg2Rad;
        float cos = Mathf.Cos(angle);
        float sin = Mathf.Sin(angle);
        float x = cos * (point.x - pivot.x) - sin * (point.y - pivot.y) + pivot.x;
        float y = sin * (point.x - pivot.x) + cos * (point.y - pivot.y) + pivot.y;
        return new Vector2(x, y);
    }
    //public void RotateLine(float angle)
    //{
    //    transform.rotation = Quaternion.Euler(0, 0, angle);
    //}
    private void DrawShape(VertexHelper vh)
    {
        vh.AddTriangle(0, 1, 3);
        vh.AddTriangle(1, 2, 3);
    }


    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Q))
        //{
        //    RotateLine(45f); // 45 derece saat yönünde dön
        //}

        //// Örneðin, "E" tuþuna basýldýðýnda çizgiyi saat yönünün tersine döndür
        //if (Input.GetKeyDown(KeyCode.E))
        //{
        //    RotateLine(-45f); // 45 derece saat yönünün tersine dön
        //}

        Vector2 pos1 = _recTransform[0].anchoredPosition;
        Vector2 pos2 = _recTransform[1].anchoredPosition;
        points = new Vector2[] { pos1, pos2 };
        //RotateLine(Mathf.Atan(_recTransform[1].anchoredPosition.y/ _recTransform[0].anchoredPosition.y));
        SetVerticesDirty(); // Deðiþiklikleri UI'ye bildir

    }


}
