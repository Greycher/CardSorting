using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraStabler : MonoBehaviour {
    [SerializeField] private Camera _camera;
    [SerializeField] private float _targetaspect;
    void Start () {
        float windowaspect = (float)Screen.width / (float)Screen.height;
        float scaleheight = windowaspect / _targetaspect;

        if (scaleheight < 1.0f) {  
            Rect rect = _camera.rect;

            rect.width = 1.0f;
            rect.height = scaleheight;
            rect.x = 0;
            rect.y = (1.0f - scaleheight) / 2.0f;
        
            _camera.rect = rect;
        }
        else {
            float scalewidth = 1.0f / scaleheight;

            Rect rect = _camera.rect;

            rect.width = scalewidth;
            rect.height = 1.0f;
            rect.x = (1.0f - scalewidth) / 2.0f;
            rect.y = 0;

            _camera.rect = rect;
        }
    }
}
