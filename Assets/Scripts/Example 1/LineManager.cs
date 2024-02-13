using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineManager : MonoBehaviour
{
    [SerializeField] private RectTransform[] _points;
    [SerializeField] private LineController _controller;

    private void Start()
    {
        _controller.SetUpLine(_points);
    }

}
