using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustCamera : MonoBehaviour
{
    [SerializeField] private Vector2 _aspectVec2;

    void Start()
    {
        Camera _Camera = GetComponent<Camera>();

        float baseAspect = _aspectVec2.x / _aspectVec2.y;
        float screenAspect = (float)Screen.width / (float)Screen.height;
        float Scale = baseAspect / screenAspect;

        if (Scale < 1)
            _Camera.rect = new Rect((1 - Scale) / 2, 0.0f, Scale, 1.0f);
        else
        {
            Scale = 1 / Scale;
            _Camera.rect = new Rect(0.0f, (1 - Scale) / 2, 1.0f, Scale);
        }
    }
}
