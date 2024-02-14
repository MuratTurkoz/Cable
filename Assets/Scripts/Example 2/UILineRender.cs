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
    //"VertexHelper", Unity'de bir UI ��esinin (User Interface - Kullan�c� Aray�z�) grafiksel i�eri�ini olu�turmak, d�zenlemek ve optimize etmek i�in kullan�lan bir yard�mc� s�n�ft�r. Bu s�n�f, bir UI ��esinin vertex'lerini (noktalar�n�), ��genlerini ve di�er grafik bile�enlerini do�rudan manip�le etmeyi sa�lar. "VertexHelper" genellikle bir "MaskableGraphic" alt s�n�f�nda (�rne�in, bir "Image" veya "Text" ��esi) kullan�l�r.
    protected override void OnPopulateMesh(VertexHelper vh)
    {
        vh.Clear();
        base.OnPopulateMesh(vh);
        pos1 = _recTransform[1].anchoredPosition;
        float width = rectTransform.rect.width;
        float height = rectTransform.rect.height;
        Debug.Log(pos1);


        SetMesh(vh);
        //vh.AddUIVertexQuad(vertexs);




        //"UIVertex.simpleVert", Unity'de bir UI ��esinin grafiksel i�eri�ini temsil etmek i�in kullan�lan bir yap�d�r
        // position: Vertex'in d�nya uzay�ndaki konumu.
        //color: Vertex'in renk bilgisi (RGBA).
        //normal: Vertex'in normal vekt�r� (3 boyutlu).
        //tangent: Vertex'in tanjant vekt�r� (3 boyutlu).
        //uv0: Vertex'in ilk UV koordinat�.
        //uv1: Vertex'in ikinci UV koordinat�.
        //uv2: Vertex'in ���nc� UV koordinat�.
        //uv3: Vertex'in d�rd�nc� UV koordinat�.


    }

    public void SetMesh(VertexHelper vh)
    {

        UIVertex vertex1 = UIVertex.simpleVert;
        vertex1.color = Color.white;
        Quaternion point1Rotation = Quaternion.Euler(0, 0, RotatePointTowards(_recTransform[0].anchoredPosition, _recTransform[1].anchoredPosition) + 90);
        vertex1.position = point1Rotation * new Vector3(-thickness / 2, 0);
        vertex1.position = new Vector3(_recTransform[0].anchoredPosition.x, 0);
        vh.AddVert(vertex1);
        //vertexs[0] = vertex;
        vertex1.position = new Vector3(_recTransform[0].anchoredPosition.x, thickness);
        vh.AddVert(vertex1);
        Quaternion point2Rotation = Quaternion.Euler(0, 0, RotatePointTowards(_recTransform[0].anchoredPosition, _recTransform[1].anchoredPosition) - 90);
        vertex1.position = point2Rotation * new Vector3(-thickness / 2, 0);
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
    private float RotatePointTowards(Vector2 vertex, Vector2 target)
    {
        return (float)(Mathf.Atan2(target.y - vertex.y, target.x - vertex.x) * (180 / Mathf.PI));
    }
    private void DrawShape(VertexHelper vh)
    {
        //RotateLine(Mathf.Atan(_recTransform[1].anchoredPosition.y / _recTransform[0].anchoredPosition.y));
        vh.AddTriangle(0, 1, 3);
        vh.AddTriangle(1, 2, 3);
    }
    
    //public void RotateLine(float angle)
    //{
    //    transform.rotation = Quaternion.Euler(0, 0, angle);
    //}



    private void Update()
    {


        Vector2 pos1 = _recTransform[0].anchoredPosition;
        Vector2 pos2 = _recTransform[1].anchoredPosition;
        points = new Vector2[] { pos1, pos2 };
        SetVerticesDirty(); // De�i�iklikleri UI'ye bildir

    }


}
