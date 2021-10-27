using System;
using UnityEngine;

[ExecuteInEditMode]
public class CameraSticker : MonoBehaviour {
    [SerializeField] private Camera camera;
    [SerializeField] private Vector3 viewport = new Vector3(0.5f, 0f, 2.5f);
    [SerializeField] private float tiltAngle = 20f;

    private void Update() {
        Stick();
    }

    private void Stick() {
        transform.position = camera.ViewportToWorldPoint(viewport);
        var cameraTr = camera.transform;
        transform.rotation = Quaternion.LookRotation(cameraTr.forward, cameraTr.up) * Quaternion.AngleAxis(tiltAngle, Vector3.right);
    }
}
