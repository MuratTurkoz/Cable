using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILineRender : Graphic
{
    public Vector2[] points;
    public RectTransform[] _recTransform = new RectTransform[2];
    public float thickness = 10f;
    public bool center = true;
    //"VertexHelper", Unity'de bir UI öðesinin (User Interface - Kullanýcý Arayüzü) grafiksel içeriðini oluþturmak, düzenlemek ve optimize etmek için kullanýlan bir yardýmcý sýnýftýr. Bu sýnýf, bir UI öðesinin vertex'lerini (noktalarýný), üçgenlerini ve diðer grafik bileþenlerini doðrudan manipüle etmeyi saðlar. "VertexHelper" genellikle bir "MaskableGraphic" alt sýnýfýnda (örneðin, bir "Image" veya "Text" öðesi) kullanýlýr.
    protected override void OnPopulateMesh(VertexHelper vh)
    {
        vh.Clear();
        float width = rectTransform.rect.width;
        float height = rectTransform.rect.height;
        //UIVertex[] vertexs = new UIVertex[4];

        //UIVertex vertex = UIVertex.simpleVert;
        //vertex.color = Color.white;

        //float index0 = _recTransform[0].position.x;
        //float index1 = _recTransform[0].position.y;
        //float index2 = _recTransform[1].rect.width;
        //float index3 = _recTransform[1].rect.height;
        //vertex.position = new Vector3(index0, index1);
        //vh.AddVert(vertex);
        //vertexs[0] = vertex;
        //vertex.position = new Vector3(index2, index3);
        //vh.AddVert(vertex);
        //vertexs[1] = vertex;
        //vertex.position = new Vector3(index2, index3+50f);
        //vh.AddVert(vertex);
        //vertexs[2] = vertex;
        //vertex.position = new Vector3(index0+50f, index1);
        //vh.AddVert(vertex);
        //vertexs[3] = vertex;

        //vh.AddTriangle(0, 1, 2);


        UIVertex vertex1 = UIVertex.simpleVert;
        vertex1.color = Color.white;
        vertex1.position = new Vector3(0 , 0);
        vh.AddVert(vertex1);
        //vertexs[0] = vertex;
        vertex1.position = new Vector3(0, height);
        vh.AddVert(vertex1);
        //vertexs[1] = vertex;
        vertex1.position = new Vector3(width, height);
        vh.AddVert(vertex1);
        //vertexs[2] = vertex;
        vertex1.position = new Vector3(width, 0);
        vh.AddVert(vertex1);
        vh.AddTriangle(0, 1, 2);
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


        //if (_recTransform.Length < 2)
        //    return;

        //for (int i = 0; i < _recTransform.Length - 1; i++)
        //{
        //    Vector2 pos=new Vector2(_recTransform[i].position.x, _recTransform[i].position.y);
        //    Vector2 posNext= new Vector2(_recTransform[i+1].position.x, _recTransform[i].position.y);
        //    // Create a line segment between the next two points
        //    CreateLineSegment(pos, posNext, vh);

        //    int index = i * 5;

        //    // Add the line segment to the triangles array
        //    vh.AddTriangle(index, index + 1, index + 3);
        //    vh.AddTriangle(index + 3, index + 2, index);

        //    // These two triangles create the beveled edges
        //    // between line segments using the end point of
        //    // the last line segment and the start points of this one
        //    if (i != 0)
        //    {
        //        vh.AddTriangle(index, index - 1, index - 3);
        //        vh.AddTriangle(index + 1, index - 1, index - 2);
        //    }
        //}
    }

    /// <summary>
    /// Creates a rect from two points that acts as a line segment
    /// </summary>
    /// <param name="point1">The starting point of the segment</param>
    /// <param name="point2">The endint point of the segment</param>
    /// <param name="vh">The vertex helper that the segment is added to</param>
    private void CreateLineSegment(Vector3 point1, Vector3 point2, VertexHelper vh)
    {
        Vector3 offset = center ? (rectTransform.sizeDelta / 2) : Vector2.zero;

        // Create vertex template
        UIVertex vertex = UIVertex.simpleVert;
        vertex.color = color;

        // Create the start of the segment
        Quaternion point1Rotation = Quaternion.Euler(0, 0, RotatePointTowards(point1, point2) + 90);
        vertex.position = point1Rotation * new Vector3(-thickness / 2, 0);
        vertex.position += point1 - offset;
        vh.AddVert(vertex);
        vertex.position = point1Rotation * new Vector3(thickness / 2, 0);
        vertex.position += point1 - offset;
        vh.AddVert(vertex);

        // Create the end of the segment
        Quaternion point2Rotation = Quaternion.Euler(0, 0, RotatePointTowards(point2, point1) - 90);
        vertex.position = point2Rotation * new Vector3(-thickness / 2, 0);
        vertex.position += point2 - offset;
        vh.AddVert(vertex);
        vertex.position = point2Rotation * new Vector3(thickness / 2, 0);
        vertex.position += point2 - offset;
        vh.AddVert(vertex);

        // Also add the end point
        vertex.position = point2 - offset;
        vh.AddVert(vertex);
    }

    /// <summary>
    /// Gets the angle that a vertex needs to rotate to face target vertex
    /// </summary>
    /// <param name="vertex">The vertex being rotated</param>
    /// <param name="target">The vertex to rotate towards</param>
    /// <returns>The angle required to rotate vertex towards target</returns>
    private float RotatePointTowards(Vector2 vertex, Vector2 target)
    {
        return (float)(Mathf.Atan2(target.y - vertex.y, target.x - vertex.x) * (180 / Mathf.PI));
    }
}
