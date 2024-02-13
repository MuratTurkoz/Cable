using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class UIGridRender : Graphic
{
    public float _thickness = 10f;
    public Vector2Int gridSize = new Vector2Int(1, 1);
    public float width;
    public float height;
    public float cellWidth;
    public float cellHeight;
    public int count;

    protected override void OnPopulateMesh(VertexHelper vh)
    {
        //https://www.youtube.com/watch?v=--LB7URk60A
        // https://www.youtube.com/watch?v=M4247oZ8sEI
        //https://www.youtube.com/watch?v=hiIOPyj9ULU
        //"VertexHelper", Unity'de bir UI ��esinin (User Interface - Kullan�c� Aray�z�) grafiksel i�eri�ini olu�turmak, d�zenlemek ve optimize etmek i�in kullan�lan bir yard�mc� s�n�ft�r. Bu s�n�f, bir UI ��esinin vertex'lerini (noktalar�n�), ��genlerini ve di�er grafik bile�enlerini do�rudan manip�le etmeyi sa�lar. "VertexHelper" genellikle bir "MaskableGraphic" alt s�n�f�nda (�rne�in, bir "Image" veya "Text" ��esi) kullan�l�r.
        vh.Clear();
        width = rectTransform.rect.width;
        height = rectTransform.rect.height;
        cellWidth = width / (float)gridSize.x;
        cellHeight = height / (float)gridSize.y;
        count = 0;
        for (int i = 0; i < gridSize.y; i++)
        {
            for (int j = 0; j < gridSize.x; j++)
            {
                DrawCell(i, j, count, vh);
                count++;
            }
        }



        //vh.AddUIVertexQuad(vertex);
        //vh.AddTriangle(2, 3, 0);
    }

    void DrawCell(int x, int y, int index, VertexHelper vh)
    {
        float xPos = cellWidth * x;
        float yPos = cellHeight * y;
        UIVertex vertex = UIVertex.simpleVert;
        //"UIVertex.simpleVert", Unity'de bir UI ��esinin grafiksel i�eri�ini temsil etmek i�in kullan�lan bir yap�d�r
        // position: Vertex'in d�nya uzay�ndaki konumu.
        //color: Vertex'in renk bilgisi (RGBA).
        //normal: Vertex'in normal vekt�r� (3 boyutlu).
        //tangent: Vertex'in tanjant vekt�r� (3 boyutlu).
        //uv0: Vertex'in ilk UV koordinat�.
        //uv1: Vertex'in ikinci UV koordinat�.
        //uv2: Vertex'in ���nc� UV koordinat�.
        //uv3: Vertex'in d�rd�nc� UV koordinat�.

        vertex.color = Color.red;

        vertex.position = new Vector3(xPos, yPos);
        vh.AddVert(vertex);

        vertex.position = new Vector3(xPos, yPos + cellHeight);
        vh.AddVert(vertex);

        vertex.position = new Vector3(xPos + cellWidth, yPos + cellHeight);
        vh.AddVert(vertex);

        vertex.position = new Vector3(xPos + cellWidth, yPos);
        vh.AddVert(vertex);


        //vh.AddTriangle(0, 1, 2);
        //vh.AddTriangle(2,3,0);

        float widthSqr = _thickness * _thickness;
        float distanceSqr = widthSqr / 2;
        float distance = Mathf.Sqrt(distanceSqr);

        vertex.position = new Vector3(xPos + distance, yPos + distance);
        vh.AddVert(vertex);

        vertex.position = new Vector3(xPos + distance, yPos + (cellHeight - distance));
        vh.AddVert(vertex);
        vertex.position = new Vector3(xPos + (cellWidth - distance), yPos + (cellHeight - distance));
        vh.AddVert(vertex);

        vertex.position = new Vector3(xPos + (cellWidth - distance), yPos + distance);
        vh.AddVert(vertex);
        int offset = index * 8;

        //Left Edge
        vh.AddTriangle(offset + 0, offset + 1, offset + 5);
        vh.AddTriangle(offset + 5, offset + 4, offset + 0);

        //Top Edge
        vh.AddTriangle(offset + 1, offset + 2, offset + 6);
        vh.AddTriangle(offset + 6, offset + 5, offset + 1);

        //Right Edge
        vh.AddTriangle(offset + 2, offset + 3, offset + 7);
        vh.AddTriangle(offset + 7, offset + 6, offset + 2);

        //Bottom Edge
        vh.AddTriangle(offset + 3, offset + 0, offset + 4);
        vh.AddTriangle(offset + 4, offset + 7, offset + 3);
    }
}
