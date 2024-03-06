using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UI;

public class DrawLine : Graphic
{

    private Material _material;
    public float thickness = 10f;
    public bool center = true;
    public RectTransform[] _recTransform = new RectTransform[2];
    [SerializeField] private int width = 20;
    [SerializeField] private Material newMaterial;
    public int Width { get => width; set => width = value; }



    protected override void OnPopulateMesh(VertexHelper vh)
    {
        vh.Clear();
        if (_recTransform.Length < 2)
            return;


        for (int i = 0; i < _recTransform.Length - 1; i++)
        {

            CreateLineSegment(_recTransform[i].anchoredPosition, _recTransform[i + 1].anchoredPosition, vh);

            int index = i * 5;


            vh.AddTriangle(index, index + 1, index + 3);
            vh.AddTriangle(index + 3, index + 2, index);

            if (i != 0)
            {
                vh.AddTriangle(index, index - 1, index - 3);
                vh.AddTriangle(index + 1, index - 1, index - 2);
            }
        }
    }

    private void CreateLineSegment(Vector2 point1, Vector2 point2, VertexHelper vh)
    {

        Vector3 val1 = new Vector3(point1.x, point1.y, 0);
        Vector3 val2 = new Vector3(point2.x, point2.y, 0);

        UIVertex vertex = UIVertex.simpleVert;
        vertex.color = color;

        // Create the start of the segment
        Quaternion point1Rotation = Quaternion.Euler(0, 0, RotatePointTowards(val1, val2) + 90);
        vertex.position = point1Rotation * new Vector3(-thickness / 2, 0);
        vertex.position += val1;
        vh.AddVert(vertex);
        vertex.position = point1Rotation * new Vector3(thickness / 2, 0);
        vertex.position += val1;


        vh.AddVert(vertex);

        // Create the end of the segment
        Quaternion point2Rotation = Quaternion.Euler(0, 0, RotatePointTowards(val2, val1) - 90);
        vertex.position = point2Rotation * new Vector3(-thickness / 2, 0);
        vertex.position += val2;
        vh.AddVert(vertex);
        vertex.position = point2Rotation * new Vector3(thickness / 2, 0);
        vertex.position += val2;
        vh.AddVert(vertex);

        // Also add the end point
        vertex.position = val2;
        vh.AddVert(vertex);
    }


    private float RotatePointTowards(Vector2 vertex, Vector2 target)
    {
        return (float)(Mathf.Atan2(target.y - vertex.y, target.x - vertex.x) * (180 / Mathf.PI));
    }

    private void Update()
    {
        //material.SetTexture = newMaterial;
        SetMaterialDirty();
        SetVerticesDirty();
    }

    internal void SetVertex(Vector3[] corners)
    {

    }
}
