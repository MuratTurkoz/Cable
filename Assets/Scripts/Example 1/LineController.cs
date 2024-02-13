using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineController : MonoBehaviour
{
    private LineRenderer _lr;
    private RectTransform[] _points;
    private void Awake()
    {
        _lr = GetComponent<LineRenderer>();
    }
    public void SetUpLine(RectTransform[] points)
    {
        _lr.positionCount = points.Length;
        _points = points;
    }
    private void Update()
    {
        for (int i = 0; i < _points.Length; i++)
        {
            _lr.SetPosition(i, _points[i].position);
        }
    }
}
